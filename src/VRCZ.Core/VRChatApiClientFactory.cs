using System.Diagnostics.CodeAnalysis;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Bundle;
using VRCZ.VRChatApi.Generated;

namespace VRCZ.Core;

[ExcludeFromCodeCoverage]
public class VRChatApiClientFactory(HttpClient httpClient)
{
    public VRChatApiClient GetClient()
    {
        return new VRChatApiClient(new DefaultRequestAdapter(new AnonymousAuthenticationProvider(),
            httpClient: httpClient));
    }
}
