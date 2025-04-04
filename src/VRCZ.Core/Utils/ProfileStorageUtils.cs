namespace VRCZ.Core.Utils;

public static class ProfileStorageUtils
{
    public const string ProfileStorageRootFolderName = "VRCZ";

    public const string UserProfileMetaDataFileName = "profile.json";
    public const string UserProfileSecretFileName = "secret.json";

    public static string GetProfileStorageRootPath()
    {
        var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData,
            Environment.SpecialFolderOption.DoNotVerify);

        return Path.Combine(appDataPath, ProfileStorageRootFolderName);
    }

    public static string GetImageCacheRootPath()
    {
        return Path.Combine(GetProfileStorageRootPath(), "ImageCache");
    }

    public static string GetUserProfilesStoragePath()
    {
        return Path.Combine(GetProfileStorageRootPath(), "Profiles");
    }

    public static string GetUserProfileStoragePath(string profileId)
    {
        return Path.Combine(GetUserProfilesStoragePath(), profileId);
    }

    public static void EnsureProfileStorage()
    {
        var profileStoragePath = GetUserProfilesStoragePath();

        if (!Directory.Exists(profileStoragePath))
            Directory.CreateDirectory(profileStoragePath);
    }

    public static void EnsureProfileStorage(string profileId)
    {
        var profileStoragePath = GetUserProfileStoragePath(profileId);

        if (!Directory.Exists(profileStoragePath))
            Directory.CreateDirectory(profileStoragePath);
    }
}
