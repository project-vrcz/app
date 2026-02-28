namespace VRCZ.Core.GameLogging.Models.LogEvent;

public interface IGameLogEventParser<out TEvent> : IGameLogEventParser where TEvent : GameLogEventBase
{
    new TEvent? Parse(GameLogEntity entity);
}

public interface IGameLogEventParser
{
    GameLogEventBase? Parse(GameLogEntity entity);
}
