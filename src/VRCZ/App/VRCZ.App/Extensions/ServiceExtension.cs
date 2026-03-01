using Microsoft.Extensions.DependencyInjection;
using VRCZ.App.ViewModel;

namespace VRCZ.App.Extensions;

public static class ServiceExtension
{
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        services.AddSingleton<GameLogViewModelFactory>();

        services.AddSingleton<MainWindowViewModel>();

        return services;
    }
}