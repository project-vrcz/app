using System.Diagnostics.CodeAnalysis;

namespace VRCZ.Core.Exceptions;

[ExcludeFromCodeCoverage]
public class UserProfileAlreadyExistException(string profileId) : Exception
{
    public string ProfileId => profileId;
    public override string Message => $"User profile {profileId} already exist";
}
