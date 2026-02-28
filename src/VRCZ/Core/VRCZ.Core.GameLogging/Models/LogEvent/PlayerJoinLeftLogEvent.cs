using System.Text.RegularExpressions;

namespace VRCZ.Core.GameLogging.Models.LogEvent;

public sealed class PlayerJoinLogEvent(GameLogEntity logEntity, string nickname, string userId) : GameLogEventBase(logEntity)
{
    public string Nickname => nickname;
    public string UserId => userId;
}

public sealed partial class PlayerJoinLogEventParser : IGameLogEventParser<PlayerJoinLogEvent>
{
    [GeneratedRegex(@"^\[Behaviour\] OnPlayerJoined (?<Nickname>.+) \((?<UserId>.+)\)$")]
    private static partial Regex OnPlayerJoinedRegex();

    public PlayerJoinLogEvent? Parse(GameLogEntity logEntity)
    {
        var regex = OnPlayerJoinedRegex();

        var match = regex.Match(logEntity.Message);

        if (!match.Success)
            return null;

        var nickname = match.Groups["Nickname"].Value;
        var userId = match.Groups["UserId"].Value;

        return new PlayerJoinLogEvent(logEntity, nickname, userId);
    }

    GameLogEventBase? IGameLogEventParser.Parse(GameLogEntity entity)
    {
        return Parse(entity);
    }
}

public sealed class PlayerLeftLogEvent(GameLogEntity logEntity, string Nickname, string UserId) : GameLogEventBase(logEntity);

public sealed partial class PlayerLeftLogEventParser : IGameLogEventParser<PlayerLeftLogEvent>
{
    [GeneratedRegex(@"^\[Behaviour\] OnPlayerLeft (?<Nickname>.+) \((?<UserId>.+)\)$")]
    private static partial Regex OnPlayerLeftRegex();

    public PlayerLeftLogEvent? Parse(GameLogEntity logEntity)
    {
        var regex = OnPlayerLeftRegex();

        var match = regex.Match(logEntity.Message);

        if (!match.Success)
            return null;

        var nickname = match.Groups["Nickname"].Value;
        var userId = match.Groups["UserId"].Value;

        return new PlayerLeftLogEvent(logEntity, nickname, userId);
    }

    GameLogEventBase? IGameLogEventParser.Parse(GameLogEntity entity)
    {
        return Parse(entity);
    }
}
