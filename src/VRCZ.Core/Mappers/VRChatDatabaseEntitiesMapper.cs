using Riok.Mapperly.Abstractions;
using VRCZ.VRChatApi.Generated.Models;
using VRCZ.Core.Models.Entities.VRChat;

namespace VRCZ.Core.Mappers;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.None, AllowNullPropertyAssignment = true)]
public static partial class VRChatDatabaseEntitiesMapper
{
    [MapProperty(nameof(Avatar.CreatedAt), nameof(AvatarEntity.AvatarCreatedAt))]
    [MapProperty(nameof(Avatar.UpdatedAt), nameof(AvatarEntity.AvatarUpdatedAt))]
    [MapProperty(nameof(Avatar.Styles.Primary), nameof(AvatarEntity.PrimaryStyle))]
    [MapProperty(nameof(Avatar.Styles.Secondary), nameof(AvatarEntity.SecondaryStyle))]
    public static partial AvatarEntity MapToAvatarEntity(Avatar avatarEntity);

    [MapProperty(nameof(AvatarEntity.AvatarCreatedAt), nameof(Avatar.CreatedAt))]
    [MapProperty(nameof(AvatarEntity.AvatarUpdatedAt), nameof(Avatar.UpdatedAt))]
    [MapProperty(nameof(AvatarEntity.PrimaryStyle), nameof(Avatar.Styles.Primary))]
    [MapProperty(nameof(AvatarEntity.SecondaryStyle), nameof(Avatar.Styles.Secondary))]
    public static partial Avatar MapToAvatar(AvatarEntity avatarEntity);
}
