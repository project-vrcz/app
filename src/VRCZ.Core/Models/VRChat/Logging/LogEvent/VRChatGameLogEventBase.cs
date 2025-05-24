using System.Diagnostics.CodeAnalysis;

namespace VRCZ.Core.Models.VRChat.Logging.LogEvent;

[ExcludeFromCodeCoverage]
public abstract record VRChatGameLogEventBase(VRChatLogEntity LogEntity)
{
    public DateTime Timestamp => LogEntity.Timestamp;
}
