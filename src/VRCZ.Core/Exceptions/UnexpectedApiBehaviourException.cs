using System.Diagnostics.CodeAnalysis;

namespace VRCZ.Core.Exceptions;

[ExcludeFromCodeCoverage]
public class UnexpectedApiBehaviourException(string message) : Exception(message);
