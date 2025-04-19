using Microsoft.EntityFrameworkCore;
using VRCZ.Core.DbContexts;
using VRCZ.Core.Models.Entities.LocalFavorites;

namespace VRCZ.Core.Services.LocalFavorites;

public class AvatarLocalFavoritesService(AppDbContext appDbContext)
{
    public async ValueTask<AvatarFavoritesFolder[]> GetAvatarFavoritesFoldersAsync()
    {
        return await appDbContext.AvatarFavoritesFolders
            .AsNoTracking()
            .ToArrayAsync();
    }

    public async ValueTask<AvatarFavoritesFolder> CreateAvatarFavoritesFolderAsync(string name, string description)
    {
        var folder = new AvatarFavoritesFolder
        {
            Name = name,
            Description = description
        };

        await appDbContext.AvatarFavoritesFolders.AddAsync(folder);
        await appDbContext.SaveChangesAsync();

        return folder;
    }
}
