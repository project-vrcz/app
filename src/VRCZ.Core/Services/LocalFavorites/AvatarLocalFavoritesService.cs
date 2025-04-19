using Microsoft.EntityFrameworkCore;
using VRCZ.Core.DbContexts;
using VRCZ.Core.Models.Entities.LocalFavorites;
using VRCZ.Core.Models.Entities.VRChat;

namespace VRCZ.Core.Services.LocalFavorites;

public class AvatarLocalFavoritesService(AppDbContext appDbContext)
{
    public async ValueTask<AvatarFavoritesFolder[]> GetAvatarFavoritesFoldersAsync()
    {
        return await appDbContext.AvatarFavoritesFolders
            .AsNoTracking()
            .ToArrayAsync();
    }

    public async ValueTask<AvatarFavoritesFolder?> GetAvatarFavoritesFolderAsync(Guid folderId)
    {
        return await appDbContext.AvatarFavoritesFolders
            .AsNoTracking()
            .Include(folder => folder.Avatars)
            .FirstOrDefaultAsync(f => f.Id == folderId);
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

    public async Task AddAvatarToFavoritesAsync(AvatarEntity avatarEntity, Guid folderId)
    {
        var folder = await appDbContext.AvatarFavoritesFolders.FindAsync(folderId)
            ?? throw new InvalidOperationException("Folder not found");

        folder.Avatars.Add(avatarEntity);

        await appDbContext.SaveChangesAsync();
    }
}
