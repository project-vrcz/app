using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace VRCZ.Core.Models.VRChat.WebSocket.Payload;

[ExcludeFromCodeCoverage]
public record InstanceQueueJoinedEvent(
    [property: JsonPropertyName("instanceLocation")]
    string InstanceLocation,
    [property: JsonPropertyName("position")]
    int Position
) : VRChatWebSocketPayloadBase;

[ExcludeFromCodeCoverage]
public record InstanceQueueReadyEvent(
    [property: JsonPropertyName("instanceLocation")]
    string InstanceLocation,
    [property: JsonPropertyName("expiry")] DateTimeOffset Expiry
) : VRChatWebSocketPayloadBase;
