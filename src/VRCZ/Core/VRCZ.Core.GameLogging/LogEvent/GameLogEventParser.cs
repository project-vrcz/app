using VRCZ.Core.GameLogging.Models;
using VRCZ.Core.GameLogging.Models.LogEvent;

namespace VRCZ.Core.GameLogging.LogEvent;

public class GameLogEventParser
{
    public List<IGameLogEventParser> LogEventParsers { get; } = [
        new PlayerJoinLogEventParser(),
        new PlayerLeftLogEventParser(),
        new JoiningInstanceEventParser(),
        new LeftInstanceEventParser()
    ];

    public GameLogEventBase? Parse(GameLogEntity logEntity)
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
