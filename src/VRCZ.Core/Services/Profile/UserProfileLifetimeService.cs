using System.Net;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using VRCZ.Core.Exceptions;
using VRCZ.Core.Models;
using VRCZ.Core.Models.VRChat;
using VRCZ.Core.Services.Database;
using VRCZ.Core.Services.Tracking;
using VRCZ.VRChatApi.Generated;
using VRCZ.VRChatApi.Generated.Auth.User;
using VRCZ.VRChatApi.Generated.Models;

namespace VRCZ.Core.Services.Profile;

public class UserProfileLifetimeService(
    VRChatPipelineService vrchatPipelineService,
    CurrentUserProfileService currentUserProfileService,
    UserProfileService userProfileService,
    VRChatTrackedEntitiesService trackedEntitiesService,
    VRChatLoggingService vrchatLoggingService,
    VRChatApiClient apiClient,
    VRChatAuthService authService,
    ILogger<UserProfileLifetimeService> logger,
    IServiceScopeFactory serviceScopeFactory)
{
    public async Task LoadProfileAsync(string userId,
        Func<TwoFactorRequired_requiresTwoFactorAuth[], Task>? handleTwoFactorRequired = null)
    {
        try
        {
            await currentUserProfileService.LoadProfileAsync(userId);

            await using var scope = serviceScopeFactory.CreateAsyncScope();
            var databaseInitializeMigrateService =
                scope.ServiceProvider.GetRequiredService<DatabaseInitializeMigrateService>();

            await databaseInitializeMigrateService.EnsureDatabaseReadyAsync();

            CurrentUser currentUser;
            try
            {
                currentUser = await UpdateProfileFromRemoteAsync();
            }
            catch (RequireRefreshTwoFactorException requireRefreshTwoFactorException)
            {
                if (handleTwoFactorRequired is null) throw;

                await handleTwoFactorRequired(requireRefreshTwoFactorException.AvailableTwoFactor);

                currentUser = await UpdateProfileFromRemoteAsync();
            }

            trackedEntitiesService.SetLoggedInUser(currentUser);
            await trackedEntitiesService.GetFriendsAsync();

            await trackedEntitiesService.StartAsync();

            await vrchatPipelineService.ConnectAsync();
            await vrchatLoggingService.StartAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to load profile for user {UserId}", userId);

            if (currentUserProfileService.IsProfileLoaded)
            {
                await UnloadProfileAsync();
            }

            throw;
        }
    }

    public async Task UnloadProfileAsync()
    {
        if (!currentUserProfileService.IsProfileLoaded)
            throw new InvalidOperationException("No profile loaded");

        await currentUserProfileService.UnloadProfileAsync();

        await vrchatPipelineService.DisconnectAsync();
        await vrchatLoggingService.StopAsync();
        await trackedEntitiesService.StartAsync();
    }

    #region Create & Update

    public async Task<string> CreateProfileFromCurrentAsync(string password)
    {
        if (currentUserProfileService.IsProfileLoaded)
            throw new InvalidOperationException("Profile is already loaded");

        var userResponse = await apiClient.Auth.User.GetAsUserGetResponseAsync();

        if (userResponse?.CurrentUser?.Id is null)
            throw new InvalidOperationException("Not logged in");

        if (userResponse.CurrentUser.DisplayName is null)
            throw new UnexpectedApiBehaviourException("User DisplayName is null");

        // You can still get current user's username
#pragma warning disable CS0618
        if (userResponse.CurrentUser.Username is null)
#pragma warning restore CS0618
            throw new UnexpectedApiBehaviourException("User Username is null");

        var profileId = userResponse.CurrentUser.Id;
        var avatarUrl = GetAvatarUrl(userResponse.CurrentUser.UserIcon, userResponse.CurrentUser.CurrentAvatarImageUrl);

#pragma warning disable CS0618
        await userProfileService.CreateProfileAsync(profileId, userResponse.CurrentUser.Username,
            userResponse.CurrentUser.DisplayName,
            avatarUrl, password, currentUserProfileService.CookieContainer.GetAllCookies().ToArray());
#pragma warning restore CS0618

        currentUserProfileService.ClearCookies();

        return userResponse.CurrentUser.Id;
    }

    public async ValueTask<CurrentUser> UpdateProfileFromRemoteAsync()
    {
        var secret = currentUserProfileService.CurrentProfileSecret;
        var profile = currentUserProfileService.CurrentProfile;

        if (!currentUserProfileService.IsProfileLoaded || secret is null ||
            profile is null)
            throw new InvalidOperationException("Profile is not loaded");

        UserRequestBuilder.UserGetResponse? userResponse;
        try
        {
            userResponse = await apiClient.Auth.User.GetAsUserGetResponseAsync();
        }
        catch (Error apiError)
        {
            if (apiError.ErrorProp?.StatusCode != (int)VRChatApiErrorStatusCode.MissingCredentials)
                throw;

            var loginResult = await authService.LoginAsync(profile.Username,
                secret.Password);

            if (loginResult.ResultType != LoginResultType.Success)
            {
                throw;
            }

            userResponse = new UserRequestBuilder.UserGetResponse
            {
                CurrentUser = loginResult.User
            };
        }

        if (userResponse?.TwoFactorRequired is { } twoFactorRequired)
        {
            var availableMethods = twoFactorRequired.RequiresTwoFactorAuth?
                .Where(method => method is not null)
                .OfType<TwoFactorRequired_requiresTwoFactorAuth>()
                .ToArray() ?? [];

            if (availableMethods.Length == 0)
                throw new UnexpectedApiBehaviourException("TwoFactorRequired.RequiresTwoFactorAuth is empty");

            throw new RequireRefreshTwoFactorException(availableMethods);
        }

        if (userResponse?.CurrentUser?.Id is not { } userId)
            throw new UnexpectedApiBehaviourException("Api didn't require two factor but didn't return user id");

        if (profile.Id != userId)
            throw new InvalidOperationException("Api response user id is not same with loaded profile id");

        if (userResponse.CurrentUser.DisplayName is not { } displayName)
            throw new UnexpectedApiBehaviourException("User DisplayName is null");

        var avatarUrl = GetAvatarUrl(userResponse.CurrentUser.UserIcon, userResponse.CurrentUser.CurrentAvatarImageUrl);

        await currentUserProfileService.UpdateAsync(displayName, avatarUrl);

        return userResponse.CurrentUser;
    }

    #endregion

    private static string GetAvatarUrl(string? userIcon, string? currentAvatarImageUrl)
    {
        return !string.IsNullOrWhiteSpace(userIcon)
            ? userIcon
            : !string.IsNullOrWhiteSpace(currentAvatarImageUrl)
                ? currentAvatarImageUrl
                : throw new UnexpectedApiBehaviourException(
                    "User CurrentAvatarImageUrl and UserIcon is null at same time");
    }
}
