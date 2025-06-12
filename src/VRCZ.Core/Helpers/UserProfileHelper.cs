using System.Text.Json;
using VRCZ.Core.Models;

namespace VRCZ.Core.Helpers;

public static class UserProfileHelper
{
    public static async ValueTask<(UserProfile Profile, UserProfileSecret ProfileSecret)> ParseUserProfileAsync(
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
}
