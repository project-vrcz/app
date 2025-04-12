using System.Net;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using VRCZ.Core.Exceptions;
using VRCZ.Core.Models;
using VRCZ.Core.Services.Database;
using VRCZ.Core.Utils;

namespace VRCZ.Core.Services;

public class UserProfileService(
    ILogger<UserProfileService> logger,
    IServiceScopeFactory serviceScopeFactory)
{
    public bool IsProfileLoaded => CurrentProfile is not null && CurrentProfileSecret is not null;

    public UserProfile? CurrentProfile { get; private set; }
    public UserProfileSecret? CurrentProfileSecret { get; private set; }

    public event EventHandler? ProfileChanged;

    public CookieContainer CookieContainer { get; } = new();

    public async Task<UserProfile[]> GetProfilesAsync()
    {
        var profilesStoragePath = ProfileStorageUtils.GetUserProfilesStoragePath();

        if (!Directory.Exists(profilesStoragePath))
            return [];

        var profileDirectories = Directory.GetDirectories(profilesStoragePath);

        List<UserProfile> profiles = [];
        foreach (var profileId in profileDirectories)
        {
            var profilePath = Path.Combine(ProfileStorageUtils.GetUserProfileStoragePath(profileId));
            var profileMetaDataPath = Path.Combine(profilePath, ProfileStorageUtils.UserProfileMetaDataFileName);
            var profileSecretPath = Path.Combine(profilePath, ProfileStorageUtils.UserProfileSecretFileName);

            if (!File.Exists(profileMetaDataPath) || !File.Exists(profileSecretPath))
                break;

            var profileData = await ParseUserProfile(profileMetaDataPath, profileSecretPath);
            profiles.Add(profileData.Profile);
        }

        return profiles.ToArray();
    }

    public async Task LoadProfileAsync(string profileId)
    {
        logger.LogInformation("Loading profile {ProfileId}", profileId);

        var profilePath = Path.Combine(ProfileStorageUtils.GetUserProfileStoragePath(profileId));
        var profileMetaDataPath = Path.Combine(profilePath, ProfileStorageUtils.UserProfileMetaDataFileName);
        var profileSecretPath = Path.Combine(profilePath, ProfileStorageUtils.UserProfileSecretFileName);

        if (!File.Exists(profileMetaDataPath) || !File.Exists(profileSecretPath))
        {
            throw new UserProfileNoExistException(profileId);
        }

        var profileData = await ParseUserProfile(profileMetaDataPath, profileSecretPath);

        CurrentProfile = profileData.Profile;
        CurrentProfileSecret = profileData.ProfileSecret;

        if (CurrentProfile is null || CurrentProfileSecret is null)
        {
            throw new InvalidOperationException("Failed to load profile");
        }

        foreach (Cookie cookie in CookieContainer.GetAllCookies())
        {
            cookie.Expired = true;
        }

        foreach (var cookie in CurrentProfileSecret.Cookies)
        {
            CookieContainer.Add(cookie);
        }

        await using var scope = serviceScopeFactory.CreateAsyncScope();
        var databaseService = scope.ServiceProvider.GetRequiredService<DatabaseInitializeMigrateService>();

        logger.LogInformation("Loading database for profile {ProfileId}", profileId);
        await databaseService.EnsureDatabaseReadyAsync();
        logger.LogInformation("Database for profile {ProfileId} READY", profileId);

        logger.LogInformation("Profile {ProfileId} loaded", profileId);
        ProfileChanged?.Invoke(this, EventArgs.Empty);
    }

    private static async Task<(UserProfile Profile, UserProfileSecret ProfileSecret)> ParseUserProfile(
        string metadataPath, string secretPath)
    {
        var rawMetaData = await File.ReadAllTextAsync(metadataPath);
        var rawSecret = await File.ReadAllTextAsync(secretPath);

        var metaData = JsonSerializer.Deserialize<UserProfile>(rawMetaData, UserProfileContext.Default.UserProfile);
        var secret =
            JsonSerializer.Deserialize<UserProfileSecret>(rawSecret, UserProfileContext.Default.UserProfileSecret);

        if (metaData is null || secret is null)
        {
            throw new InvalidOperationException("Failed to load profile");
        }

        return (metaData, secret);
    }

    public async Task UnloadProfileAsync()
    {
        if (!IsProfileLoaded)
        {
            throw new InvalidOperationException("No profile loaded");
        }

        logger.LogInformation("Unloading profile {ProfileId}", CurrentProfile?.Id);

        await SaveProfileAsync();

        CurrentProfile = null;
        CurrentProfileSecret = null;

        logger.LogInformation("Profile unloaded");
        ProfileChanged?.Invoke(this, EventArgs.Empty);
    }

    public async Task CreateProfileAsync(string profileId, string username, string displayName, string avatarUrl,
        string password)
    {
        var profileStoragePath = ProfileStorageUtils.GetUserProfileStoragePath(profileId);
        var profileMetaDataPath = Path.Combine(profileStoragePath, ProfileStorageUtils.UserProfileMetaDataFileName);
        var profileSecretPath = Path.Combine(profileStoragePath, ProfileStorageUtils.UserProfileSecretFileName);

        if (Directory.Exists(profileStoragePath) && Directory.GetFiles(profileStoragePath).Length > 0)
        {
            throw new UserProfileAlreadyExistException(profileId);
        }

        ProfileStorageUtils.EnsureProfileStorage(profileId);

        var profile = new UserProfile
        {
            Id = profileId,
            Username = username,
            DisplayName = displayName,
            AvatarUrl = avatarUrl
        };

        var secret = new UserProfileSecret
        {
            Cookies = CookieContainer.GetAllCookies().ToList(),
            Username = username,
            Password = password
        };

        await File.WriteAllTextAsync(profileMetaDataPath,
            JsonSerializer.Serialize(profile, UserProfileContext.Default.UserProfile));
        await File.WriteAllTextAsync(profileSecretPath,
            JsonSerializer.Serialize(secret, UserProfileContext.Default.UserProfileSecret));
    }

    public async Task SaveProfileAsync()
    {
        if (CurrentProfile is null || CurrentProfileSecret is null)
        {
            throw new InvalidOperationException("No profile loaded");
        }

        logger.LogInformation("Saving profile {ProfileId}", CurrentProfile.Id);

        var profileStoragePath = ProfileStorageUtils.GetUserProfileStoragePath(CurrentProfile.Id);
        var secretFilePath = Path.Combine(profileStoragePath, ProfileStorageUtils.UserProfileSecretFileName);
        var metaDataFilePath = Path.Combine(profileStoragePath, ProfileStorageUtils.UserProfileMetaDataFileName);

        ProfileStorageUtils.EnsureProfileStorage(CurrentProfile.Id);

        CurrentProfileSecret.Cookies = CookieContainer.GetAllCookies().ToList();

        await File.WriteAllTextAsync(secretFilePath,
            JsonSerializer.Serialize(CurrentProfileSecret, UserProfileContext.Default.UserProfileSecret));
        await File.WriteAllTextAsync(metaDataFilePath,
            JsonSerializer.Serialize(CurrentProfile, UserProfileContext.Default.UserProfile));

        logger.LogInformation("Profile {ProfileId} saved", CurrentProfile.Id);
    }
}
