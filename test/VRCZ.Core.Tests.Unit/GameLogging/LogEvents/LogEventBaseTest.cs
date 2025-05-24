using VRCZ.Core.Models.VRChat.Logging;
using VRCZ.Core.Models.VRChat.Logging.LogEvent;

namespace VRCZ.Core.Tests.Unit.GameLogging.LogEvents;

public class LogEventBaseTest
{
    [Fact]
    public void Constructor()
    {
        var timestamp = new DateTime(2023, 10, 1, 12, 0, 0);
        var logEntity = new VRChatLogEntity(timestamp, "Debug", "Test message");

        var logEvent = new VRChatGameLogEventBase(logEntity);

        Assert.Equal(logEntity.Timestamp, logEvent.Timestamp);
    }
}
