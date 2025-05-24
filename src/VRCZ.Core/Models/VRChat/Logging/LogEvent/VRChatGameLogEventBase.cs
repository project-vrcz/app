namespace VRCZ.Core.Models.VRChat.Logging.LogEvent;

public record VRChatGameLogEventBase(VRChatLogEntity LogEntity)
{
    public DateTime Timestamp => LogEntity.Timestamp;
}
