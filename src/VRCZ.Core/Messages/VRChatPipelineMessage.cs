using VRCZ.Core.Models.VRChat.WebSocket;

namespace VRCZ.Core.Messages;

public sealed record VRChatPipelineMessage(VRChatWebSocketPayloadBase Payload);
