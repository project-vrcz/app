using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using VRCZ.VRChatApi.Generated.Models;

namespace VRCZ.Core.Models.VRChat.WebSocket.Payload;

[ExcludeFromCodeCoverage]
public record FriendAddEvent(
    [property: JsonPropertyName("userId")] string UserId
) : VRChatWebSocketPayloadBase, IVRChatWebSocketFriendUserPayload
{
    [JsonIgnore] public VRChatWebSocketFriendUser? User { get; set; }
}

[ExcludeFromCodeCoverage]
public record FriendDeleteEvent(
    [property: JsonPropertyName("userId")] string UserId
) : VRChatWebSocketPayloadBase;

[ExcludeFromCodeCoverage]
public record FriendOnlineEvent(
    [property: JsonPropertyName("userId")] string UserId,
    [property: JsonPropertyName("platform")]
    string Platform,
    [property: JsonPropertyName("location")]
    string Location,
    [property: JsonPropertyName("travelingToLocation")]
    string TravelingToLocation,
    [property: JsonPropertyName("canRequestInvite")]
    bool CanRequestInvite,
    [property: JsonPropertyName("worldId")]
    string WorldId,
    [property: JsonPropertyName("world")] VRChatWebSocketWorld? World = null
) : VRChatWebSocketPayloadBase, IVRChatWebSocketFriendUserPayload, IVRChatWebSocketWorldPayload,
    IVRChatWebSocketLocationPayload
{
    [JsonIgnore] public VRChatWebSocketFriendUser? User { get; set; }
}

[ExcludeFromCodeCoverage]
public record FriendActiveEvent(
    [property: JsonPropertyName("userId")] string UserId,
    [property: JsonPropertyName("platform")]
    string Platform
) : VRChatWebSocketPayloadBase, IVRChatWebSocketFriendUserPayload
{
    [JsonIgnore] public VRChatWebSocketFriendUser? User { get; set; }
}

[ExcludeFromCodeCoverage]
public record FriendOfflineEvent(
    [property: JsonPropertyName("userId")] string UserId
) : VRChatWebSocketPayloadBase;

[ExcludeFromCodeCoverage]
public record FriendUpdateEvent(
    [property: JsonPropertyName("userId")] string UserId
) : VRChatWebSocketPayloadBase, IVRChatWebSocketFriendUserPayload
{
    [JsonIgnore] public VRChatWebSocketFriendUser? User { get; set; }
}

[ExcludeFromCodeCoverage]
public record FriendLocationEvent(
    [property: JsonPropertyName("userId")] string UserId,
    [property: JsonPropertyName("location")]
    string Location,
    [property: JsonPropertyName("travelingToLocation")]
    string TravelingToLocation,
    [property: JsonPropertyName("canRequestInvite")]
    bool CanRequestInvite,
    [property: JsonPropertyName("platform")]
    string Platform,
    [property: JsonPropertyName("worldId")]
    string WorldId,
    [property: JsonPropertyName("world")] VRChatWebSocketWorld? World = null
) : VRChatWebSocketPayloadBase, IVRChatWebSocketFriendUserPayload, IVRChatWebSocketWorldPayload,
    IVRChatWebSocketLocationPayload
{
    [JsonIgnore] public VRChatWebSocketFriendUser? User { get; set; }
}
