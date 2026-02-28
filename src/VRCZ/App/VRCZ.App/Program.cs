using Avalonia;
using System;
using HotAvalonia;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Formatting.Compact;
using Serilog.Sinks.SystemConsole.Themes;
using VRCZ.App.Extensions;
using VRCZ.Core.Shared;

namespace VRCZ.App;

class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args)
    {
        var jsonLogPath = Path.Combine(AppStoragePathUtils.GetLogsPath(), "log-.json");
        var plainTextLogPath = Path.Combine(AppStoragePathUtils.GetLogsPath(), "log-.log");
        Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .Enrich.WithProperty("Application", "Project VRCZ App")
            .Enrich.WithProperty("ApplicationVersion", AppVersionUtils.GetAppVersion())
            .Enrich.WithProperty("ApplicationBuildDate", AppVersionUtils.GetAppBuildDate())
            .Enrich.WithProperty("ApplicationCommitHash", AppVersionUtils.GetAppCommitHash())
            .WriteTo.Console(applyThemeToRedirectedOutput: true, theme: AnsiConsoleTheme.Code)
            .WriteTo.Async(writer =>
                writer.File(new CompactJsonFormatter(), jsonLogPath,
                    rollingInterval: RollingInterval.Day))
            .WriteTo.Async(writer =>
                writer.File(plainTextLogPath, rollingInterval: RollingInterval.Day))
            .WriteTo.Debug()
            .CreateLogger();

        Log.Information(
            "Project VRCZ App v{AppVersion} built on {AppBuildDate} (commit {AppCommitHash}) starting up...",
            AppVersionUtils.GetAppVersion(),
            AppVersionUtils.GetAppBuildDate(),
            AppVersionUtils.GetAppCommitHash()
        );

        try
        {
            var builder = new HostApplicationBuilder();

            builder.Services.AddSerilog();

            builder.Services.AddAvaloniaApplication<App>(appBuilder => appBuilder
                .UseHotReload()
                .UsePlatformDetect()
                .WithInterFont()
                .LogToTrace());

            using var host = builder.Build();

            host.RunAvaloniaWaitForShutdown(args);
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Oops, the application has crashed!");
            Environment.ExitCode = -1;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
}