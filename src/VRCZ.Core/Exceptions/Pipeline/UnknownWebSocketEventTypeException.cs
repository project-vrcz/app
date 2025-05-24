using System.Diagnostics.CodeAnalysis;

namespace VRCZ.Core.Exceptions.Pipeline;

[ExcludeFromCodeCoverage]
public class UnknownWebSocketEventTypeException(string eventType) : Exception
{
    public string EventType => eventType;
    public override string Message => $"Unknown event type: {eventType}";
}
