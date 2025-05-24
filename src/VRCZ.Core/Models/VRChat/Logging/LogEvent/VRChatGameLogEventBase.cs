namespace VRCZ.Core.Models.VRChat.Logging.LogEvent;

public abstract record VRChatGameLogEventBase(VRChatLogEntity LogEntity)
{
    public DateTime Timestamp => LogEntity.Timestamp;
}
