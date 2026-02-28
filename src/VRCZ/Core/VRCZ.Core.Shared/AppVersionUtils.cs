using System.Reflection;
using VRCZ.Core.Shared.Attributes;

namespace VRCZ.Core.Shared;

public static class AppVersionUtils
{
    public static string GetAppVersion()
    {
        var assembly = Assembly.GetExecutingAssembly();
        return assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion ?? "snapshot";
    }

    public static string GetAppCommitHash()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var commitHashAttribute = assembly.GetCustomAttribute<GitCommitHashAttribute>();
        return commitHashAttribute?.CommitHash ?? "unknown";
    }
    
    public static DateTimeOffset? GetAppBuildDate()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var buildDateAttribute = assembly.GetCustomAttribute<BuildDateTimeAttribute>();
        return buildDateAttribute?.DateTime;
    }
}