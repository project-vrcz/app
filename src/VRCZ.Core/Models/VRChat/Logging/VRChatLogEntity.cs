using System.Text.RegularExpressions;

namespace VRCZ.Core.Models.VRChat.Logging;

public partial record VRChatLogEntity(DateTime Timestamp, string LogLevel, string Message)
{
    [GeneratedRegex(@"(?<TimeStampe>\d{4}\.\d{2}\.\d{2} \d{2}:\d{2}:\d{2}) (?<LogLevel>\w+) *- {2}(?<Message>.+)")]
    private static partial Regex GetLogRegex();

    private static Regex? _logRegex;
    public static Regex LogRegex => _logRegex ??= GetLogRegex();

    public static VRChatLogEntity Parse(string logLine)
    {
        var lines = logLine.Split(["\r\n", "\n", "\r"], StringSplitOptions.None);

        var match = LogRegex.Match(lines[0]);

        if (!match.Success)
            throw new FormatException("Invalid log line format.");

        var timestamp = DateTime.Parse(match.Groups["TimeStampe"].Value);
        var logLevel = match.Groups["LogLevel"].Value;
        var firstLineMessage = match.Groups["Message"].Value;

        var message = lines.Length == 1
            ? firstLineMessage
            : firstLineMessage + "\n" + string.Join("\n", lines.Skip(1));

        return new VRChatLogEntity(timestamp, logLevel, message);
    }
}
