namespace VRCZ.Core.Models.VRChat.Logging.LogEvent;

public interface IVRChatGameLogEventParser<out TEvent> : IVRChatGameLogEventParser where TEvent : VRChatGameLogEventBase
{
    TEvent? Parse(VRChatLogEntity entity);
}

public interface IVRChatGameLogEventParser
{
    VRChatGameLogEventBase? Parse(VRChatLogEntity entity);
}
