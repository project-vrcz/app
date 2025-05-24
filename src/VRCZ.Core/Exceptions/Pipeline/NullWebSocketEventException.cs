using System.Diagnostics.CodeAnalysis;

namespace VRCZ.Core.Exceptions.Pipeline;

[ExcludeFromCodeCoverage]
public class NullWebSocketEventException : Exception
{
    public override string Message => "Deserialized WebSocket event is null";
}
