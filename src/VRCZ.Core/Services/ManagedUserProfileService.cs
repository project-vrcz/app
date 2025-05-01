using VRCZ.Core.Exceptions;
using VRCZ.VRChatApi.Generated.Models;

namespace VRCZ.Core.Services;

public class ManagedUserProfileService(
    VRChatPipelineService vrchatPipelineService,
    VRChatAuthService vrchatAuthService,
    UserProfileService userProfileService,
    VRChatTrackedEntitiesService vrchatTrackedEntitiesService,
    VRChatLoggingService vrchatLoggingService)
{
    public async Task LoadProfileAsync(string userId,
        Func<TwoFactorRequired_requiresTwoFactorAuth[], Task>? handleTwoFactorRequired = null)
    {
        try
        {
            await userProfileService.LoadProfileAsync(userId);

            CurrentUser currentUser;
            try
            {
                currentUser = await vrchatAuthService.UpdateProfileForCurrentAccountAsync();
            }
            catch (RequireRefreshTwoFactorException requireRefreshTwoFactorException)
            {
                if (handleTwoFactorRequired is null) throw;

                await handleTwoFactorRequired(requireRefreshTwoFactorException.AvailableTwoFactor);

                currentUser = await vrchatAuthService.UpdateProfileForCurrentAccountAsync();
            }

            vrchatTrackedEntitiesService.SetLoggedInUser(currentUser);
            await vrchatTrackedEntitiesService.GetFriendsAsync();

            await vrchatPipelineService.ConnectAsync();
            await vrchatLoggingService.StartAsync();
        }
        catch
        {
            if (userProfileService.IsProfileLoaded)
            {
                await userProfileService.UnloadProfileAsync();
            }

            throw;
        }
    }
}
