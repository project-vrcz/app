// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System;
using VRCZ.VRChatApi.Generated.Models;
namespace VRCZ.VRChatApi.Generated.Favorite.Groups
{
    /// <summary>
    /// Builds and executes requests for operations under \favorite\groups
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class GroupsRequestBuilder : BaseRequestBuilder
    {
        /// <summary>
        /// Instantiates a new <see cref="global::VRCZ.VRChatApi.Generated.Favorite.Groups.GroupsRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public GroupsRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/favorite/groups{?n*,offset*,ownerId*}", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::VRCZ.VRChatApi.Generated.Favorite.Groups.GroupsRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public GroupsRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/favorite/groups{?n*,offset*,ownerId*}", rawUrl)
        {
        }
        /// <summary>
        /// Return a list of favorite groups owned by a user. Returns the same information as `getFavoriteGroups`.
        /// </summary>
        /// <returns>A List&lt;global::VRCZ.VRChatApi.Generated.Models.FavoriteGroup&gt;</returns>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
        /// <exception cref="global::VRCZ.VRChatApi.Generated.Models.Error">When receiving a 401 status code</exception>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<List<global::VRCZ.VRChatApi.Generated.Models.FavoriteGroup>?> GetAsync(Action<RequestConfiguration<global::VRCZ.VRChatApi.Generated.Favorite.Groups.GroupsRequestBuilder.GroupsRequestBuilderGetQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task<List<global::VRCZ.VRChatApi.Generated.Models.FavoriteGroup>> GetAsync(Action<RequestConfiguration<global::VRCZ.VRChatApi.Generated.Favorite.Groups.GroupsRequestBuilder.GroupsRequestBuilderGetQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            var requestInfo = ToGetRequestInformation(requestConfiguration);
            var errorMapping = new Dictionary<string, ParsableFactory<IParsable>>
            {
                { "401", global::VRCZ.VRChatApi.Generated.Models.Error.CreateFromDiscriminatorValue },
            };
            var collectionResult = await RequestAdapter.SendCollectionAsync<global::VRCZ.VRChatApi.Generated.Models.FavoriteGroup>(requestInfo, global::VRCZ.VRChatApi.Generated.Models.FavoriteGroup.CreateFromDiscriminatorValue, errorMapping, cancellationToken).ConfigureAwait(false);
            return collectionResult?.AsList();
        }
        /// <summary>
        /// Return a list of favorite groups owned by a user. Returns the same information as `getFavoriteGroups`.
        /// </summary>
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<global::VRCZ.VRChatApi.Generated.Favorite.Groups.GroupsRequestBuilder.GroupsRequestBuilderGetQueryParameters>>? requestConfiguration = default)
        {
#nullable restore
#else
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<global::VRCZ.VRChatApi.Generated.Favorite.Groups.GroupsRequestBuilder.GroupsRequestBuilderGetQueryParameters>> requestConfiguration = default)
        {
#endif
            var requestInfo = new RequestInformation(Method.GET, UrlTemplate, PathParameters);
            requestInfo.Configure(requestConfiguration);
            requestInfo.Headers.TryAdd("Accept", "application/json");
            return requestInfo;
        }
        /// <summary>
        /// Returns a request builder with the provided arbitrary URL. Using this method means any other path or query parameters are ignored.
        /// </summary>
        /// <returns>A <see cref="global::VRCZ.VRChatApi.Generated.Favorite.Groups.GroupsRequestBuilder"/></returns>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        public global::VRCZ.VRChatApi.Generated.Favorite.Groups.GroupsRequestBuilder WithUrl(string rawUrl)
        {
            return new global::VRCZ.VRChatApi.Generated.Favorite.Groups.GroupsRequestBuilder(rawUrl, RequestAdapter);
        }
        /// <summary>
        /// Return a list of favorite groups owned by a user. Returns the same information as `getFavoriteGroups`.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class GroupsRequestBuilderGetQueryParameters 
        {
            /// <summary>The number of objects to return.</summary>
            [QueryParameter("n")]
            public int? N { get; set; }
            /// <summary>A zero-based offset from the default object sorting from where search results start.</summary>
            [QueryParameter("offset")]
            public int? Offset { get; set; }
            /// <summary>The owner of whoms favorite groups to return. Must be a UserID.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("ownerId")]
            public string? OwnerId { get; set; }
#nullable restore
#else
            [QueryParameter("ownerId")]
            public string OwnerId { get; set; }
#endif
        }
        /// <summary>
        /// Configuration for the request such as headers, query parameters, and middleware options.
        /// </summary>
        [Obsolete("This class is deprecated. Please use the generic RequestConfiguration class generated by the generator.")]
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class GroupsRequestBuilderGetRequestConfiguration : RequestConfiguration<global::VRCZ.VRChatApi.Generated.Favorite.Groups.GroupsRequestBuilder.GroupsRequestBuilderGetQueryParameters>
        {
        }
    }
}
#pragma warning restore CS0618
