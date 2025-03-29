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
using VRCZ.VRChatApi.Generated.Users.Item.AddTags;
using VRCZ.VRChatApi.Generated.Users.Item.Avatar;
using VRCZ.VRChatApi.Generated.Users.Item.Badges;
using VRCZ.VRChatApi.Generated.Users.Item.Delete;
using VRCZ.VRChatApi.Generated.Users.Item.Feedback;
using VRCZ.VRChatApi.Generated.Users.Item.Groups;
using VRCZ.VRChatApi.Generated.Users.Item.Instances;
using VRCZ.VRChatApi.Generated.Users.Item.Item;
using VRCZ.VRChatApi.Generated.Users.Item.Name;
using VRCZ.VRChatApi.Generated.Users.Item.RemoveTags;
namespace VRCZ.VRChatApi.Generated.Users.Item
{
    /// <summary>
    /// Builds and executes requests for operations under \users\{user-id}
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class UserItemRequestBuilder : BaseRequestBuilder
    {
        /// <summary>The addTags property</summary>
        public global::VRCZ.VRChatApi.Generated.Users.Item.AddTags.AddTagsRequestBuilder AddTags
        {
            get => new global::VRCZ.VRChatApi.Generated.Users.Item.AddTags.AddTagsRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The avatar property</summary>
        public global::VRCZ.VRChatApi.Generated.Users.Item.Avatar.AvatarRequestBuilder Avatar
        {
            get => new global::VRCZ.VRChatApi.Generated.Users.Item.Avatar.AvatarRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The badges property</summary>
        public global::VRCZ.VRChatApi.Generated.Users.Item.Badges.BadgesRequestBuilder Badges
        {
            get => new global::VRCZ.VRChatApi.Generated.Users.Item.Badges.BadgesRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The deletePath property</summary>
        public global::VRCZ.VRChatApi.Generated.Users.Item.Delete.DeleteRequestBuilder DeletePath
        {
            get => new global::VRCZ.VRChatApi.Generated.Users.Item.Delete.DeleteRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The feedback property</summary>
        public global::VRCZ.VRChatApi.Generated.Users.Item.Feedback.FeedbackRequestBuilder Feedback
        {
            get => new global::VRCZ.VRChatApi.Generated.Users.Item.Feedback.FeedbackRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The groups property</summary>
        public global::VRCZ.VRChatApi.Generated.Users.Item.Groups.GroupsRequestBuilder Groups
        {
            get => new global::VRCZ.VRChatApi.Generated.Users.Item.Groups.GroupsRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The instances property</summary>
        public global::VRCZ.VRChatApi.Generated.Users.Item.Instances.InstancesRequestBuilder Instances
        {
            get => new global::VRCZ.VRChatApi.Generated.Users.Item.Instances.InstancesRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The name property</summary>
        public global::VRCZ.VRChatApi.Generated.Users.Item.Name.NameRequestBuilder Name
        {
            get => new global::VRCZ.VRChatApi.Generated.Users.Item.Name.NameRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The removeTags property</summary>
        public global::VRCZ.VRChatApi.Generated.Users.Item.RemoveTags.RemoveTagsRequestBuilder RemoveTags
        {
            get => new global::VRCZ.VRChatApi.Generated.Users.Item.RemoveTags.RemoveTagsRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>Gets an item from the VRCZ.VRChatApi.Generated.users.item.item collection</summary>
        /// <param name="position">Must be a valid world ID.</param>
        /// <returns>A <see cref="global::VRCZ.VRChatApi.Generated.Users.Item.Item.WithWorldItemRequestBuilder"/></returns>
        public global::VRCZ.VRChatApi.Generated.Users.Item.Item.WithWorldItemRequestBuilder this[string position]
        {
            get
            {
                var urlTplParams = new Dictionary<string, object>(PathParameters);
                urlTplParams.Add("worldId", position);
                return new global::VRCZ.VRChatApi.Generated.Users.Item.Item.WithWorldItemRequestBuilder(urlTplParams, RequestAdapter);
            }
        }
        /// <summary>
        /// Instantiates a new <see cref="global::VRCZ.VRChatApi.Generated.Users.Item.UserItemRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public UserItemRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/users/{user%2Did}", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::VRCZ.VRChatApi.Generated.Users.Item.UserItemRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public UserItemRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/users/{user%2Did}", rawUrl)
        {
        }
        /// <summary>
        /// Get public user information about a specific user using their ID.
        /// </summary>
        /// <returns>A <see cref="global::VRCZ.VRChatApi.Generated.Models.User"/></returns>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
        /// <exception cref="global::VRCZ.VRChatApi.Generated.Models.Error">When receiving a 401 status code</exception>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<global::VRCZ.VRChatApi.Generated.Models.User?> GetAsync(Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task<global::VRCZ.VRChatApi.Generated.Models.User> GetAsync(Action<RequestConfiguration<DefaultQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            var requestInfo = ToGetRequestInformation(requestConfiguration);
            var errorMapping = new Dictionary<string, ParsableFactory<IParsable>>
            {
                { "401", global::VRCZ.VRChatApi.Generated.Models.Error.CreateFromDiscriminatorValue },
            };
            return await RequestAdapter.SendAsync<global::VRCZ.VRChatApi.Generated.Models.User>(requestInfo, global::VRCZ.VRChatApi.Generated.Models.User.CreateFromDiscriminatorValue, errorMapping, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Update a users information such as the email and birthday.
        /// </summary>
        /// <returns>A <see cref="global::VRCZ.VRChatApi.Generated.Models.CurrentUser"/></returns>
        /// <param name="body">The request body</param>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
        /// <exception cref="global::VRCZ.VRChatApi.Generated.Models.Error">When receiving a 401 status code</exception>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<global::VRCZ.VRChatApi.Generated.Models.CurrentUser?> PutAsync(global::VRCZ.VRChatApi.Generated.Models.UpdateUserRequest body, Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task<global::VRCZ.VRChatApi.Generated.Models.CurrentUser> PutAsync(global::VRCZ.VRChatApi.Generated.Models.UpdateUserRequest body, Action<RequestConfiguration<DefaultQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            _ = body ?? throw new ArgumentNullException(nameof(body));
            var requestInfo = ToPutRequestInformation(body, requestConfiguration);
            var errorMapping = new Dictionary<string, ParsableFactory<IParsable>>
            {
                { "401", global::VRCZ.VRChatApi.Generated.Models.Error.CreateFromDiscriminatorValue },
            };
            return await RequestAdapter.SendAsync<global::VRCZ.VRChatApi.Generated.Models.CurrentUser>(requestInfo, global::VRCZ.VRChatApi.Generated.Models.CurrentUser.CreateFromDiscriminatorValue, errorMapping, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Get public user information about a specific user using their ID.
        /// </summary>
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = default)
        {
#nullable restore
#else
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<DefaultQueryParameters>> requestConfiguration = default)
        {
#endif
            var requestInfo = new RequestInformation(Method.GET, UrlTemplate, PathParameters);
            requestInfo.Configure(requestConfiguration);
            requestInfo.Headers.TryAdd("Accept", "application/json");
            return requestInfo;
        }
        /// <summary>
        /// Update a users information such as the email and birthday.
        /// </summary>
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="body">The request body</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToPutRequestInformation(global::VRCZ.VRChatApi.Generated.Models.UpdateUserRequest body, Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = default)
        {
#nullable restore
#else
        public RequestInformation ToPutRequestInformation(global::VRCZ.VRChatApi.Generated.Models.UpdateUserRequest body, Action<RequestConfiguration<DefaultQueryParameters>> requestConfiguration = default)
        {
#endif
            _ = body ?? throw new ArgumentNullException(nameof(body));
            var requestInfo = new RequestInformation(Method.PUT, UrlTemplate, PathParameters);
            requestInfo.Configure(requestConfiguration);
            requestInfo.Headers.TryAdd("Accept", "application/json");
            requestInfo.SetContentFromParsable(RequestAdapter, "application/json", body);
            return requestInfo;
        }
        /// <summary>
        /// Returns a request builder with the provided arbitrary URL. Using this method means any other path or query parameters are ignored.
        /// </summary>
        /// <returns>A <see cref="global::VRCZ.VRChatApi.Generated.Users.Item.UserItemRequestBuilder"/></returns>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        public global::VRCZ.VRChatApi.Generated.Users.Item.UserItemRequestBuilder WithUrl(string rawUrl)
        {
            return new global::VRCZ.VRChatApi.Generated.Users.Item.UserItemRequestBuilder(rawUrl, RequestAdapter);
        }
        /// <summary>
        /// Configuration for the request such as headers, query parameters, and middleware options.
        /// </summary>
        [Obsolete("This class is deprecated. Please use the generic RequestConfiguration class generated by the generator.")]
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class UserItemRequestBuilderGetRequestConfiguration : RequestConfiguration<DefaultQueryParameters>
        {
        }
        /// <summary>
        /// Configuration for the request such as headers, query parameters, and middleware options.
        /// </summary>
        [Obsolete("This class is deprecated. Please use the generic RequestConfiguration class generated by the generator.")]
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class UserItemRequestBuilderPutRequestConfiguration : RequestConfiguration<DefaultQueryParameters>
        {
        }
    }
}
#pragma warning restore CS0618
