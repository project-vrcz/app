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
    /// Reasons available for reporting users
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class APIConfig_reportReasons : IAdditionalDataHolder, IParsable
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>A reason used for reporting users</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::VRCZ.VRChatApi.Generated.Models.ReportReason? Billing { get; set; }
#nullable restore
#else
        public global::VRCZ.VRChatApi.Generated.Models.ReportReason Billing { get; set; }
#endif
        /// <summary>A reason used for reporting users</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::VRCZ.VRChatApi.Generated.Models.ReportReason? Botting { get; set; }
#nullable restore
#else
        public global::VRCZ.VRChatApi.Generated.Models.ReportReason Botting { get; set; }
#endif
        /// <summary>A reason used for reporting users</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::VRCZ.VRChatApi.Generated.Models.ReportReason? Cancellation { get; set; }
#nullable restore
#else
        public global::VRCZ.VRChatApi.Generated.Models.ReportReason Cancellation { get; set; }
#endif
        /// <summary>A reason used for reporting users</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::VRCZ.VRChatApi.Generated.Models.ReportReason? Gore { get; set; }
#nullable restore
#else
        public global::VRCZ.VRChatApi.Generated.Models.ReportReason Gore { get; set; }
#endif
        /// <summary>A reason used for reporting users</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::VRCZ.VRChatApi.Generated.Models.ReportReason? Hacking { get; set; }
#nullable restore
#else
        public global::VRCZ.VRChatApi.Generated.Models.ReportReason Hacking { get; set; }
#endif
        /// <summary>A reason used for reporting users</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::VRCZ.VRChatApi.Generated.Models.ReportReason? Harassing { get; set; }
#nullable restore
#else
        public global::VRCZ.VRChatApi.Generated.Models.ReportReason Harassing { get; set; }
#endif
        /// <summary>A reason used for reporting users</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::VRCZ.VRChatApi.Generated.Models.ReportReason? Hateful { get; set; }
#nullable restore
#else
        public global::VRCZ.VRChatApi.Generated.Models.ReportReason Hateful { get; set; }
#endif
        /// <summary>A reason used for reporting users</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::VRCZ.VRChatApi.Generated.Models.ReportReason? Impersonation { get; set; }
#nullable restore
#else
        public global::VRCZ.VRChatApi.Generated.Models.ReportReason Impersonation { get; set; }
#endif
        /// <summary>A reason used for reporting users</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::VRCZ.VRChatApi.Generated.Models.ReportReason? Inappropriate { get; set; }
#nullable restore
#else
        public global::VRCZ.VRChatApi.Generated.Models.ReportReason Inappropriate { get; set; }
#endif
        /// <summary>A reason used for reporting users</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::VRCZ.VRChatApi.Generated.Models.ReportReason? Leaking { get; set; }
#nullable restore
#else
        public global::VRCZ.VRChatApi.Generated.Models.ReportReason Leaking { get; set; }
#endif
        /// <summary>A reason used for reporting users</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::VRCZ.VRChatApi.Generated.Models.ReportReason? Malicious { get; set; }
#nullable restore
#else
        public global::VRCZ.VRChatApi.Generated.Models.ReportReason Malicious { get; set; }
#endif
        /// <summary>A reason used for reporting users</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::VRCZ.VRChatApi.Generated.Models.ReportReason? Missing { get; set; }
#nullable restore
#else
        public global::VRCZ.VRChatApi.Generated.Models.ReportReason Missing { get; set; }
#endif
        /// <summary>A reason used for reporting users</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::VRCZ.VRChatApi.Generated.Models.ReportReason? Nudity { get; set; }
#nullable restore
#else
        public global::VRCZ.VRChatApi.Generated.Models.ReportReason Nudity { get; set; }
#endif
        /// <summary>A reason used for reporting users</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::VRCZ.VRChatApi.Generated.Models.ReportReason? Renewal { get; set; }
#nullable restore
#else
        public global::VRCZ.VRChatApi.Generated.Models.ReportReason Renewal { get; set; }
#endif
        /// <summary>A reason used for reporting users</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::VRCZ.VRChatApi.Generated.Models.ReportReason? Security { get; set; }
#nullable restore
#else
        public global::VRCZ.VRChatApi.Generated.Models.ReportReason Security { get; set; }
#endif
        /// <summary>A reason used for reporting users</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::VRCZ.VRChatApi.Generated.Models.ReportReason? Service { get; set; }
