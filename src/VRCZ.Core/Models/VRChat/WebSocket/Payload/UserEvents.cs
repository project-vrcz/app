﻿using System.Text.Json.Serialization;
using VRCZ.VRChatApi.Generated.Models;

namespace VRCZ.Core.Models.VRChat.WebSocket.Payload;

public record UserUpdateEvent(
    [property: JsonPropertyName("userId")] string UserId
) : VRChatWebSocketPayloadBase, IVRChatCurrentUserPayload
{
    [JsonIgnore] public CurrentUser? User { get; set; }
}

public record UserLocationEvent(
    [property: JsonPropertyName("userId")] string UserId,
    [property: JsonPropertyName("location")]
    string Location,
    [property: JsonPropertyName("travelingToLocation")]
    string TravelingToLocation,
    [property: JsonPropertyName("instance")]
    string InstanceId,
    [property: JsonPropertyName("worldId")]
    string? WorldId,
    [property: JsonPropertyName("world")] World? World
) : VRChatWebSocketPayloadBase, IVRChatCurrentUserPayload, IVRChatWebSocketLocationPayload
{
    [JsonIgnore] public CurrentUser? User { get; set; }
}

public record UserBadgeAssignedEvent(
    [property: JsonPropertyName("badge")] string BadgeId
) : VRChatWebSocketPayloadBase;

public record UserBadgeUnassignedEvent(
    [property: JsonPropertyName("badge")] string BadgeId
) : VRChatWebSocketPayloadBase;

public record ContentRefreshEvent(
    [property: JsonPropertyName("contentType")]
    string ContentType,
    [property: JsonPropertyName("actionType")]
    string ActionType
) : VRChatWebSocketPayloadBase;
