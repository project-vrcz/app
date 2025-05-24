using System.Diagnostics.CodeAnalysis;
using VRCZ.VRChatApi.Generated.Models;

namespace VRCZ.Core.Models.VRChat.TrackedEntities;

[ExcludeFromCodeCoverage]
public class TrackedVRChatUser
{
    public AgeVerificationStatus? AgeVerificationStatus { get; set; }
    public bool? AgeVerified { get; set; }
    public bool? AllowAvatarCopying { get; set; }
    public Badge[]? Badges { get; set; }
    public string? Bio { get; set; }
    public string[]? BioLinks { get; set; }
    public string? CurrentAvatarImageUrl { get; set; }
    public string[]? CurrentAvatarTags { get; set; }
    public string? CurrentAvatarThumbnailImageUrl { get; set; }
    public DateOnly? DateJoined { get; set; }
    public string? DeveloperType { get; set; }
    public string? DisplayName { get; set; }
    public string? FriendKey { get; set; }
    public string? FriendRequestStatus { get; set; }
    public required string Id { get; set; }
    public string? InstanceId { get; set; }
    public bool? IsFriend { get; set; }
    public DateTimeOffset? LastActivity { get; set; }
    public DateTimeOffset? LastLogin { get; set; }
    public DateTimeOffset? LastMobile { get; set; }
    public string? LastPlatform { get; set; }
    public string? Location { get; set; }
    public string? Note { get; set; }
    public string? Platform { get; set; }
    public string? ProfilePicOverride { get; set; }
    public string? ProfilePicOverrideThumbnail { get; set; }
    public string? Pronouns { get; set; }
    public string? State { get; set; }
    public string? Status { get; set; }
    public string? StatusDescription { get; set; }
    public string[]? Tags { get; set; }
    public string? TravelingToInstance { get; set; }
    public string? TravelingToLocation { get; set; }
    public string? TravelingToWorld { get; set; }
    public string? UserIcon { get; set; }
    public string? WorldId { get; set; }
}
