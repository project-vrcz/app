using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using Microsoft.Kiota.Abstractions;

namespace VRCZ.Core.Models.VRChat.WebSocket;

[ExcludeFromCodeCoverage]
public class VRChatWebSocketCurrentUser
{
    [JsonPropertyName("id")] public string? Id { get; set; }
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
    [JsonPropertyName("username")] public string? Username { get; set; }
    [JsonPropertyName("pastDisplayNames")] public object[]? PastDisplayNames { get; set; }
    [JsonPropertyName("hasEmail")] public bool? HasEmail { get; set; }
    [JsonPropertyName("hasPendingEmail")] public bool? HasPendingEmail { get; set; }
    [JsonPropertyName("obfuscatedEmail")] public string? ObfuscatedEmail { get; set; }

    [JsonPropertyName("obfuscatedPendingEmail")]
    public string? ObfuscatedPendingEmail { get; set; }

    [JsonPropertyName("emailVerified")] public bool? EmailVerified { get; set; }
    [JsonPropertyName("hasBirthday")] public bool? HasBirthday { get; set; }
    [JsonPropertyName("isAdult")] public bool? IsAdult { get; set; }

    [JsonPropertyName("hideContentFilterSettings")]
    public bool? HideContentFilterSettings { get; set; }

    [JsonPropertyName("unsubscribe")] public bool? Unsubscribe { get; set; }
    [JsonPropertyName("statusHistory")] public string[]? StatusHistory { get; set; }
    [JsonPropertyName("statusFirstTime")] public bool? StatusFirstTime { get; set; }
    [JsonPropertyName("friends")] public string[]? Friends { get; set; }
    [JsonPropertyName("friendGroupNames")] public object[]? FriendGroupNames { get; set; }
    [JsonPropertyName("userLanguage")] public string? UserLanguage { get; set; }
    [JsonPropertyName("userLanguageCode")] public string? UserLanguageCode { get; set; }

    [JsonPropertyName("receiveMobileInvitations")]
    public bool? ReceiveMobileInvitations { get; set; }

    [JsonPropertyName("currentAvatarImageUrl")]
    public string? CurrentAvatarImageUrl { get; set; }

    [JsonPropertyName("currentAvatarThumbnailImageUrl")]
    public string? CurrentAvatarThumbnailImageUrl { get; set; }

    [JsonPropertyName("currentAvatarTags")]
    public object[]? CurrentAvatarTags { get; set; }

    [JsonPropertyName("currentAvatar")] public string? CurrentAvatar { get; set; }

    [JsonPropertyName("currentAvatarAssetUrl")]
    public string? CurrentAvatarAssetUrl { get; set; }

    [JsonPropertyName("fallbackAvatar")] public string? FallbackAvatar { get; set; }

    [JsonPropertyName("accountDeletionDate")]
    public Date AccountDeletionDate { get; set; }

    [JsonPropertyName("accountDeletionLog")]
    public object AccountDeletionLog { get; set; }

    [JsonPropertyName("acceptedTOSVersion")]
    public long? AcceptedTosVersion { get; set; }

    [JsonPropertyName("acceptedPrivacyVersion")]
    public long? AcceptedPrivacyVersion { get; set; }

    [JsonPropertyName("steamId")] public string? SteamId { get; set; }
    [JsonPropertyName("googleId")] public string? GoogleId { get; set; }
    [JsonPropertyName("oculusId")] public string? OculusId { get; set; }
    [JsonPropertyName("picoId")] public string? PicoId { get; set; }
    [JsonPropertyName("viveId")] public string? ViveId { get; set; }

    [JsonPropertyName("hasLoggedInFromClient")]
    public bool? HasLoggedInFromClient { get; set; }

    [JsonPropertyName("homeLocation")] public string? HomeLocation { get; set; }

    [JsonPropertyName("twoFactorAuthEnabled")]
    public bool? TwoFactorAuthEnabled { get; set; }

    [JsonPropertyName("twoFactorAuthEnabledDate")]
    public DateTimeOffset? TwoFactorAuthEnabledDate { get; set; }

    [JsonPropertyName("updated_at")] public DateTimeOffset? UpdatedAt { get; set; }
    [JsonPropertyName("isBoopingEnabled")] public bool? IsBoopingEnabled { get; set; }
    [JsonPropertyName("platform_history")] public string[]? PlatformHistory { get; set; }
    [JsonPropertyName("state")] public string? State { get; set; }
    [JsonPropertyName("last_mobile")] public DateTimeOffset? LastMobile { get; set; }
    [JsonPropertyName("tags")] public string[]? Tags { get; set; }
    [JsonPropertyName("developerType")] public string? DeveloperType { get; set; }
    [JsonPropertyName("last_login")] public DateTimeOffset? LastLogin { get; set; }
    [JsonPropertyName("last_platform")] public string? LastPlatform { get; set; }

    [JsonPropertyName("allowAvatarCopying")]
    public bool? AllowAvatarCopying { get; set; }

    [JsonPropertyName("status")] public string? Status { get; set; }
    [JsonPropertyName("date_joined")] public DateTimeOffset? DateJoined { get; set; }
    [JsonPropertyName("isFriend")] public bool? IsFriend { get; set; }
    [JsonPropertyName("friendKey")] public string? FriendKey { get; set; }
    [JsonPropertyName("last_activity")] public DateTimeOffset? LastActivity { get; set; }
}
