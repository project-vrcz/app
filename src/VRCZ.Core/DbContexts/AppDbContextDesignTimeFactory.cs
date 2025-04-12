using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using VRCZ.Core.Services.Database;

namespace VRCZ.Core.DbContexts;

public class AppDbContextDesignTimeFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        return new AppDbContext(new DbContextOptions<AppDbContext>(), new MockConnectionStringProvider());
    }
}
