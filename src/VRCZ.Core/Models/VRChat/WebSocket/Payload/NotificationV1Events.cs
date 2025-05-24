using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using VRCZ.VRChatApi.Generated.Models;

namespace VRCZ.Core.Models.VRChat.WebSocket.Payload;

[ExcludeFromCodeCoverage]
public record NotificationEvent(
    Notification Notification
) : VRChatWebSocketPayloadBase;

[ExcludeFromCodeCoverage]
public record ResponseNotificationEvent(
    [property: JsonPropertyName("notificationId")]
    string NotificationId,
    [property: JsonPropertyName("receiverId")]
    string ReceiverId,
    [property: JsonPropertyName("responseId")]
    string ResponseId
) : VRChatWebSocketPayloadBase;

[ExcludeFromCodeCoverage]
public record SeeNotificationEvent(
    string NotificationId
) : VRChatWebSocketPayloadBase;

[ExcludeFromCodeCoverage]
public record HideNotificationEvent(
    string NotificationId
) : VRChatWebSocketPayloadBase;

[ExcludeFromCodeCoverage]
public record ClearNotificationEvent : VRChatWebSocketPayloadBase;

[ExcludeFromCodeCoverage]
public record WebSocketNotificationV1<T>(
    [property: JsonPropertyName("created_at")]
    DateTimeOffset CreatedAt,
    [property: JsonPropertyName("details")]
    T Details,
    [property: JsonPropertyName("message")]
    string Message,
    [property: JsonPropertyName("receiverUserId")]
    string? ReceiverUserId,
    [property: JsonPropertyName("senderUserId")]
    string SenderUserId,
    [property: JsonPropertyName("type")] string Type
) : VRChatWebSocketPayloadBase;

[ExcludeFromCodeCoverage]
public record WebSocketNotificationV1InviteDetail(
    [property: JsonPropertyName("worldId")]
    string WorldId,
    [property: JsonPropertyName("worldName")]
    string WorldName,
    [property: JsonPropertyName("inviteMessage")]
    string InviteMessage
);

[ExcludeFromCodeCoverage]
public record WebSocketNotificationV1InviteResponseDetail(
    [property: JsonPropertyName("inResponseTo")] string ResponseToNotificationId,
    [property: JsonPropertyName("responseMessage")] string ResponseMessage
);

[ExcludeFromCodeCoverage]
public record WebSocketNotificationV1RequestInviteDetail(
    [property: JsonPropertyName("requestMessage")]
    string RequestMessage
);

[ExcludeFromCodeCoverage]
public record WebSocketNotificationV1RequestInviteResponseDetail(
    [property: JsonPropertyName("inResponseTo")] string ResponseToNotificationId,
    [property: JsonPropertyName("requestMessage")] string RequestMessage
);

[ExcludeFromCodeCoverage]
public record WebSocketNotificationV1VoteToKickDetail(
    [property: JsonPropertyName("initiatorUserId")] string InitiatorUserId,
    [property: JsonPropertyName("userToKickId")] string TargetUserId
);
