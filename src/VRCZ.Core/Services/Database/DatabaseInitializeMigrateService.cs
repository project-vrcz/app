using Microsoft.EntityFrameworkCore;
using VRCZ.Core.DbContexts;

namespace VRCZ.Core.Services.Database;

public class DatabaseInitializeMigrateService(AppDbContext appDbContext)
{
    public async Task EnsureDatabaseReadyAsync()
    {
        // TODO: Use a aot-supported way to migrate database
        await appDbContext.Database.MigrateAsync();
    }
}
