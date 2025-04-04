using AsyncImageLoader;
using AsyncImageLoader.Loaders;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using HotAvalonia;
using Microsoft.Extensions.DependencyInjection;
using VRCZ.App.Controls.AsyncImageLoader;
using VRCZ.App.Pages;
using VRCZ.App.Services;
using VRCZ.App.ViewModels;
using VRCZ.App.ViewModels.Pages;

namespace VRCZ.App;

public class App : Application
{
    private readonly IServiceProvider _serviceProvider = null!;

    private readonly IAsyncImageLoader _asyncImageLoader;

    public App(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;

        _asyncImageLoader = _serviceProvider.GetRequiredService<AsyncImageLoaderFactory>().Create();
    }

    public App()
    {
        // Make Previewer happy

        _asyncImageLoader = new RamCachedWebImageLoader();
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

        ImageLoader.AsyncImageLoader.Dispose();
        ImageLoader.AsyncImageLoader = _asyncImageLoader;

        ImageBrushLoader.AsyncImageLoader.Dispose();
        ImageBrushLoader.AsyncImageLoader = _asyncImageLoader;

        UrsaAvatarLoader.AsyncImageLoader.Dispose();
        UrsaAvatarLoader.AsyncImageLoader = _asyncImageLoader;

        base.OnFrameworkInitializationCompleted();
    }
}
