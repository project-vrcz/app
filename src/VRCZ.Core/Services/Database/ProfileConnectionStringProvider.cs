using Microsoft.Data.Sqlite;
using VRCZ.Core.Services.Profile;
using VRCZ.Core.Utils;

namespace VRCZ.Core.Services.Database;

public class ProfileConnectionStringProvider(CurrentUserProfileService userProfileService) : IConnectionStringProvider
{
    public string GetConnectionString()
    {
        if (!userProfileService.IsProfileLoaded)
            throw new InvalidOperationException("User profile is not loaded.");

        var profileStoragePath = ProfileStorageUtils.GetUserProfileStoragePath(userProfileService.CurrentProfile.Id);
        var databasePath = Path.Combine(profileStoragePath, ProfileStorageUtils.UserProfileDatabaseFileName);

        var connectionStringBuilder = new SqliteConnectionStringBuilder
        {
            DataSource = databasePath
        };

        return connectionStringBuilder.ToString();
    }
}
