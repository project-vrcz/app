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
    public void Parse_ShouldReturnPlayerJoinEvent_WhenLogEntityIsValid()
    {
        // Arrange
        var parser = new PlayerJoinLogEventParser();

        // Act
        var result = parser.Parse(_validLogEntity);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(PlayerNickname, result.Nickname);
        Assert.Equal(PlayerUserId, result.UserId);
    }

    [Fact]
    public void Parse_ShouldReturnNull_WhenLogEntityIsInvalid()
    {
        // Arrange
        var parser = new PlayerJoinLogEventParser();

        // Act
        var result = parser.Parse(_invalidLogEntity);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void Parse_AsInterface_ShouldReturnPlayerJoinEvent_WhenLogEntityIsValid()
    {
        // Arrange
        IVRChatGameLogEventParser parser = new PlayerJoinLogEventParser();

        // Act
        var result = parser.Parse(_validLogEntity);

        // Assert
        var typedResult = Assert.IsType<PlayerJoinLogEvent>(result);
        Assert.NotNull(result);
        Assert.Equal(PlayerNickname, typedResult.Nickname);
        Assert.Equal(PlayerUserId, typedResult.UserId);
    }

    [Fact]
    public void Parse_AsInterface_ShouldReturnNull_WhenLogEntityIsInvalid()
    {
        // Arrange
        IVRChatGameLogEventParser parser = new PlayerJoinLogEventParser();

        // Act
        var result = parser.Parse(_invalidLogEntity);

        // Assert
        Assert.Null(result);
    }
}
