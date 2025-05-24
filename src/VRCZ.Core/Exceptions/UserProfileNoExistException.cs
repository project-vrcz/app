using System.Diagnostics.CodeAnalysis;

namespace VRCZ.Core.Exceptions;

[ExcludeFromCodeCoverage]
public class UserProfileNoExistException(string profileId) : Exception
{
    public string ProfileId => profileId;
    public override string Message => $"User profile {ProfileId} does not exist.";
}
