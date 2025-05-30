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
    public partial class APIConfig_reportOptions_world : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The environment property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? Environment { get; set; }
#nullable restore
#else
        public List<string> Environment { get; set; }
#endif
        /// <summary>The text property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? Text { get; set; }
#nullable restore
#else
        public List<string> Text { get; set; }
#endif
        /// <summary>The warnings property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? Warnings { get; set; }
#nullable restore
#else
        public List<string> Warnings { get; set; }
#endif
        /// <summary>The worldimage property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? Worldimage { get; set; }
#nullable restore
#else
        public List<string> Worldimage { get; set; }
#endif
        /// <summary>The worldstore property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? Worldstore { get; set; }
#nullable restore
#else
        public List<string> Worldstore { get; set; }
#endif
        /// <summary>
        /// Instantiates a new <see cref="global::VRCZ.VRChatApi.Generated.Models.APIConfig_reportOptions_world"/> and sets the default values.
        /// </summary>
        public APIConfig_reportOptions_world()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::VRCZ.VRChatApi.Generated.Models.APIConfig_reportOptions_world"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::VRCZ.VRChatApi.Generated.Models.APIConfig_reportOptions_world CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::VRCZ.VRChatApi.Generated.Models.APIConfig_reportOptions_world();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "environment", n => { Environment = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "text", n => { Text = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "warnings", n => { Warnings = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "worldimage", n => { Worldimage = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "worldstore", n => { Worldstore = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteCollectionOfPrimitiveValues<string>("environment", Environment);
            writer.WriteCollectionOfPrimitiveValues<string>("text", Text);
            writer.WriteCollectionOfPrimitiveValues<string>("warnings", Warnings);
            writer.WriteCollectionOfPrimitiveValues<string>("worldimage", Worldimage);
            writer.WriteCollectionOfPrimitiveValues<string>("worldstore", Worldstore);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
