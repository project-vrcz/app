using SqlSugar;
using VRCZ.Core.Models.Entities.LocalFavorites;
using VRCZ.Core.Models.Entities.VRChat;

namespace VRCZ.Core.Services.LocalFavorites;

public class AvatarLocalFavoritesService(SqlSugarClient sqlSugarClient)
{
    public async ValueTask<AvatarFavoritesFolder[]> GetAvatarFavoritesFoldersAsync()
    {
        return await sqlSugarClient
            .Queryable<AvatarFavoritesFolder>()
            .ToArrayAsync();
    }

    public async ValueTask<AvatarFavoritesFolder?> GetAvatarFavoritesFolderAsync(Guid folderId)
    {
        return await sqlSugarClient
            .Queryable<AvatarFavoritesFolder>()
            .Includes(folder => folder.Avatars)
            .SingleAsync(folder => folder.Id == folderId);
    }

    public async ValueTask<AvatarFavoritesFolder> CreateAvatarFavoritesFolderAsync(string name, string description)
    {
        var folder = new AvatarFavoritesFolder
        {
            Name = name,
            Description = description
        };

        await sqlSugarClient.Insertable(folder).ExecuteCommandAsync();

        return folder;
    }

    public async Task AddAvatarToFavoritesAsync(AvatarEntity avatarEntity, Guid folderId)
    {
        if (!await sqlSugarClient.Queryable<AvatarFavoritesFolder>().AnyAsync(entity => entity.Id == folderId))
            throw new InvalidOperationException("Folder not found");

        var avatar = await sqlSugarClient.Storageable(avatarEntity).ExecuteReturnEntityAsync();
        var mapping = new AvatarFavoritesMapping
        {
            AvatarEntityId = avatar.Id,
            AvatarFavoritesFolderId = folderId
        };

        await sqlSugarClient.Insertable(mapping).ExecuteCommandAsync();
    }
}
