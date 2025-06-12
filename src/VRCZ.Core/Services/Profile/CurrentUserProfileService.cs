using System.Net;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using VRCZ.Core.Exceptions;
using VRCZ.Core.Helpers;
using VRCZ.Core.Models;
using VRCZ.Core.Models.VRChat;
using VRCZ.Core.Utils;
using VRCZ.VRChatApi.Generated;
using VRCZ.VRChatApi.Generated.Auth.User;
using VRCZ.VRChatApi.Generated.Models;

namespace VRCZ.Core.Services.Profile;

public class CurrentUserProfileService(ILogger<CurrentUserProfileService> logger)
{
    public bool IsProfileLoaded => _userProfile is not null && _userProfileSecret is not null;

    private UserProfile? _userProfile;
    private UserProfileSecret? _userProfileSecret;

    public UserProfile CurrentProfile
    {
        get
        {
            if (_userProfile is null || _userProfileSecret is null)
                throw new InvalidOperationException("No profile loaded");

            return _userProfile;
        }
    }

    public UserProfileSecret CurrentProfileSecret
    {
        get
        {
            if (_userProfile is null || _userProfileSecret is null)
                throw new InvalidOperationException("No profile loaded");

            return _userProfileSecret;
        }
    }

    public CookieContainer CookieContainer { get; } = new();

    public event EventHandler<UserProfile>? ProfileLoaded;
    public event EventHandler? ProfileUnloading;

    #region Load & Unload & Save

    internal async Task UnloadProfileAsync()
    {
        if (!IsProfileLoaded)
            throw new InvalidOperationException("Profile is not loaded");

        logger.LogInformation("Unloading profile {ProfileId}", CurrentProfile.Id);
        ProfileUnloading?.Invoke(this, EventArgs.Empty);

        await SaveProfileAsync();

        _userProfile = null;
        _userProfileSecret = null;

        ClearCookies();

        logger.LogInformation("Profile unloaded");
    }

    internal async Task LoadProfileAsync(string profileId)
    {
        if (IsProfileLoaded)
            throw new InvalidOperationException("Profile is already loaded");

        logger.LogInformation("Loading profile {ProfileId}", profileId);

        var profilePath = Path.Combine(ProfileStorageUtils.GetUserProfileStoragePath(profileId));
        var profileMetaDataPath = Path.Combine(profilePath, ProfileStorageUtils.UserProfileMetaDataFileName);
        var profileSecretPath = Path.Combine(profilePath, ProfileStorageUtils.UserProfileSecretFileName);

        if (!File.Exists(profileMetaDataPath) || !File.Exists(profileSecretPath))
        {
            throw new UserProfileNoExistException(profileId);
        }

        var profileData = await UserProfileHelper.ParseUserProfileAsync(profileMetaDataPath, profileSecretPath);

        _userProfile = profileData.Profile;
        _userProfileSecret = profileData.ProfileSecret;

        if (CurrentProfile is null || _userProfileSecret is null)
        {
            throw new InvalidOperationException("Failed to load profile");
        }

        ClearCookies();

        foreach (var cookie in CurrentProfileSecret.Cookies)
        {
            CookieContainer.Add(cookie);
        }

        ProfileLoaded?.Invoke(this, CurrentProfile);
        logger.LogInformation("Profile {ProfileId} loaded", profileId);
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

    #endregion

    public async Task UpdateAsync(string displayName, string avatarUrl)
    {
        if (!IsProfileLoaded)
            throw new InvalidOperationException("No profile loaded");

        CurrentProfile.DisplayName = displayName;
        CurrentProfile.AvatarUrl = avatarUrl;

        await SaveProfileAsync();
    }

    public string? GetAuthCookie()
    {
        var cookies = CookieContainer.GetCookies(new Uri("https://api.vrchat.cloud"));

        var authCookie = cookies["auth"];

        return authCookie?.Value;
    }

    internal void ClearCookies()
    {
        foreach (Cookie cookie in CookieContainer.GetAllCookies())
        {
            cookie.Expired = true;
        }
    }
}
