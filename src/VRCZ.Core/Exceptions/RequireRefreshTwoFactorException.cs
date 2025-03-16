﻿using VRCZ.VRChatApi.Generated.Models;

namespace VRCZ.Core.Exceptions;

public class RequireRefreshTwoFactorException(TwoFactorRequired_requiresTwoFactorAuth[] availableTwoFactor) : Exception
{
    public override string Message => "Require Refresh Two Factor";
    public TwoFactorRequired_requiresTwoFactorAuth[] AvailableTwoFactor => availableTwoFactor;
}
