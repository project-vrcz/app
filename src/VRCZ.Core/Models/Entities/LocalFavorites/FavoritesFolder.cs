﻿using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using VRCZ.Core.Models.Entities.VRChat;

namespace VRCZ.Core.Models.Entities.LocalFavorites;

[ExcludeFromCodeCoverage]
public class AvatarFavoritesFolder : BaseEntity
{
    [Required] [MinLength(1)] public required string Name { get; set; }
    [Required] public required string Description { get; set; }

    public List<AvatarEntity> Avatars { get; set; } = [];
}
