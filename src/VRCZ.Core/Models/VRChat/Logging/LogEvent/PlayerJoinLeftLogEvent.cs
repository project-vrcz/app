using System.Text.RegularExpressions;

namespace VRCZ.Core.Models.VRChat.Logging.LogEvent;

public sealed record PlayerJoinLogEvent(VRChatLogEntity LogEntity, string Nickname, string UserId)
    : VRChatGameLogEventBase(LogEntity);

public sealed partial class PlayerJoinLogEventParser : IVRChatGameLogEventParser<PlayerJoinLogEvent>
{
    [GeneratedRegex(@"^\[Behaviour\] OnPlayerJoined (?<Nickname>.+) \((?<UserId>.+)\)$")]
    private static partial Regex OnPlayerJoinedRegex();

    public PlayerJoinLogEvent? Parse(VRChatLogEntity logEntity)
    {
        var regex = OnPlayerJoinedRegex();

        var match = regex.Match(logEntity.Message);

        if (!match.Success)
            return null;

        var nickname = match.Groups["Nickname"].Value;
        var userId = match.Groups["UserId"].Value;

        return new PlayerJoinLogEvent(logEntity, nickname, userId);
    }

    VRChatGameLogEventBase? IVRChatGameLogEventParser.Parse(VRChatLogEntity entity)
    {
        return Parse(entity);
    }
}

public sealed partial record PlayerLeftLogEvent(VRChatLogEntity LogEntity, string Nickname, string UserId)
    : VRChatGameLogEventBase(LogEntity);

public sealed partial class PlayerLeftLogEventParser : IVRChatGameLogEventParser<PlayerLeftLogEvent>
{
    [GeneratedRegex(@"^\[Behaviour\] OnPlayerLeft (?<Nickname>.+) \((?<UserId>.+)\)$")]
    private static partial Regex OnPlayerLeftRegex();

    public PlayerLeftLogEvent? Parse(VRChatLogEntity logEntity)
    {
        var regex = OnPlayerLeftRegex();

        var match = regex.Match(logEntity.Message);

        if (!match.Success)
            return null;

        var nickname = match.Groups["Nickname"].Value;
        var userId = match.Groups["UserId"].Value;

        return new PlayerLeftLogEvent(logEntity, nickname, userId);
    }

    VRChatGameLogEventBase? IVRChatGameLogEventParser.Parse(VRChatLogEntity entity)
    {
        return Parse(entity);
    }
}
