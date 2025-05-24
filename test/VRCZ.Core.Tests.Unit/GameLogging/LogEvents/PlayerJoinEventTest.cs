using VRCZ.Core.Models.VRChat.Logging;
using VRCZ.Core.Models.VRChat.Logging.LogEvent;

namespace VRCZ.Core.Tests.Unit.GameLogging.LogEvents;

public class PlayerJoinEventTest
{
    private const string PlayerNickname = "TestUser";
    private const string PlayerUserId = "usr_c6806bef-xxxx-427d-xxxx-e24a66acb829";

    private readonly VRChatLogEntity _validLogEntity =
        new(new DateTime(2023, 10, 1, 12, 0, 0), "Debug",
            $"[Behaviour] OnPlayerJoined {PlayerNickname} ({PlayerUserId})");

    private readonly VRChatLogEntity _invalidLogEntity =
        new(new DateTime(2023, 10, 1, 12, 0, 0), "Debug", "Invalid log message");

    [Fact]
    public void ParsePlayerJoinLogEvent()
    {
        var parser = new PlayerJoinLogEventParser();

        var result = parser.Parse(_validLogEntity);

        Assert.NotNull(result);
        Assert.Equal(PlayerNickname, result.Nickname);
        Assert.Equal(PlayerUserId, result.UserId);
    }

    [Fact]
    public void ParseInvalidLogEntityReturnsNull()
    {
        var parser = new PlayerJoinLogEventParser();

        var result = parser.Parse(_invalidLogEntity);

        Assert.Null(result);
    }

    [Fact]
    public void ParsePlayerJoinLogEventAsInterface()
    {
        IVRChatGameLogEventParser parser = new PlayerJoinLogEventParser();

        var result = parser.Parse(_validLogEntity);

        var typedResult = Assert.IsType<PlayerJoinLogEvent>(result);

        Assert.NotNull(result);
        Assert.Equal(PlayerNickname, typedResult.Nickname);
        Assert.Equal(PlayerUserId, typedResult.UserId);
    }

    [Fact]
    public void ParseInvalidLogEntityReturnsNullAsInterface()
    {
        IVRChatGameLogEventParser parser = new PlayerJoinLogEventParser();

        var result = parser.Parse(_invalidLogEntity);

        Assert.Null(result);
    }
}
