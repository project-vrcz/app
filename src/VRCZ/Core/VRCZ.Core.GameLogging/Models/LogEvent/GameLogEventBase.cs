using System.Diagnostics.CodeAnalysis;

namespace VRCZ.Core.GameLogging.Models.LogEvent;

[ExcludeFromCodeCoverage]
public abstract class GameLogEventBase(GameLogEntity logEntity)
{
    public DateTime Timestamp => LogEntity.Timestamp;

    public GameLogEntity LogEntity => logEntity;
}