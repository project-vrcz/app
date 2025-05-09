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
    public partial class TransactionSteamInfo : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>Steam Order ID</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? OrderId { get; set; }
#nullable restore
#else
        public string OrderId { get; set; }
#endif
        /// <summary>Steam User ID</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? SteamId { get; set; }
#nullable restore
#else
        public string SteamId { get; set; }
#endif
        /// <summary>Empty</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? SteamUrl { get; set; }
#nullable restore
#else
        public string SteamUrl { get; set; }
#endif
        /// <summary>Steam Transaction ID, NOT the same as VRChat TransactionID</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? TransId { get; set; }
#nullable restore
#else
        public string TransId { get; set; }
#endif
        /// <summary>The walletInfo property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::VRCZ.VRChatApi.Generated.Models.TransactionSteamWalletInfo? WalletInfo { get; set; }
#nullable restore
#else
        public global::VRCZ.VRChatApi.Generated.Models.TransactionSteamWalletInfo WalletInfo { get; set; }
#endif
        /// <summary>
        /// Instantiates a new <see cref="global::VRCZ.VRChatApi.Generated.Models.TransactionSteamInfo"/> and sets the default values.
        /// </summary>
        public TransactionSteamInfo()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::VRCZ.VRChatApi.Generated.Models.TransactionSteamInfo"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::VRCZ.VRChatApi.Generated.Models.TransactionSteamInfo CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::VRCZ.VRChatApi.Generated.Models.TransactionSteamInfo();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "orderId", n => { OrderId = n.GetStringValue(); } },
                { "steamId", n => { SteamId = n.GetStringValue(); } },
                { "steamUrl", n => { SteamUrl = n.GetStringValue(); } },
                { "transId", n => { TransId = n.GetStringValue(); } },
                { "walletInfo", n => { WalletInfo = n.GetObjectValue<global::VRCZ.VRChatApi.Generated.Models.TransactionSteamWalletInfo>(global::VRCZ.VRChatApi.Generated.Models.TransactionSteamWalletInfo.CreateFromDiscriminatorValue); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteStringValue("orderId", OrderId);
            writer.WriteStringValue("steamId", SteamId);
            writer.WriteStringValue("steamUrl", SteamUrl);
            writer.WriteStringValue("transId", TransId);
            writer.WriteObjectValue<global::VRCZ.VRChatApi.Generated.Models.TransactionSteamWalletInfo>("walletInfo", WalletInfo);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
