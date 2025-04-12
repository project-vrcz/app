using System.Net.Http.Headers;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using HotAvalonia;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using VRCZ.App.Pages;
using VRCZ.App.ViewModels;
using VRCZ.App.ViewModels.Pages;
using VRCZ.Core.Services;

namespace VRCZ.App;

public class App : Application
{
    private readonly IServiceProvider _serviceProvider = null!;

    public readonly AppWebImageLoader AsyncImageLoader;

#pragma warning disable CS8600
#pragma warning disable CS8603
    public new static App Current => (App)Application.Current;
#pragma warning restore CS8603
#pragma warning restore CS8600

    public App(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;

        AsyncImageLoader = _serviceProvider.GetRequiredService<AppWebImageLoader>();
    }

    public App()
    {
        // Make Previewer happy

        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("VRCZ AvaloniaUI Previewer"));

        AsyncImageLoader = new AppWebImageLoader(new RemoteImageService(httpClient, new MemoryCache(new MemoryCacheOptions())));
    }

    public override void Initialize()
    {
        ViewLocator.Register<HomeViewModel, HomePage>();

        this.EnableHotReload();
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var trayMenuViewModel = _serviceProvider.GetRequiredService<TrayMenuViewModel>();
            trayMenuViewModel.ApplicationLifetime = desktop;

            DataContext = trayMenuViewModel;

            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        base.OnFrameworkInitializationCompleted();
    }
}
