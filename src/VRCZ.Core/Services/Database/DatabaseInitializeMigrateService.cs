using VRCZ.Core.DbContexts;

namespace VRCZ.Core.Services.Database;

public class DatabaseInitializeMigrateService(AppDbContext appDbContext)
{
    public async Task EnsureDatabaseReadyAsync()
    {
#if DEBUG
        await appDbContext.Database.EnsureCreatedAsync();
#endif

        // TODO: Use a aot-supported way to migrate database
        // await appDbContext.Database.MigrateAsync();
    }
}
