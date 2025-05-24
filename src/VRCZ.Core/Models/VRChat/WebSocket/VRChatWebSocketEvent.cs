using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using VRCZ.Core.Models.VRChat.WebSocket.Payload;
using VRCZ.VRChatApi.Generated.Models;

namespace VRCZ.Core.Models.VRChat.WebSocket;

[ExcludeFromCodeCoverage]
public record VRChatWebSocketEvent(
    [property: JsonPropertyName("type")] string Type,
    [property: JsonPropertyName("content")]
    string? Content);

[ExcludeFromCodeCoverage]
public record VRChatWebSocketErrorEvent(
    [property: JsonPropertyName("err")] string ErrorMessage
);

[ExcludeFromCodeCoverage]
public abstract record VRChatWebSocketPayloadBase;

public interface IVRChatCurrentUserPayload
{
    public CurrentUser? User { get; set; }
}

public interface IVRChatWebSocketFriendUserPayload
{
    public VRChatWebSocketFriendUser? User { get; set; }
}

public interface IVRChatWebSocketWorldPayload
{
    public VRChatWebSocketWorld? World { get; }
}

public interface IVRChatWebSocketLocationPayload
{
    public string Location { get; init; }
    public string TravelingToLocation { get; }
}

[JsonSerializable(typeof(VRChatWebSocketEvent))]
[JsonSerializable(typeof(VRChatWebSocketErrorEvent))]
[JsonSerializable(typeof(VRChatWebSocketFriendUser))]
[JsonSerializable(typeof(FriendAddEvent)),
 JsonSerializable(typeof(FriendDeleteEvent)),
 JsonSerializable(typeof(FriendOnlineEvent)),
 JsonSerializable(typeof(FriendActiveEvent)),
 JsonSerializable(typeof(FriendOfflineEvent)),
 JsonSerializable(typeof(FriendUpdateEvent)),
 JsonSerializable(typeof(FriendLocationEvent)),
 JsonSerializable(typeof(GroupJoinedEvent)),
 JsonSerializable(typeof(GroupLeftEvent)),
 JsonSerializable(typeof(GroupMemberUpdateEvent)),
 JsonSerializable(typeof(GroupRoleUpdateEvent)),
 JsonSerializable(typeof(InstanceQueueJoinedEvent)),
 JsonSerializable(typeof(InstanceQueueReadyEvent)),
 JsonSerializable(typeof(WebSocketNotificationV1<WebSocketNotificationV1InviteDetail>)),
 JsonSerializable(typeof(WebSocketNotificationV1<WebSocketNotificationV1InviteResponseDetail>)),
 JsonSerializable(typeof(WebSocketNotificationV1<WebSocketNotificationV1RequestInviteDetail>)),
 JsonSerializable(typeof(WebSocketNotificationV1<WebSocketNotificationV1RequestInviteResponseDetail>)),
 JsonSerializable(typeof(WebSocketNotificationV1<WebSocketNotificationV1VoteToKickDetail>)),
 JsonSerializable(typeof(WebSocketNotificationV1<object>)),
 JsonSerializable(typeof(NotificationEvent)),
 JsonSerializable(typeof(ResponseNotificationEvent)),
 JsonSerializable(typeof(UserUpdateEvent)),
 JsonSerializable(typeof(UserLocationEvent)),
 JsonSerializable(typeof(UserBadgeAssignedEvent)),
 JsonSerializable(typeof(UserBadgeUnassignedEvent)),
 JsonSerializable(typeof(ContentRefreshEvent))
]
[JsonSourceGenerationOptions(RespectRequiredConstructorParameters = true, RespectNullableAnnotations = true, PropertyNamingPolicy = JsonKnownNamingPolicy.SnakeCaseLower)]
public partial class VRChatWebSocketEventContext : JsonSerializerContext;
