using System.Diagnostics.CodeAnalysis;
using VRCZ.VRChatApi.Generated.Models;

namespace VRCZ.Core.Models;

[ExcludeFromCodeCoverage]
public record LoginResult(LoginResultType ResultType, TwoFactorRequired_requiresTwoFactorAuth[]? Available2FAMethods = null, CurrentUser? User = null);

public enum LoginResultType
{
    Success,
    TwoFactorRequired
}
