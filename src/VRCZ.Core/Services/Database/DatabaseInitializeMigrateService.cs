using SqlSugar;
using VRCZ.Core.Models.Entities.LocalFavorites;
using VRCZ.Core.Models.Entities.VRChat;

namespace VRCZ.Core.Services.Database;

public class DatabaseInitializeMigrateService(SqlSugarClient sqlSugarClient)
{
    public async Task EnsureDatabaseReadyAsync()
    {
        await Task.Run(() =>
        {
            sqlSugarClient.DbMaintenance.CreateDatabase();

            sqlSugarClient.CodeFirst.InitTables<AvatarEntity>();

            sqlSugarClient.CodeFirst.InitTables<AvatarFavoritesFolder>();
            sqlSugarClient.CodeFirst.InitTables<AvatarFavoritesMapping>();
        });
    }
}
