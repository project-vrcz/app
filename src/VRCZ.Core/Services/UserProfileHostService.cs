using Microsoft.Extensions.Hosting;
using VRCZ.Core.Services.Tracking;

namespace VRCZ.Core.Services;

public class UserProfileHostService(
    UserProfileService userProfileService,
    VRChatPipelineService vrchatPipelineService,
    VRChatLoggingService vrchatLoggingService)
    : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        userProfileService.ProfileChanged += OnProfileChanged;
        return Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await userProfileService.SaveProfileAsync();

        await vrchatPipelineService.DisconnectAsync();
        await vrchatLoggingService.StopAsync();
    }

    private async void OnProfileChanged(object? sender, EventArgs e)
    {
        try
        {
            if (!userProfileService.IsProfileLoaded)
            {
                await vrchatPipelineService.DisconnectAsync();
                await vrchatLoggingService.StopAsync();
            }
        }
        catch
        {
        }
    }
}
