using System.Net.Http.Headers;
using Microsoft.Extensions.DependencyInjection;
using VRCZ.Core.DbContexts;
using VRCZ.Core.Services;
using VRCZ.Core.Services.Database;
using VRCZ.Core.Services.LocalFavorites;
using VRCZ.Core.Services.Tracking;

namespace VRCZ.Core.Extensions;

public static class ServiceCollectionExtenstion
{
    public static IServiceCollection AddVRCZCore(this IServiceCollection services)
    {
        services.AddMemoryCache();

        services.AddKiotaHandlers();

        services.AddSingleton<VRChatAuthService>();
        services.AddSingleton<UserProfileService>();

        services.AddTransient<IConnectionStringProvider, ProfileConnectionStringProvider>();
        services.AddDbContext<AppDbContext>();
        services.AddTransient<DatabaseInitializeMigrateService>();

        services.AddTransient(serviceProvider =>
            serviceProvider.GetRequiredService<VRChatApiClientFactory>().GetClient());
        services.AddHttpClient<VRChatApiClientFactory>(client =>
            {
                client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("VRCZ.Core", "snapshot"));
            })
            .ConfigurePrimaryHttpMessageHandler(servicesProvider => new SocketsHttpHandler
            {
                CookieContainer = servicesProvider.GetRequiredService<UserProfileService>().CookieContainer,
                UseCookies = true,
                PooledConnectionLifetime = TimeSpan.FromMinutes(2)
            })
            .AttachKiotaHandlers();

        services.AddSingleton<VRChatPipelineService>();
        services.AddHttpClient<VRChatPipelineService>(client =>
        {
            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("VRCZ.Core", "snapshot"));
        });

        services.AddSingleton<RemoteImageService>();
        services.AddHttpClient<RemoteImageService>(client =>
                client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("VRCZ.App", "snapshot"))
            )
            .ConfigurePrimaryHttpMessageHandler(() => new SocketsHttpHandler
            {
                PooledConnectionLifetime = TimeSpan.FromMinutes(2),
            });

        services.AddHostedService<UserProfileHostService>();

        services.AddTransient<ManagedUserProfileService>();

        services.AddSingleton<VRChatTrackedEntitiesService>();

        services.AddTransient<AvatarLocalFavoritesService>();

        services.AddTransient<VRChatLoggingService>();

        return services;
    }
}
