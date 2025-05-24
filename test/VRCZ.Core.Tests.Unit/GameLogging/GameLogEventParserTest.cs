using VRCZ.Core.GameLogging.LogEvent;
using VRCZ.Core.Models.VRChat.Logging;
using VRCZ.Core.Models.VRChat.Logging.LogEvent;

namespace VRCZ.Core.Tests.Unit.GameLogging;

public class GameLogEventParserTest
{
    [Fact]
    public void Parse_ShouldReturnNull_WhenLogEntityMatchNothing()
    {
        // Arrange

        var parser = new VRChatGameLogEventParser();
        var logEntity = new VRChatLogEntity(new DateTime(2023, 10, 1), "Debug", "TestMessage");

        // Act
        var result = parser.Parse(logEntity);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void Parse_ShouldReturnPlayerJoinEvent_WhenLogEntityIsPlayerJoin()
    {
        // Arrange
        var parser = new VRChatGameLogEventParser();
        var logEntity = new VRChatLogEntity(new DateTime(2023, 10, 1), "Info", "[Behaviour] OnPlayerJoined PlayerNickname (PlayerUserId)");

        // Act
        var result = parser.Parse(logEntity);

        // Assert
        Assert.NotNull(result);

        var joinEvent = Assert.IsType<PlayerJoinLogEvent>(result);

        Assert.Equal("PlayerNickname", joinEvent.Nickname);
        Assert.Equal("PlayerUserId", joinEvent.UserId);
    }
}
