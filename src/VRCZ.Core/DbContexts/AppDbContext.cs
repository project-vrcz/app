using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VRCZ.Core.CompiledModels;
using VRCZ.Core.Models.Entities;
using VRCZ.Core.Models.Entities.LocalFavorites;
using VRCZ.Core.Models.Entities.VRChat;
using VRCZ.Core.Services.Database;

namespace VRCZ.Core.DbContexts;

[DbContext(typeof(AppDbContext))]
public class AppDbContext(
    DbContextOptions<AppDbContext> options,
    IConnectionStringProvider connectionStringProvider) : DbContext(options)
{
    public DbSet<AvatarEntity> Avatars { get; set; }

    public DbSet<AvatarFavoritesFolder> AvatarFavoritesFolders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = connectionStringProvider.GetConnectionString();

        // dotnet ef dbcontext optimize --output-dir CompiledModels --namespace VRCZ.Core.CompiledModels
        optionsBuilder
            .UseModel(AppDbContextModel.Instance)
            .UseSqlite(connectionString);
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        UpdateTrackedEntitiesTimestamp();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
    {
        UpdateTrackedEntitiesTimestamp();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void UpdateTrackedEntitiesTimestamp()
    {
        var modifiedEntities = ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Modified);

        var now = DateTimeOffset.Now;

        foreach (var entity in modifiedEntities)
        {
            entity.Property(nameof(BaseEntity.UpdatedAt)).CurrentValue = now;
        }

        var createdEntities = ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Modified);

        foreach (var entity in createdEntities)
        {
            entity.Property(nameof(BaseEntity.CreatedAt)).CurrentValue = now;
        }
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // https://gist.github.com/GeorgDangl/b90370124720ed8fed9539509aafd155#file-databasecontext-cs
        if (Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
        {
            // SQLite does not have proper support for DateTimeOffset via Entity Framework Core, see the limitations
            // here: https://docs.microsoft.com/en-us/ef/core/providers/sqlite/limitations#query-limitations
            // To work around this, when the Sqlite database provider is used, all model properties of type DateTimeOffset
            // use the DateTimeOffsetToBinaryConverter
            // Based on: https://github.com/aspnet/EntityFrameworkCore/issues/10784#issuecomment-415769754
            // This only supports millisecond precision, but should be sufficient for most use cases.
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                var properties = entityType.ClrType
                    .GetProperties()
                    .Where(p => p.PropertyType == typeof(DateTime) || p.PropertyType == typeof(DateTime?));
                foreach (var property in properties)
                {
                    builder
                        .Entity(entityType.Name)
                        .Property(property.Name)
                        .HasConversion(new DateTimeOffsetToBinaryConverter());
                }
            }
        }

        builder.Entity<AvatarEntity>()
            .ToTable("Avatars");

        builder.Entity<AvatarFavoritesFolder>()
            .ToTable("AvatarFavoritesFolders")
            .HasMany(e => e.Avatars)
            .WithMany();
    }
}
