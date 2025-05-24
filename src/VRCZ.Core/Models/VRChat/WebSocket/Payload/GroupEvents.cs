using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using VRCZ.VRChatApi.Generated.Models;

namespace VRCZ.Core.Models.VRChat.WebSocket.Payload;

[ExcludeFromCodeCoverage]
public record GroupJoinedEvent(
    [property: JsonPropertyName("groupId")]
    string GroupId
) : VRChatWebSocketPayloadBase;

[ExcludeFromCodeCoverage]
public record GroupLeftEvent(
    [property: JsonPropertyName("groupId")]
    string GroupId
) : VRChatWebSocketPayloadBase;

[ExcludeFromCodeCoverage]
public record GroupMemberUpdateEvent(
    [property: JsonPropertyName("member")] GroupLimitedMember[] Members
) : VRChatWebSocketPayloadBase;

[ExcludeFromCodeCoverage]
public record GroupRoleUpdateEvent(
    [property: JsonPropertyName("role")] GroupRole[] Roles
) : VRChatWebSocketPayloadBase;
