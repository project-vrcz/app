using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using SqlSugar;
using VRCZ.Core.Models.Entities.VRChat;
#pragma warning disable CS8618

namespace VRCZ.Core.Models.Entities.LocalFavorites;

[ExcludeFromCodeCoverage]
[SugarTable]
public class AvatarFavoritesFolder : BaseEntity
{
    [Required] [MinLength(1)] public string Name { get; set; }
    [Required] public string Description { get; set; }

    [Navigate(typeof(AvatarFavoritesMapping),
        nameof(AvatarFavoritesMapping.AvatarFavoritesFolderId),
        nameof(AvatarFavoritesMapping.AvatarEntityId))]
    public List<AvatarEntity> Avatars { get; set; }
}

[SugarTable]
public class AvatarFavoritesMapping
{
    [SugarColumn(IsPrimaryKey = true)]
    public Guid AvatarFavoritesFolderId { get; set; }

    [SugarColumn(IsPrimaryKey = true)]
    public string AvatarEntityId { get; set; }
}
