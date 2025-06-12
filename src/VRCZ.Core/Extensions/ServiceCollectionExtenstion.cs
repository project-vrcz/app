using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Headers;
using Microsoft.Extensions.DependencyInjection;
using VRCZ.Core.DbContexts;
using VRCZ.Core.Services;
using VRCZ.Core.Services.Database;
using VRCZ.Core.Services.LocalFavorites;
using VRCZ.Core.Services.Profile;
using VRCZ.Core.Services.Tracking;

namespace VRCZ.Core.Extensions;

[ExcludeFromCodeCoverage]
public static class ServiceCollectionExtenstion
{
    public static IServiceCollection AddVRCZCore(this IServiceCollection services)
    {
        services.AddMemoryCache();

        services.AddKiotaHandlers();

        services.AddSingleton<MessengerService>();

        services.AddSingleton<VRChatAuthService>();

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
                CookieContainer = servicesProvider.GetRequiredService<CurrentUserProfileService>().CookieContainer,
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

        #region Profile

        services.AddSingleton<CurrentUserProfileService>();
        services.AddTransient<UserProfileLifetimeService>();
        services.AddHostedService<UserProfileLifetimeHostService>();

        services.AddTransient<UserProfileService>();

        #endregion

        #region Tracking

        services.AddSingleton<VRChatTrackedEntitiesService>();
        services.AddTransient<AvatarLocalFavoritesService>();
        services.AddTransient<VRChatLoggingService>();

        #endregion

        return services;
    }
}
