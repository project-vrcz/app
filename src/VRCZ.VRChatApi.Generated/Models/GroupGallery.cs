// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace VRCZ.VRChatApi.Generated.Models
{
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    #pragma warning disable CS1591
    public partial class GroupGallery : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The createdAt property</summary>
        public DateTimeOffset? CreatedAt { get; set; }
        /// <summary>Description of the gallery.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Description { get; set; }
#nullable restore
#else
        public string Description { get; set; }
#endif
        /// <summary>The id property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Id { get; set; }
#nullable restore
#else
        public string Id { get; set; }
#endif
        /// <summary>Whether the gallery is members only.</summary>
        public bool? MembersOnly { get; set; }
        /// <summary>Name of the gallery.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Name { get; set; }
#nullable restore
#else
        public string Name { get; set; }
#endif
        /// <summary> </summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? RoleIdsToAutoApprove { get; set; }
#nullable restore
#else
        public List<string> RoleIdsToAutoApprove { get; set; }
#endif
        /// <summary> </summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? RoleIdsToManage { get; set; }
#nullable restore
#else
        public List<string> RoleIdsToManage { get; set; }
#endif
        /// <summary> </summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? RoleIdsToSubmit { get; set; }
#nullable restore
#else
        public List<string> RoleIdsToSubmit { get; set; }
#endif
        /// <summary> </summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? RoleIdsToView { get; set; }
#nullable restore
#else
        public List<string> RoleIdsToView { get; set; }
#endif
        /// <summary>The updatedAt property</summary>
        public DateTimeOffset? UpdatedAt { get; set; }
        /// <summary>
        /// Instantiates a new <see cref="global::VRCZ.VRChatApi.Generated.Models.GroupGallery"/> and sets the default values.
        /// </summary>
        public GroupGallery()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::VRCZ.VRChatApi.Generated.Models.GroupGallery"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::VRCZ.VRChatApi.Generated.Models.GroupGallery CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::VRCZ.VRChatApi.Generated.Models.GroupGallery();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "createdAt", n => { CreatedAt = n.GetDateTimeOffsetValue(); } },
                { "description", n => { Description = n.GetStringValue(); } },
                { "id", n => { Id = n.GetStringValue(); } },
                { "membersOnly", n => { MembersOnly = n.GetBoolValue(); } },
                { "name", n => { Name = n.GetStringValue(); } },
                { "roleIdsToAutoApprove", n => { RoleIdsToAutoApprove = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "roleIdsToManage", n => { RoleIdsToManage = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "roleIdsToSubmit", n => { RoleIdsToSubmit = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "roleIdsToView", n => { RoleIdsToView = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "updatedAt", n => { UpdatedAt = n.GetDateTimeOffsetValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteDateTimeOffsetValue("createdAt", CreatedAt);
            writer.WriteStringValue("description", Description);
            writer.WriteStringValue("id", Id);
            writer.WriteBoolValue("membersOnly", MembersOnly);
            writer.WriteStringValue("name", Name);
            writer.WriteCollectionOfPrimitiveValues<string>("roleIdsToAutoApprove", RoleIdsToAutoApprove);
            writer.WriteCollectionOfPrimitiveValues<string>("roleIdsToManage", RoleIdsToManage);
            writer.WriteCollectionOfPrimitiveValues<string>("roleIdsToSubmit", RoleIdsToSubmit);
            writer.WriteCollectionOfPrimitiveValues<string>("roleIdsToView", RoleIdsToView);
            writer.WriteDateTimeOffsetValue("updatedAt", UpdatedAt);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
