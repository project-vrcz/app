using System.Text;
using Microsoft.Kiota.Http.HttpClientLibrary.Middleware.Options;
using VRCZ.Core.Exceptions;
using VRCZ.Core.Models;
using VRCZ.Core.Models.VRChat;
using VRCZ.VRChatApi.Generated;
using VRCZ.VRChatApi.Generated.Auth.User;
using VRCZ.VRChatApi.Generated.Models;

namespace VRCZ.Core.Services;

public class VRChatAuthService(UserProfileService userProfileService, VRChatApiClient vrchatApiClient)
{
    /// <summary>
    /// Login
    /// </summary>
    /// <param name="username">Username or Email</param>
    /// <param name="password">Password</param>
    /// <returns>User Information</returns>
    /// <exception cref="UnexpectedApiBehaviourException">Unexpected Api Behaviour</exception>
    /// <exception cref="Error">Api Error (e.g. Password is wrong or too many session)</exception>
    public async Task<LoginResult> LoginAsync(string username, string password)
    {
        var token = Convert.ToBase64String(
            Encoding.UTF8.GetBytes($"{Uri.EscapeDataString(username)}:{Uri.EscapeDataString(password)}"));

        var headersInspectionHandler = new HeadersInspectionHandlerOption()
        {
            InspectResponseHeaders = true
        };

        var result = await vrchatApiClient.Auth.User.GetAsUserGetResponseAsync(config =>
        {
            config.Headers.Add("Authorization", $"Basic {token}");

            config.Options.Add(headersInspectionHandler);
        });

        if (result is null)
            throw new UnexpectedApiBehaviourException("Auth User endpoint response null body");

        if (result.CurrentUser is not null)
            return new LoginResult(LoginResultType.Success, User: result.CurrentUser);

        if (result.TwoFactorRequired is not null)
        {
            var availableMethods = result.TwoFactorRequired.RequiresTwoFactorAuth?
                .Where(method => method is not null)
                .OfType<TwoFactorRequired_requiresTwoFactorAuth>()
                .ToArray() ?? [];

            return new LoginResult(LoginResultType.TwoFactorRequired, availableMethods);
        }

        throw new UnexpectedApiBehaviourException("Auth User endpoint response null body");
    }

    public async Task<bool> VerifyTwoFactorAsync(string code, TwoFactorRequired_requiresTwoFactorAuth method)
    {
        var body = new TwoFactorAuthCode
        {
            Code = code
        };

        var result = method switch
        {
            TwoFactorRequired_requiresTwoFactorAuth.Otp => await vrchatApiClient.Auth.Twofactorauth.Otp.Verify.PostAsync(body),
            TwoFactorRequired_requiresTwoFactorAuth.Totp => await vrchatApiClient.Auth.Twofactorauth.Totp.Verify.PostAsync(body),
            _ => throw new NotImplementedException()
        };

        if (result?.Verified is not { } verified)
            throw new UnexpectedApiBehaviourException("Auth Twofactorauth Totp Verify endpoint response null body");

        return verified;
    }

    public async Task CreateProfileForCurrentAccountAsync(string password)
    {
        var userResponse = await vrchatApiClient.Auth.User.GetAsUserGetResponseAsync();

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
            avatarUrl, password);
#pragma warning restore CS0618

        await userProfileService.LoadProfileAsync(profileId);
    }

    public async ValueTask<CurrentUser> UpdateProfileForCurrentAccountAsync()
    {
        if (!userProfileService.IsProfileLoaded || userProfileService.CurrentProfileSecret is null ||
            userProfileService.CurrentProfile is null)
            throw new InvalidOperationException("Profile is not loaded");

        UserRequestBuilder.UserGetResponse? userResponse;
        try
        {
            userResponse = await vrchatApiClient.Auth.User.GetAsUserGetResponseAsync();
        }
        catch (Error apiError)
        {
            if (apiError.ErrorProp?.StatusCode != (int)VRChatApiErrorStatusCode.MissingCredentials)
                throw;

            var loginResult = await LoginAsync(userProfileService.CurrentProfileSecret.Username,
                userProfileService.CurrentProfileSecret.Password);

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

        if (userProfileService.CurrentProfile.Id != userId)
            throw new InvalidOperationException("Api response user id is not same with loaded profile id");

        if (userResponse.CurrentUser.DisplayName is null)
            throw new UnexpectedApiBehaviourException("User DisplayName is null");

        var avatarUrl = GetAvatarUrl(userResponse.CurrentUser.UserIcon, userResponse.CurrentUser.CurrentAvatarImageUrl);

        userProfileService.CurrentProfile.DisplayName = userResponse.CurrentUser.DisplayName;
        userProfileService.CurrentProfile.AvatarUrl = avatarUrl;

        await userProfileService.SaveProfileAsync();

        return userResponse.CurrentUser;
    }

    private string GetAvatarUrl(string? userIcon, string? currentAvatarImageUrl)
    {
        return !string.IsNullOrWhiteSpace(userIcon)
            ? userIcon
            : !string.IsNullOrWhiteSpace(currentAvatarImageUrl)
                ? currentAvatarImageUrl
                : throw new UnexpectedApiBehaviourException(
                    "User CurrentAvatarImageUrl and UserIcon is null at same time");
    }

    public string? GetAuthCookie()
    {
        var cookieContainer = userProfileService.CookieContainer;

        var cookies = cookieContainer.GetCookies(new Uri("https://api.vrchat.cloud"));

        var authCookie = cookies["auth"];

        return authCookie?.Value;
    }
}
