// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace VRCZ.VRChatApi.Generated.Models
{
    /// <summary>
    /// Status information for a service request
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class ServiceStatus : IAdditionalDataHolder, IParsable
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The created_at property</summary>
        public DateTimeOffset? CreatedAt { get; set; }
        /// <summary>The id of this service, NOT the id of the thing this service was requested for.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Id { get; set; }
#nullable restore
#else
        public string Id { get; set; }
#endif
        /// <summary>The progress property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::VRCZ.VRChatApi.Generated.Models.ServiceStatus_progress>? Progress { get; set; }
#nullable restore
#else
        public List<global::VRCZ.VRChatApi.Generated.Models.ServiceStatus_progress> Progress { get; set; }
#endif
        /// <summary>A users unique ID, usually in the form of `usr_c1644b5b-3ca4-45b4-97c6-a2a0de70d469`. Legacy players can have old IDs in the form of `8JoV9XEdpo`. The ID can never be changed.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? RequesterUserId { get; set; }
#nullable restore
#else
        public string RequesterUserId { get; set; }
#endif
        /// <summary>The state property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? State { get; set; }
#nullable restore
#else
        public string State { get; set; }
#endif
        /// <summary>The id of the thing this service was requested for.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? SubjectId { get; set; }
#nullable restore
#else
        public string SubjectId { get; set; }
#endif
        /// <summary>The kind of the thing this service was requested for.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? SubjectType { get; set; }
#nullable restore
#else
        public string SubjectType { get; set; }
#endif
        /// <summary>The kind of service that was requested.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Type { get; set; }
#nullable restore
#else
        public string Type { get; set; }
#endif
        /// <summary>The updated_at property</summary>
        public DateTimeOffset? UpdatedAt { get; set; }
        /// <summary>
        /// Instantiates a new <see cref="global::VRCZ.VRChatApi.Generated.Models.ServiceStatus"/> and sets the default values.
        /// </summary>
        public ServiceStatus()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::VRCZ.VRChatApi.Generated.Models.ServiceStatus"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::VRCZ.VRChatApi.Generated.Models.ServiceStatus CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::VRCZ.VRChatApi.Generated.Models.ServiceStatus();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "created_at", n => { CreatedAt = n.GetDateTimeOffsetValue(); } },
                { "id", n => { Id = n.GetStringValue(); } },
                { "progress", n => { Progress = n.GetCollectionOfObjectValues<global::VRCZ.VRChatApi.Generated.Models.ServiceStatus_progress>(global::VRCZ.VRChatApi.Generated.Models.ServiceStatus_progress.CreateFromDiscriminatorValue)?.AsList(); } },
                { "requesterUserId", n => { RequesterUserId = n.GetStringValue(); } },
                { "state", n => { State = n.GetStringValue(); } },
                { "subjectId", n => { SubjectId = n.GetStringValue(); } },
                { "subjectType", n => { SubjectType = n.GetStringValue(); } },
                { "type", n => { Type = n.GetStringValue(); } },
                { "updated_at", n => { UpdatedAt = n.GetDateTimeOffsetValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteDateTimeOffsetValue("created_at", CreatedAt);
            writer.WriteStringValue("id", Id);
            writer.WriteCollectionOfObjectValues<global::VRCZ.VRChatApi.Generated.Models.ServiceStatus_progress>("progress", Progress);
            writer.WriteStringValue("requesterUserId", RequesterUserId);
            writer.WriteStringValue("state", State);
            writer.WriteStringValue("subjectId", SubjectId);
            writer.WriteStringValue("subjectType", SubjectType);
            writer.WriteStringValue("type", Type);
            writer.WriteDateTimeOffsetValue("updated_at", UpdatedAt);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
