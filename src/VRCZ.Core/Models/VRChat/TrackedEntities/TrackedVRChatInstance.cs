using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace VRCZ.Core.Models.VRChat.TrackedEntities;

[ExcludeFromCodeCoverage]
public class TrackedVRChatInstance
{
    [JsonPropertyName("active")] public bool? Active { get; set; }
    [JsonPropertyName("ageGate")] public bool? AgeGate { get; set; }
    [JsonPropertyName("canRequestInvite")] public bool? CanRequestInvite { get; set; }
    [JsonPropertyName("capacity")] public long? Capacity { get; set; }
    [JsonPropertyName("displayName")] public string? DisplayName { get; set; }
    [JsonPropertyName("full")] public bool? Full { get; set; }
    [JsonPropertyName("gameServerVersion")]
    public long? GameServerVersion { get; set; }
    [JsonPropertyName("id")] public string? Id { get; set; }
    [JsonPropertyName("instanceId")] public string? InstanceId { get; set; }
    [JsonPropertyName("instancePersistenceEnabled")]
    public string? InstancePersistenceEnabled { get; set; }
    [JsonPropertyName("location")] public string? Location { get; set; }
    [JsonPropertyName("n_users")] public long? NUsers { get; set; }
    [JsonPropertyName("name")] public string? Name { get; set; }
    [JsonPropertyName("ownerId")] public string? OwnerId { get; set; }
    [JsonPropertyName("permanent")] public bool? Permanent { get; set; }
    [JsonPropertyName("photonRegion")] public string? PhotonRegion { get; set; }
    [JsonPropertyName("platforms")] public PlatformPlayerCount? PlatformPlayerCount { get; set; }
    [JsonPropertyName("playerPersistenceEnabled")]
    public bool? PlayerPersistenceEnabled { get; set; }
    [JsonPropertyName("region")] public string? Region { get; set; }
    [JsonPropertyName("secureName")] public string? SecureName { get; set; }
    [JsonPropertyName("shortName")] public string? ShortName { get; set; }
    [JsonPropertyName("tags")] public string[]? Tags { get; set; }
    [JsonPropertyName("type")] public string? Type { get; set; }
    [JsonPropertyName("worldId")] public string? WorldId { get; set; }
    [JsonPropertyName("hidden")] public string? Hidden { get; set; }
    [JsonPropertyName("friends")] public string? Friends { get; set; }
    [JsonPropertyName("private")] public string? Private { get; set; }
    [JsonPropertyName("queueEnabled")] public bool? QueueEnabled { get; set; }
    [JsonPropertyName("queueSize")] public long? QueueSize { get; set; }
    [JsonPropertyName("recommendedCapacity")]
    public long? RecommendedCapacity { get; set; }
    [JsonPropertyName("roleRestricted")] public bool? RoleRestricted { get; set; }
    [JsonPropertyName("strict")] public bool? Strict { get; set; }
    [JsonPropertyName("userCount")] public long? UserCount { get; set; }
    [JsonPropertyName("groupAccessType")] public string? GroupAccessType { get; set; }
    [JsonPropertyName("hasCapacityForYou")]
    public bool? HasCapacityForYou { get; set; }
    [JsonPropertyName("nonce")] public string? Nonce { get; set; }
    [JsonPropertyName("closedAt")] public DateTimeOffset? ClosedAt { get; set; }
    [JsonPropertyName("hardClose")] public bool? HardClose { get; set; }
}

[ExcludeFromCodeCoverage]
public class PlatformPlayerCount
{
    [JsonPropertyName("android")] public long? Android { get; set; }
    [JsonPropertyName("ios")] public long? Ios { get; set; }
    [JsonPropertyName("standalonewindows")]
    public long? StandaloneWindows { get; set; }
}
