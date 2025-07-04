﻿using System.Text;
using Microsoft.Kiota.Http.HttpClientLibrary.Middleware.Options;
using VRCZ.Core.Exceptions;
using VRCZ.Core.Models;
using VRCZ.Core.Models.VRChat;
using VRCZ.Core.Services.Profile;
using VRCZ.VRChatApi.Generated;
using VRCZ.VRChatApi.Generated.Auth.User;
using VRCZ.VRChatApi.Generated.Models;

namespace VRCZ.Core.Services;

public class VRChatAuthService(VRChatApiClient vrchatApiClient)
{
    /// <summary>
    /// Login
    /// </summary>
    /// <param name="username">Username or Email</param>
    /// <param name="password">Password</param>
    /// <returns>User Information</returns>
    /// <exception cref="UnexpectedApiBehaviourException">Unexpected Api Behaviour</exception>
    /// <exception cref="Error">Api Error (e.g. Password is wrong or too many session)</exception>
    public async Task<LoginResult> LoginAsync(string username, string password)
    {
        var token = Convert.ToBase64String(
            Encoding.UTF8.GetBytes($"{Uri.EscapeDataString(username)}:{Uri.EscapeDataString(password)}"));

        var headersInspectionHandler = new HeadersInspectionHandlerOption()
        {
            InspectResponseHeaders = true
        };

        var result = await vrchatApiClient.Auth.User.GetAsUserGetResponseAsync(config =>
        {
            config.Headers.Add("Authorization", $"Basic {token}");

            config.Options.Add(headersInspectionHandler);
        });

        if (result is null)
            throw new UnexpectedApiBehaviourException("Auth User endpoint response null body");

        if (result.CurrentUser is not null)
            return new LoginResult(LoginResultType.Success, User: result.CurrentUser);

        if (result.TwoFactorRequired is not null)
        {
            var availableMethods = result.TwoFactorRequired.RequiresTwoFactorAuth?
                .Where(method => method is not null)
                .OfType<TwoFactorRequired_requiresTwoFactorAuth>()
                .ToArray() ?? [];

            return new LoginResult(LoginResultType.TwoFactorRequired, availableMethods);
        }

        throw new UnexpectedApiBehaviourException("Auth User endpoint response null body");
    }

    public async Task<bool> VerifyTwoFactorAsync(string code, TwoFactorRequired_requiresTwoFactorAuth method)
    {
        var body = new TwoFactorAuthCode
        {
            Code = code
        };

        var result = method switch
        {
            TwoFactorRequired_requiresTwoFactorAuth.Otp => await vrchatApiClient.Auth.Twofactorauth.Otp.Verify.PostAsync(body),
            TwoFactorRequired_requiresTwoFactorAuth.Totp => await vrchatApiClient.Auth.Twofactorauth.Totp.Verify.PostAsync(body),
            _ => throw new NotImplementedException()
        };

        if (result?.Verified is not { } verified)
            throw new UnexpectedApiBehaviourException("Auth Twofactorauth Totp Verify endpoint response null body");

        return verified;
    }
}
