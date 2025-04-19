using System.ComponentModel.DataAnnotations;
using VRCZ.Core.Models.Entities.VRChat;

namespace VRCZ.Core.Models.Entities.LocalFavorites;

public class AvatarFavoritesFolder : BaseEntity
{
    [Required] [MinLength(1)] public required string Name { get; set; }
    [Required] public required string Description { get; set; }

    public List<Avatar> Avatars { get; set; } = [];
}
