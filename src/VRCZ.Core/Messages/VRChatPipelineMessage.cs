using System.Diagnostics.CodeAnalysis;
using VRCZ.Core.Models.VRChat.WebSocket;

namespace VRCZ.Core.Messages;

[ExcludeFromCodeCoverage]
public sealed record VRChatPipelineMessage(VRChatWebSocketPayloadBase Payload);
