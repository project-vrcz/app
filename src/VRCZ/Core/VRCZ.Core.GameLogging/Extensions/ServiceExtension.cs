using Microsoft.Extensions.DependencyInjection;
using VRCZ.Core.GameLogging.Services;

namespace VRCZ.Core.GameLogging.Extensions;

public static class ServiceExtension
{
    public static IServiceCollection AddGameLogging(this IServiceCollection services)
    {
        services.AddSingleton<GameLogFileSystemWatcherService>();
        services.AddHostedService(s => s.GetRequiredService<GameLogFileSystemWatcherService>());

        return services;
    }
}