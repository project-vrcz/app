using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace VRCZ.Core.Models.VRChat.WebSocket;

[ExcludeFromCodeCoverage]
public class VRChatWebSocketFriendUser
{
    [JsonPropertyName("id")] public required string? Id { get; set; }
    [JsonPropertyName("displayName")] public string? DisplayName { get; set; }
    [JsonPropertyName("userIcon")] public string? UserIcon { get; set; }
    [JsonPropertyName("bio")] public string? Bio { get; set; }
    [JsonPropertyName("bioLinks")] public string[]? BioLinks { get; set; }
    [JsonPropertyName("profilePicOverride")]
    public string? ProfilePicOverride { get; set; }
    [JsonPropertyName("profilePicOverrideThumbnail")]
    public string? ProfilePicOverrideThumbnail { get; set; }
    [JsonPropertyName("statusDescription")]
    public string? StatusDescription { get; set; }
    [JsonPropertyName("pronouns")] public string? Pronouns { get; set; }
    [JsonPropertyName("ageVerificationStatus")]
    public string? AgeVerificationStatus { get; set; }
    [JsonPropertyName("ageVerified")] public bool? AgeVerified { get; set; }
    [JsonPropertyName("currentAvatarImageUrl")]
    public string? CurrentAvatarImageUrl { get; set; }
    [JsonPropertyName("currentAvatarThumbnailImageUrl")]
    public string? CurrentAvatarThumbnailImageUrl { get; set; }
    [JsonPropertyName("currentAvatarTags")]
    public string[]? CurrentAvatarTags { get; set; }
    [JsonPropertyName("state")] public string? State { get; set; }
    [JsonPropertyName("last_mobile")] public DateTimeOffset? LastMobile { get; set; }
    [JsonPropertyName("tags")] public string[] Tags { get; set; }
    [JsonPropertyName("developerType")] public string? DeveloperType { get; set; }
    [JsonPropertyName("last_login")] public DateTimeOffset? LastLogin { get; set; }
    [JsonPropertyName("last_platform")] public string? LastPlatform { get; set; }
    [JsonPropertyName("allowAvatarCopying")]
    public bool? AllowAvatarCopying { get; set; }
    [JsonPropertyName("status")] public string? Status { get; set; }
    [JsonPropertyName("date_joined")] public DateOnly? DateJoined { get; set; }
    [JsonPropertyName("isFriend")] public bool? IsFriend { get; set; }
    [JsonPropertyName("friendKey")] public string? FriendKey { get; set; }
    [JsonPropertyName("last_activity")] public DateTimeOffset? LastActivity { get; set; }
}
