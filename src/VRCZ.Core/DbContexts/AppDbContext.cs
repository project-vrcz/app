using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using VRCZ.Core.CompiledModels;
using VRCZ.Core.Services.Database;

namespace VRCZ.Core.DbContexts;

[DbContext(typeof(AppDbContext))]
public class AppDbContext(DbContextOptions<AppDbContext> options,
    IConnectionStringProvider connectionStringProvider) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = connectionStringProvider.GetConnectionString();

        optionsBuilder
            .UseModel(AppDbContextModel.Instance)
            .UseSqlite(connectionString);
    }
}
