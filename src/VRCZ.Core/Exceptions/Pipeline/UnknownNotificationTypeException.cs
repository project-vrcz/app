using System.Diagnostics.CodeAnalysis;

namespace VRCZ.Core.Exceptions.Pipeline;

[ExcludeFromCodeCoverage]
public class UnknownNotificationTypeException(string eventType) : Exception
{
    public string EventType => eventType;
    public override string Message => $"Unknown notification type: {eventType}";
}
