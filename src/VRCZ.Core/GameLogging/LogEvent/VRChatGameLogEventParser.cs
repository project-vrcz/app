using VRCZ.Core.Models.VRChat.Logging;
using VRCZ.Core.Models.VRChat.Logging.LogEvent;

namespace VRCZ.Core.GameLogging.LogEvent;

public class VRChatGameLogEventParser
{
    public List<IVRChatGameLogEventParser> LogEventParsers { get; } = [
        new PlayerJoinLogEventParser(),
        new PlayerLeftLogEventParser(),
        new JoiningInstanceEventParser(),
        new LeftInstanceEventParser()
    ];

    public VRChatGameLogEventBase? Parse(VRChatLogEntity logEntity)
    {
        foreach (var parser in LogEventParsers)
        {
            var result = parser.Parse(logEntity);
            if (result != null)
            {
                return result;
            }
        }

        return null;
    }
}