#nullable restore
#else
        public global::VRCZ.VRChatApi.Generated.Models.ReportReason Service { get; set; }
#endif
        /// <summary>A reason used for reporting users</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::VRCZ.VRChatApi.Generated.Models.ReportReason? Sexual { get; set; }
#nullable restore
#else
        public global::VRCZ.VRChatApi.Generated.Models.ReportReason Sexual { get; set; }
#endif
        /// <summary>A reason used for reporting users</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::VRCZ.VRChatApi.Generated.Models.ReportReason? Threatening { get; set; }
#nullable restore
#else
        public global::VRCZ.VRChatApi.Generated.Models.ReportReason Threatening { get; set; }
#endif
        /// <summary>A reason used for reporting users</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::VRCZ.VRChatApi.Generated.Models.ReportReason? Visuals { get; set; }
#nullable restore
#else
        public global::VRCZ.VRChatApi.Generated.Models.ReportReason Visuals { get; set; }
#endif
        /// <summary>
        /// Instantiates a new <see cref="global::VRCZ.VRChatApi.Generated.Models.APIConfig_reportReasons"/> and sets the default values.
        /// </summary>
        public APIConfig_reportReasons()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::VRCZ.VRChatApi.Generated.Models.APIConfig_reportReasons"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::VRCZ.VRChatApi.Generated.Models.APIConfig_reportReasons CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::VRCZ.VRChatApi.Generated.Models.APIConfig_reportReasons();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "billing", n => { Billing = n.GetObjectValue<global::VRCZ.VRChatApi.Generated.Models.ReportReason>(global::VRCZ.VRChatApi.Generated.Models.ReportReason.CreateFromDiscriminatorValue); } },
                { "botting", n => { Botting = n.GetObjectValue<global::VRCZ.VRChatApi.Generated.Models.ReportReason>(global::VRCZ.VRChatApi.Generated.Models.ReportReason.CreateFromDiscriminatorValue); } },
                { "cancellation", n => { Cancellation = n.GetObjectValue<global::VRCZ.VRChatApi.Generated.Models.ReportReason>(global::VRCZ.VRChatApi.Generated.Models.ReportReason.CreateFromDiscriminatorValue); } },
                { "gore", n => { Gore = n.GetObjectValue<global::VRCZ.VRChatApi.Generated.Models.ReportReason>(global::VRCZ.VRChatApi.Generated.Models.ReportReason.CreateFromDiscriminatorValue); } },
                { "hacking", n => { Hacking = n.GetObjectValue<global::VRCZ.VRChatApi.Generated.Models.ReportReason>(global::VRCZ.VRChatApi.Generated.Models.ReportReason.CreateFromDiscriminatorValue); } },
                { "harassing", n => { Harassing = n.GetObjectValue<global::VRCZ.VRChatApi.Generated.Models.ReportReason>(global::VRCZ.VRChatApi.Generated.Models.ReportReason.CreateFromDiscriminatorValue); } },
                { "hateful", n => { Hateful = n.GetObjectValue<global::VRCZ.VRChatApi.Generated.Models.ReportReason>(global::VRCZ.VRChatApi.Generated.Models.ReportReason.CreateFromDiscriminatorValue); } },
                { "impersonation", n => { Impersonation = n.GetObjectValue<global::VRCZ.VRChatApi.Generated.Models.ReportReason>(global::VRCZ.VRChatApi.Generated.Models.ReportReason.CreateFromDiscriminatorValue); } },
                { "inappropriate", n => { Inappropriate = n.GetObjectValue<global::VRCZ.VRChatApi.Generated.Models.ReportReason>(global::VRCZ.VRChatApi.Generated.Models.ReportReason.CreateFromDiscriminatorValue); } },
                { "leaking", n => { Leaking = n.GetObjectValue<global::VRCZ.VRChatApi.Generated.Models.ReportReason>(global::VRCZ.VRChatApi.Generated.Models.ReportReason.CreateFromDiscriminatorValue); } },
                { "malicious", n => { Malicious = n.GetObjectValue<global::VRCZ.VRChatApi.Generated.Models.ReportReason>(global::VRCZ.VRChatApi.Generated.Models.ReportReason.CreateFromDiscriminatorValue); } },
                { "missing", n => { Missing = n.GetObjectValue<global::VRCZ.VRChatApi.Generated.Models.ReportReason>(global::VRCZ.VRChatApi.Generated.Models.ReportReason.CreateFromDiscriminatorValue); } },
                { "nudity", n => { Nudity = n.GetObjectValue<global::VRCZ.VRChatApi.Generated.Models.ReportReason>(global::VRCZ.VRChatApi.Generated.Models.ReportReason.CreateFromDiscriminatorValue); } },
                { "renewal", n => { Renewal = n.GetObjectValue<global::VRCZ.VRChatApi.Generated.Models.ReportReason>(global::VRCZ.VRChatApi.Generated.Models.ReportReason.CreateFromDiscriminatorValue); } },
                { "security", n => { Security = n.GetObjectValue<global::VRCZ.VRChatApi.Generated.Models.ReportReason>(global::VRCZ.VRChatApi.Generated.Models.ReportReason.CreateFromDiscriminatorValue); } },
                { "service", n => { Service = n.GetObjectValue<global::VRCZ.VRChatApi.Generated.Models.ReportReason>(global::VRCZ.VRChatApi.Generated.Models.ReportReason.CreateFromDiscriminatorValue); } },
                { "sexual", n => { Sexual = n.GetObjectValue<global::VRCZ.VRChatApi.Generated.Models.ReportReason>(global::VRCZ.VRChatApi.Generated.Models.ReportReason.CreateFromDiscriminatorValue); } },
                { "threatening", n => { Threatening = n.GetObjectValue<global::VRCZ.VRChatApi.Generated.Models.ReportReason>(global::VRCZ.VRChatApi.Generated.Models.ReportReason.CreateFromDiscriminatorValue); } },
                { "visuals", n => { Visuals = n.GetObjectValue<global::VRCZ.VRChatApi.Generated.Models.ReportReason>(global::VRCZ.VRChatApi.Generated.Models.ReportReason.CreateFromDiscriminatorValue); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteObjectValue<global::VRCZ.VRChatApi.Generated.Models.ReportReason>("billing", Billing);
            writer.WriteObjectValue<global::VRCZ.VRChatApi.Generated.Models.ReportReason>("botting", Botting);
            writer.WriteObjectValue<global::VRCZ.VRChatApi.Generated.Models.ReportReason>("cancellation", Cancellation);
            writer.WriteObjectValue<global::VRCZ.VRChatApi.Generated.Models.ReportReason>("gore", Gore);
            writer.WriteObjectValue<global::VRCZ.VRChatApi.Generated.Models.ReportReason>("hacking", Hacking);
            writer.WriteObjectValue<global::VRCZ.VRChatApi.Generated.Models.ReportReason>("harassing", Harassing);
            writer.WriteObjectValue<global::VRCZ.VRChatApi.Generated.Models.ReportReason>("hateful", Hateful);
            writer.WriteObjectValue<global::VRCZ.VRChatApi.Generated.Models.ReportReason>("impersonation", Impersonation);
            writer.WriteObjectValue<global::VRCZ.VRChatApi.Generated.Models.ReportReason>("inappropriate", Inappropriate);
            writer.WriteObjectValue<global::VRCZ.VRChatApi.Generated.Models.ReportReason>("leaking", Leaking);
            writer.WriteObjectValue<global::VRCZ.VRChatApi.Generated.Models.ReportReason>("malicious", Malicious);
            writer.WriteObjectValue<global::VRCZ.VRChatApi.Generated.Models.ReportReason>("missing", Missing);
            writer.WriteObjectValue<global::VRCZ.VRChatApi.Generated.Models.ReportReason>("nudity", Nudity);
            writer.WriteObjectValue<global::VRCZ.VRChatApi.Generated.Models.ReportReason>("renewal", Renewal);
            writer.WriteObjectValue<global::VRCZ.VRChatApi.Generated.Models.ReportReason>("security", Security);
            writer.WriteObjectValue<global::VRCZ.VRChatApi.Generated.Models.ReportReason>("service", Service);
            writer.WriteObjectValue<global::VRCZ.VRChatApi.Generated.Models.ReportReason>("sexual", Sexual);
            writer.WriteObjectValue<global::VRCZ.VRChatApi.Generated.Models.ReportReason>("threatening", Threatening);
            writer.WriteObjectValue<global::VRCZ.VRChatApi.Generated.Models.ReportReason>("visuals", Visuals);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
