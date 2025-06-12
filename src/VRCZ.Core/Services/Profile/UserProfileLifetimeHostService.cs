using Microsoft.Extensions.Hosting;

namespace VRCZ.Core.Services.Profile;

public class UserProfileLifetimeHostService(
    UserProfileLifetimeService userProfileLifetimeService,
    CurrentUserProfileService currentUserProfileService) : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        if (!currentUserProfileService.IsProfileLoaded)
            return;

        await userProfileLifetimeService.UnloadProfileAsync();
    }
}
