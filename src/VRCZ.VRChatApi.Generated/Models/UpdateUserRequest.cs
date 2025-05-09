// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System;
namespace VRCZ.VRChatApi.Generated.Models
{
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    #pragma warning disable CS1591
    public partial class UpdateUserRequest : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>The acceptedTOSVersion property</summary>
        public int? AcceptedTOSVersion { get; set; }
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The bio property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Bio { get; set; }
#nullable restore
#else
        public string Bio { get; set; }
#endif
        /// <summary>The bioLinks property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? BioLinks { get; set; }
#nullable restore
#else
        public List<string> BioLinks { get; set; }
#endif
        /// <summary>The birthday property</summary>
        public Date? Birthday { get; set; }
        /// <summary>The email property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Email { get; set; }
#nullable restore
#else
        public string Email { get; set; }
#endif
        /// <summary>The isBoopingEnabled property</summary>
        public bool? IsBoopingEnabled { get; set; }
        /// <summary>The pronouns property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Pronouns { get; set; }
#nullable restore
#else
        public string Pronouns { get; set; }
#endif
        /// <summary>Defines the User&apos;s current status, for example &quot;ask me&quot;, &quot;join me&quot; or &quot;offline. This status is a combined indicator of their online activity and privacy preference.</summary>
        public global::VRCZ.VRChatApi.Generated.Models.UserStatus? Status { get; set; }
        /// <summary>The statusDescription property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? StatusDescription { get; set; }
#nullable restore
#else
        public string StatusDescription { get; set; }
#endif
        /// <summary> </summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? Tags { get; set; }
#nullable restore
#else
        public List<string> Tags { get; set; }
#endif
        /// <summary>MUST be a valid VRChat /file/ url.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? UserIcon { get; set; }
#nullable restore
#else
        public string UserIcon { get; set; }
#endif
        /// <summary>
        /// Instantiates a new <see cref="global::VRCZ.VRChatApi.Generated.Models.UpdateUserRequest"/> and sets the default values.
        /// </summary>
        public UpdateUserRequest()
        {
            AdditionalData = new Dictionary<string, object>();
            Status = global::VRCZ.VRChatApi.Generated.Models.UserStatus.Offline;
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::VRCZ.VRChatApi.Generated.Models.UpdateUserRequest"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::VRCZ.VRChatApi.Generated.Models.UpdateUserRequest CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::VRCZ.VRChatApi.Generated.Models.UpdateUserRequest();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "acceptedTOSVersion", n => { AcceptedTOSVersion = n.GetIntValue(); } },
                { "bio", n => { Bio = n.GetStringValue(); } },
                { "bioLinks", n => { BioLinks = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "birthday", n => { Birthday = n.GetDateValue(); } },
                { "email", n => { Email = n.GetStringValue(); } },
                { "isBoopingEnabled", n => { IsBoopingEnabled = n.GetBoolValue(); } },
                { "pronouns", n => { Pronouns = n.GetStringValue(); } },
                { "status", n => { Status = n.GetEnumValue<global::VRCZ.VRChatApi.Generated.Models.UserStatus>(); } },
                { "statusDescription", n => { StatusDescription = n.GetStringValue(); } },
                { "tags", n => { Tags = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "userIcon", n => { UserIcon = n.GetStringValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteIntValue("acceptedTOSVersion", AcceptedTOSVersion);
            writer.WriteStringValue("bio", Bio);
            writer.WriteCollectionOfPrimitiveValues<string>("bioLinks", BioLinks);
            writer.WriteDateValue("birthday", Birthday);
            writer.WriteStringValue("email", Email);
            writer.WriteBoolValue("isBoopingEnabled", IsBoopingEnabled);
            writer.WriteStringValue("pronouns", Pronouns);
            writer.WriteEnumValue<global::VRCZ.VRChatApi.Generated.Models.UserStatus>("status", Status);
            writer.WriteStringValue("statusDescription", StatusDescription);
            writer.WriteCollectionOfPrimitiveValues<string>("tags", Tags);
            writer.WriteStringValue("userIcon", UserIcon);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
