using System.Text.RegularExpressions;
using VRCZ.Core.GameLogging.Models.LogEvent;

namespace VRCZ.Core.GameLogging.Models;

public partial class GameLogEntity(DateTime timestamp, string logLevel, string message)
{
    [GeneratedRegex(@"(?<TimeStamp>\d{4}\.\d{2}\.\d{2} \d{2}:\d{2}:\d{2}) (?<LogLevel>\w+) *- {2}(?<Message>.*)")]
    private static partial Regex GetLogRegex();

    private static Regex? _logRegex;
    public static Regex LogRegex => _logRegex ??= GetLogRegex();

    public static GameLogEntity Parse(string logLine)
    {
        var lines = logLine.Split(["\r\n", "\n", "\r"], StringSplitOptions.None);

        var match = LogRegex.Match(lines[0]);

        if (!match.Success)
            throw new FormatException("Invalid log line format.");

        var timestamp = DateTime.Parse(match.Groups["TimeStamp"].Value);
        var logLevel = match.Groups["LogLevel"].Value;
        var firstLineMessage = match.Groups["Message"].Value;

        var message = lines.Length == 1
            ? firstLineMessage
            : firstLineMessage + "\n" + string.Join("\n", lines.Skip(1));

        return new GameLogEntity(timestamp, logLevel, message);
    }

    public DateTime Timestamp => timestamp;
    public string LogLevel => logLevel;
    public string Message => message;
}

public class GameLogEntityWithEvent(GameLogEntity logEntity, GameLogEventBase? logEvent)
{
    public GameLogEntity LogEntity => logEntity;
    public GameLogEventBase? LogEvent => logEvent;
}