using Avalonia;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using HotAvalonia;
using Lemon.Hosting.AvaloniauiDesktop;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using VRCZ.App;
using VRCZ.App.Extenstions;
using VRCZ.App.ViewModels;
using VRCZ.Core.Extensions;

namespace VRCZ.Desktop;

internal sealed class Program
{
#pragma warning disable CS8618
    public static IServiceProvider ServiceProvider { get; private set; }
#pragma warning restore CS8618

    [STAThread]
    [SupportedOSPlatform("windows")]
    [SupportedOSPlatform("linux")]
    [SupportedOSPlatform("macos")]
    public static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .Enrich.FromLogContext()
#if DEBUG
            .WriteTo.Console(applyThemeToRedirectedOutput: true, theme: AnsiConsoleTheme.Code)
#else
            .WriteTo.Console(applyThemeToRedirectedOutput: false, theme: AnsiConsoleTheme.Code)
#endif
            .WriteTo.Debug()
            .CreateLogger();

        var hostBuilder = CreateHostBuilder(args);

        RunApp(hostBuilder, args);
    }

    public static HostApplicationBuilder CreateHostBuilder(string[] args)
    {
        var hostBuilder = Host.CreateEmptyApplicationBuilder(new HostApplicationBuilderSettings
        {
            Args = args,
            ApplicationName = "VRCZ.Desktop",
            ContentRootPath = Environment.CurrentDirectory
        });

        hostBuilder.Configuration
            .AddCommandLine(args)
            .AddInMemoryCollection();

        hostBuilder.Services.AddSerilog();

        hostBuilder.Services.AddVRCZCore();
        hostBuilder.Services.AddVRCZApp();

        return hostBuilder;
    }

    private static AppBuilder ConfigAvaloniaAppBuilder(AppBuilder appBuilder)
    {
        return appBuilder
            .UseHotReload()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
    }

    [SupportedOSPlatform("windows")]
    [SupportedOSPlatform("linux")]
    [SupportedOSPlatform("macos")]
    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    private static void RunApp(HostApplicationBuilder hostBuilder, string[] args)
    {
        hostBuilder.Services.AddAvaloniauiDesktopApplication<App.App>(ConfigAvaloniaAppBuilder);
        hostBuilder.Services.AddMainWindow<MainWindow, MainWindowViewModel>();
        hostBuilder.Services.AddApplicationLifetime<MainWindow>(_ => new ClassicDesktopStyleApplicationLifetime
        {
            ShutdownMode = ShutdownMode.OnExplicitShutdown,
            Args = args
        });

        // build host
        var appHost = hostBuilder.Build();
        ServiceProvider = appHost.Services.GetRequiredService<IServiceProvider>();

        // run app
        appHost.RunAvaloniauiApplication(args);
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App.App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
}
