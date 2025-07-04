﻿using System.Net;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using VRCZ.Core.Exceptions;
using VRCZ.Core.Models;
using VRCZ.Core.Services.Database;
using VRCZ.Core.Utils;

namespace VRCZ.Core.Services.Profile;

public class UserProfileService
{
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

    public async Task CreateProfileAsync(string profileId, string username, string displayName, string avatarUrl,
        string password, Cookie[] cookies)
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
            Cookies = cookies.ToList(),
            Username = username,
            Password = password
        };

        await File.WriteAllTextAsync(profileMetaDataPath,
            JsonSerializer.Serialize(profile, UserProfileContext.Default.UserProfile));
        await File.WriteAllTextAsync(profileSecretPath,
            JsonSerializer.Serialize(secret, UserProfileContext.Default.UserProfileSecret));
    }
}
