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
    public partial class CurrentUser : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>The acceptedPrivacyVersion property</summary>
        public int? AcceptedPrivacyVersion { get; set; }
        /// <summary>The acceptedTOSVersion property</summary>
        public int? AcceptedTOSVersion { get; set; }
        /// <summary>The accountDeletionDate property</summary>
        public Date? AccountDeletionDate { get; set; }
        /// <summary> </summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::VRCZ.VRChatApi.Generated.Models.AccountDeletionLog>? AccountDeletionLog { get; set; }
#nullable restore
#else
        public List<global::VRCZ.VRChatApi.Generated.Models.AccountDeletionLog> AccountDeletionLog { get; set; }
#endif
        /// <summary> </summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? ActiveFriends { get; set; }
#nullable restore
#else
        public List<string> ActiveFriends { get; set; }
#endif
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>`verified` is obsolete.User who have verified and are 18+ can switch to `plus18` status.</summary>
        public global::VRCZ.VRChatApi.Generated.Models.AgeVerificationStatus? AgeVerificationStatus { get; set; }
        /// <summary>`true` if, user is age verified (not 18+).</summary>
        public bool? AgeVerified { get; set; }
        /// <summary>The allowAvatarCopying property</summary>
        public bool? AllowAvatarCopying { get; set; }
        /// <summary> </summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::VRCZ.VRChatApi.Generated.Models.Badge>? Badges { get; set; }
#nullable restore
#else
        public List<global::VRCZ.VRChatApi.Generated.Models.Badge> Badges { get; set; }
#endif
        /// <summary>The bio property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Bio { get; set; }
#nullable restore
#else
        public string Bio { get; set; }
#endif
        /// <summary> </summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? BioLinks { get; set; }
#nullable restore
#else
        public List<string> BioLinks { get; set; }
#endif
        /// <summary>The currentAvatar property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? CurrentAvatar { get; set; }
#nullable restore
#else
        public string CurrentAvatar { get; set; }
#endif
        /// <summary>The currentAvatarAssetUrl property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? CurrentAvatarAssetUrl { get; set; }
#nullable restore
#else
        public string CurrentAvatarAssetUrl { get; set; }
#endif
        /// <summary>When profilePicOverride is not empty, use it instead.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? CurrentAvatarImageUrl { get; set; }
#nullable restore
#else
        public string CurrentAvatarImageUrl { get; set; }
#endif
        /// <summary>The currentAvatarTags property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? CurrentAvatarTags { get; set; }
#nullable restore
#else
        public List<string> CurrentAvatarTags { get; set; }
#endif
        /// <summary>When profilePicOverride is not empty, use it instead.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? CurrentAvatarThumbnailImageUrl { get; set; }
#nullable restore
#else
        public string CurrentAvatarThumbnailImageUrl { get; set; }
#endif
        /// <summary>The date_joined property</summary>
        public Date? DateJoined { get; set; }
        /// <summary>&quot;none&quot; User is a normal user&quot;trusted&quot; Unknown&quot;internal&quot; Is a VRChat Developer&quot;moderator&quot; Is a VRChat ModeratorStaff can hide their developerType at will.</summary>
        public global::VRCZ.VRChatApi.Generated.Models.DeveloperType? DeveloperType { get; set; }
        /// <summary>The displayName property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? DisplayName { get; set; }
#nullable restore
#else
        public string DisplayName { get; set; }
#endif
        /// <summary>The emailVerified property</summary>
        public bool? EmailVerified { get; set; }
        /// <summary>The fallbackAvatar property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? FallbackAvatar { get; set; }
#nullable restore
#else
        public string FallbackAvatar { get; set; }
#endif
        /// <summary>Always empty array.</summary>
        [Obsolete("")]
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? FriendGroupNames { get; set; }
#nullable restore
#else
        public List<string> FriendGroupNames { get; set; }
#endif
        /// <summary>The friendKey property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? FriendKey { get; set; }
#nullable restore
#else
        public string FriendKey { get; set; }
#endif
        /// <summary>The friends property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? Friends { get; set; }
#nullable restore
#else
        public List<string> Friends { get; set; }
#endif
        /// <summary>The googleDetails property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::VRCZ.VRChatApi.Generated.Models.CurrentUser_googleDetails? GoogleDetails { get; set; }
#nullable restore
#else
        public global::VRCZ.VRChatApi.Generated.Models.CurrentUser_googleDetails GoogleDetails { get; set; }
#endif
        /// <summary>The googleId property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? GoogleId { get; set; }
#nullable restore
#else
        public string GoogleId { get; set; }
#endif
        /// <summary>The hasBirthday property</summary>
        public bool? HasBirthday { get; set; }
        /// <summary>The hasEmail property</summary>
        public bool? HasEmail { get; set; }
        /// <summary>The hasLoggedInFromClient property</summary>
        public bool? HasLoggedInFromClient { get; set; }
        /// <summary>The hasPendingEmail property</summary>
        public bool? HasPendingEmail { get; set; }
        /// <summary>The hideContentFilterSettings property</summary>
        public bool? HideContentFilterSettings { get; set; }
        /// <summary>WorldID be &quot;offline&quot; on User profiles if you are not friends with that user.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? HomeLocation { get; set; }
#nullable restore
#else
        public string HomeLocation { get; set; }
#endif
        /// <summary>A users unique ID, usually in the form of `usr_c1644b5b-3ca4-45b4-97c6-a2a0de70d469`. Legacy players can have old IDs in the form of `8JoV9XEdpo`. The ID can never be changed.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Id { get; set; }
#nullable restore
#else
        public string Id { get; set; }
#endif
        /// <summary>The isAdult property</summary>
        public bool? IsAdult { get; set; }
        /// <summary>The isBoopingEnabled property</summary>
        public bool? IsBoopingEnabled { get; set; }
        /// <summary>The isFriend property</summary>
        public bool? IsFriend { get; set; }
        /// <summary>The last_activity property</summary>
        public DateTimeOffset? LastActivity { get; set; }
        /// <summary>The last_login property</summary>
        public DateTimeOffset? LastLogin { get; set; }
        /// <summary>The last_mobile property</summary>
        public DateTimeOffset? LastMobile { get; set; }
        /// <summary>This can be `standalonewindows` or `android`, but can also pretty much be any random Unity verison such as `2019.2.4-801-Release` or `2019.2.2-772-Release` or even `unknownplatform`.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? LastPlatform { get; set; }
#nullable restore
#else
        public string LastPlatform { get; set; }
#endif
        /// <summary>The obfuscatedEmail property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? ObfuscatedEmail { get; set; }
#nullable restore
#else
        public string ObfuscatedEmail { get; set; }
#endif
        /// <summary>The obfuscatedPendingEmail property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? ObfuscatedPendingEmail { get; set; }
#nullable restore
#else
        public string ObfuscatedPendingEmail { get; set; }
#endif
        /// <summary>The oculusId property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? OculusId { get; set; }
#nullable restore
#else
        public string OculusId { get; set; }
#endif
        /// <summary>The offlineFriends property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? OfflineFriends { get; set; }
#nullable restore
#else
        public List<string> OfflineFriends { get; set; }
#endif
        /// <summary>The onlineFriends property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? OnlineFriends { get; set; }
#nullable restore
#else
        public List<string> OnlineFriends { get; set; }
#endif
        /// <summary> </summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::VRCZ.VRChatApi.Generated.Models.PastDisplayName>? PastDisplayNames { get; set; }
#nullable restore
#else
        public List<global::VRCZ.VRChatApi.Generated.Models.PastDisplayName> PastDisplayNames { get; set; }
#endif
        /// <summary>The picoId property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? PicoId { get; set; }
#nullable restore
#else
        public string PicoId { get; set; }
#endif
        /// <summary>The platform_history property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::VRCZ.VRChatApi.Generated.Models.CurrentUser_platform_history>? PlatformHistory { get; set; }
#nullable restore
#else
        public List<global::VRCZ.VRChatApi.Generated.Models.CurrentUser_platform_history> PlatformHistory { get; set; }
#endif
        /// <summary>The presence property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::VRCZ.VRChatApi.Generated.Models.CurrentUserPresence? Presence { get; set; }
#nullable restore
#else
        public global::VRCZ.VRChatApi.Generated.Models.CurrentUserPresence Presence { get; set; }
#endif
        /// <summary>The profilePicOverride property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? ProfilePicOverride { get; set; }
#nullable restore
#else
        public string ProfilePicOverride { get; set; }
#endif
        /// <summary>The profilePicOverrideThumbnail property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? ProfilePicOverrideThumbnail { get; set; }
#nullable restore
#else
        public string ProfilePicOverrideThumbnail { get; set; }
#endif
        /// <summary>The pronouns property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Pronouns { get; set; }
#nullable restore
#else
        public string Pronouns { get; set; }
#endif
        /// <summary>The queuedInstance property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? QueuedInstance { get; set; }
#nullable restore
#else
        public string QueuedInstance { get; set; }
#endif
        /// <summary>The receiveMobileInvitations property</summary>
        public bool? ReceiveMobileInvitations { get; set; }
        /// <summary>* &quot;online&quot; User is online in VRChat* &quot;active&quot; User is online, but not in VRChat* &quot;offline&quot; User is offlineAlways offline when returned through `getCurrentUser` (/auth/user).</summary>
        public global::VRCZ.VRChatApi.Generated.Models.UserState? State { get; set; }
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
        /// <summary>The statusFirstTime property</summary>
        public bool? StatusFirstTime { get; set; }
        /// <summary>The statusHistory property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? StatusHistory { get; set; }
#nullable restore
#else
        public List<string> StatusHistory { get; set; }
#endif
        /// <summary>The steamDetails property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::VRCZ.VRChatApi.Generated.Models.CurrentUser_steamDetails? SteamDetails { get; set; }
#nullable restore
#else
        public global::VRCZ.VRChatApi.Generated.Models.CurrentUser_steamDetails SteamDetails { get; set; }
#endif
        /// <summary>The steamId property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? SteamId { get; set; }
#nullable restore
#else
        public string SteamId { get; set; }
#endif
        /// <summary>The tags property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? Tags { get; set; }
#nullable restore
#else
        public List<string> Tags { get; set; }
#endif
        /// <summary>The twoFactorAuthEnabled property</summary>
        public bool? TwoFactorAuthEnabled { get; set; }
        /// <summary>The twoFactorAuthEnabledDate property</summary>
        public DateTimeOffset? TwoFactorAuthEnabledDate { get; set; }
        /// <summary>The unsubscribe property</summary>
        public bool? Unsubscribe { get; set; }
        /// <summary>The updated_at property</summary>
        public DateTimeOffset? UpdatedAt { get; set; }
        /// <summary>The userIcon property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? UserIcon { get; set; }
#nullable restore
#else
        public string UserIcon { get; set; }
#endif
        /// <summary>The userLanguage property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? UserLanguage { get; set; }
#nullable restore
#else
        public string UserLanguage { get; set; }
#endif
        /// <summary>The userLanguageCode property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? UserLanguageCode { get; set; }
#nullable restore
#else
        public string UserLanguageCode { get; set; }
#endif
        /// <summary>-| **DEPRECATED:** VRChat API no longer return usernames of other users. [See issue by Tupper for more information](https://github.com/pypy-vrc/VRCX/issues/429).</summary>
        [Obsolete("")]
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Username { get; set; }
#nullable restore
#else
        public string Username { get; set; }
#endif
        /// <summary>The viveId property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? ViveId { get; set; }
#nullable restore
#else
        public string ViveId { get; set; }
#endif
        /// <summary>
        /// Instantiates a new <see cref="global::VRCZ.VRChatApi.Generated.Models.CurrentUser"/> and sets the default values.
        /// </summary>
        public CurrentUser()
        {
            AdditionalData = new Dictionary<string, object>();
            DeveloperType = global::VRCZ.VRChatApi.Generated.Models.DeveloperType.None;
            State = global::VRCZ.VRChatApi.Generated.Models.UserState.Offline;
            Status = global::VRCZ.VRChatApi.Generated.Models.UserStatus.Offline;
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::VRCZ.VRChatApi.Generated.Models.CurrentUser"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::VRCZ.VRChatApi.Generated.Models.CurrentUser CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::VRCZ.VRChatApi.Generated.Models.CurrentUser();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "acceptedPrivacyVersion", n => { AcceptedPrivacyVersion = n.GetIntValue(); } },
                { "acceptedTOSVersion", n => { AcceptedTOSVersion = n.GetIntValue(); } },
                { "accountDeletionDate", n => { AccountDeletionDate = n.GetDateValue(); } },
                { "accountDeletionLog", n => { AccountDeletionLog = n.GetCollectionOfObjectValues<global::VRCZ.VRChatApi.Generated.Models.AccountDeletionLog>(global::VRCZ.VRChatApi.Generated.Models.AccountDeletionLog.CreateFromDiscriminatorValue)?.AsList(); } },
                { "activeFriends", n => { ActiveFriends = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "ageVerificationStatus", n => { AgeVerificationStatus = n.GetEnumValue<global::VRCZ.VRChatApi.Generated.Models.AgeVerificationStatus>(); } },
                { "ageVerified", n => { AgeVerified = n.GetBoolValue(); } },
                { "allowAvatarCopying", n => { AllowAvatarCopying = n.GetBoolValue(); } },
                { "badges", n => { Badges = n.GetCollectionOfObjectValues<global::VRCZ.VRChatApi.Generated.Models.Badge>(global::VRCZ.VRChatApi.Generated.Models.Badge.CreateFromDiscriminatorValue)?.AsList(); } },
                { "bio", n => { Bio = n.GetStringValue(); } },
                { "bioLinks", n => { BioLinks = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "currentAvatar", n => { CurrentAvatar = n.GetStringValue(); } },
                { "currentAvatarAssetUrl", n => { CurrentAvatarAssetUrl = n.GetStringValue(); } },
                { "currentAvatarImageUrl", n => { CurrentAvatarImageUrl = n.GetStringValue(); } },
                { "currentAvatarTags", n => { CurrentAvatarTags = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "currentAvatarThumbnailImageUrl", n => { CurrentAvatarThumbnailImageUrl = n.GetStringValue(); } },
                { "date_joined", n => { DateJoined = n.GetDateValue(); } },
                { "developerType", n => { DeveloperType = n.GetEnumValue<global::VRCZ.VRChatApi.Generated.Models.DeveloperType>(); } },
                { "displayName", n => { DisplayName = n.GetStringValue(); } },
                { "emailVerified", n => { EmailVerified = n.GetBoolValue(); } },
                { "fallbackAvatar", n => { FallbackAvatar = n.GetStringValue(); } },
                { "friendGroupNames", n => { FriendGroupNames = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "friendKey", n => { FriendKey = n.GetStringValue(); } },
                { "friends", n => { Friends = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "googleDetails", n => { GoogleDetails = n.GetObjectValue<global::VRCZ.VRChatApi.Generated.Models.CurrentUser_googleDetails>(global::VRCZ.VRChatApi.Generated.Models.CurrentUser_googleDetails.CreateFromDiscriminatorValue); } },
                { "googleId", n => { GoogleId = n.GetStringValue(); } },
                { "hasBirthday", n => { HasBirthday = n.GetBoolValue(); } },
                { "hasEmail", n => { HasEmail = n.GetBoolValue(); } },
                { "hasLoggedInFromClient", n => { HasLoggedInFromClient = n.GetBoolValue(); } },
                { "hasPendingEmail", n => { HasPendingEmail = n.GetBoolValue(); } },
                { "hideContentFilterSettings", n => { HideContentFilterSettings = n.GetBoolValue(); } },
                { "homeLocation", n => { HomeLocation = n.GetStringValue(); } },
                { "id", n => { Id = n.GetStringValue(); } },
                { "isAdult", n => { IsAdult = n.GetBoolValue(); } },
                { "isBoopingEnabled", n => { IsBoopingEnabled = n.GetBoolValue(); } },
                { "isFriend", n => { IsFriend = n.GetBoolValue(); } },
                { "last_activity", n => { LastActivity = n.GetDateTimeOffsetValue(); } },
                { "last_login", n => { LastLogin = n.GetDateTimeOffsetValue(); } },
                { "last_mobile", n => { LastMobile = n.GetDateTimeOffsetValue(); } },
                { "last_platform", n => { LastPlatform = n.GetStringValue(); } },
                { "obfuscatedEmail", n => { ObfuscatedEmail = n.GetStringValue(); } },
                { "obfuscatedPendingEmail", n => { ObfuscatedPendingEmail = n.GetStringValue(); } },
                { "oculusId", n => { OculusId = n.GetStringValue(); } },
                { "offlineFriends", n => { OfflineFriends = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "onlineFriends", n => { OnlineFriends = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "pastDisplayNames", n => { PastDisplayNames = n.GetCollectionOfObjectValues<global::VRCZ.VRChatApi.Generated.Models.PastDisplayName>(global::VRCZ.VRChatApi.Generated.Models.PastDisplayName.CreateFromDiscriminatorValue)?.AsList(); } },
                { "picoId", n => { PicoId = n.GetStringValue(); } },
                { "platform_history", n => { PlatformHistory = n.GetCollectionOfObjectValues<global::VRCZ.VRChatApi.Generated.Models.CurrentUser_platform_history>(global::VRCZ.VRChatApi.Generated.Models.CurrentUser_platform_history.CreateFromDiscriminatorValue)?.AsList(); } },
                { "presence", n => { Presence = n.GetObjectValue<global::VRCZ.VRChatApi.Generated.Models.CurrentUserPresence>(global::VRCZ.VRChatApi.Generated.Models.CurrentUserPresence.CreateFromDiscriminatorValue); } },
                { "profilePicOverride", n => { ProfilePicOverride = n.GetStringValue(); } },
                { "profilePicOverrideThumbnail", n => { ProfilePicOverrideThumbnail = n.GetStringValue(); } },
                { "pronouns", n => { Pronouns = n.GetStringValue(); } },
                { "queuedInstance", n => { QueuedInstance = n.GetStringValue(); } },
                { "receiveMobileInvitations", n => { ReceiveMobileInvitations = n.GetBoolValue(); } },
                { "state", n => { State = n.GetEnumValue<global::VRCZ.VRChatApi.Generated.Models.UserState>(); } },
                { "status", n => { Status = n.GetEnumValue<global::VRCZ.VRChatApi.Generated.Models.UserStatus>(); } },
                { "statusDescription", n => { StatusDescription = n.GetStringValue(); } },
                { "statusFirstTime", n => { StatusFirstTime = n.GetBoolValue(); } },
                { "statusHistory", n => { StatusHistory = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "steamDetails", n => { SteamDetails = n.GetObjectValue<global::VRCZ.VRChatApi.Generated.Models.CurrentUser_steamDetails>(global::VRCZ.VRChatApi.Generated.Models.CurrentUser_steamDetails.CreateFromDiscriminatorValue); } },
                { "steamId", n => { SteamId = n.GetStringValue(); } },
                { "tags", n => { Tags = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "twoFactorAuthEnabled", n => { TwoFactorAuthEnabled = n.GetBoolValue(); } },
                { "twoFactorAuthEnabledDate", n => { TwoFactorAuthEnabledDate = n.GetDateTimeOffsetValue(); } },
                { "unsubscribe", n => { Unsubscribe = n.GetBoolValue(); } },
                { "updated_at", n => { UpdatedAt = n.GetDateTimeOffsetValue(); } },
                { "userIcon", n => { UserIcon = n.GetStringValue(); } },
                { "userLanguage", n => { UserLanguage = n.GetStringValue(); } },
                { "userLanguageCode", n => { UserLanguageCode = n.GetStringValue(); } },
                { "username", n => { Username = n.GetStringValue(); } },
                { "viveId", n => { ViveId = n.GetStringValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteIntValue("acceptedPrivacyVersion", AcceptedPrivacyVersion);
            writer.WriteIntValue("acceptedTOSVersion", AcceptedTOSVersion);
            writer.WriteDateValue("accountDeletionDate", AccountDeletionDate);
            writer.WriteCollectionOfObjectValues<global::VRCZ.VRChatApi.Generated.Models.AccountDeletionLog>("accountDeletionLog", AccountDeletionLog);
            writer.WriteCollectionOfPrimitiveValues<string>("activeFriends", ActiveFriends);
            writer.WriteEnumValue<global::VRCZ.VRChatApi.Generated.Models.AgeVerificationStatus>("ageVerificationStatus", AgeVerificationStatus);
            writer.WriteBoolValue("ageVerified", AgeVerified);
            writer.WriteBoolValue("allowAvatarCopying", AllowAvatarCopying);
            writer.WriteCollectionOfObjectValues<global::VRCZ.VRChatApi.Generated.Models.Badge>("badges", Badges);
            writer.WriteStringValue("bio", Bio);
            writer.WriteCollectionOfPrimitiveValues<string>("bioLinks", BioLinks);
            writer.WriteStringValue("currentAvatar", CurrentAvatar);
            writer.WriteStringValue("currentAvatarAssetUrl", CurrentAvatarAssetUrl);
            writer.WriteStringValue("currentAvatarImageUrl", CurrentAvatarImageUrl);
            writer.WriteCollectionOfPrimitiveValues<string>("currentAvatarTags", CurrentAvatarTags);
            writer.WriteStringValue("currentAvatarThumbnailImageUrl", CurrentAvatarThumbnailImageUrl);
            writer.WriteDateValue("date_joined", DateJoined);
            writer.WriteEnumValue<global::VRCZ.VRChatApi.Generated.Models.DeveloperType>("developerType", DeveloperType);
            writer.WriteStringValue("displayName", DisplayName);
            writer.WriteBoolValue("emailVerified", EmailVerified);
            writer.WriteStringValue("fallbackAvatar", FallbackAvatar);
            writer.WriteCollectionOfPrimitiveValues<string>("friendGroupNames", FriendGroupNames);
            writer.WriteStringValue("friendKey", FriendKey);
            writer.WriteCollectionOfPrimitiveValues<string>("friends", Friends);
            writer.WriteObjectValue<global::VRCZ.VRChatApi.Generated.Models.CurrentUser_googleDetails>("googleDetails", GoogleDetails);
            writer.WriteStringValue("googleId", GoogleId);
            writer.WriteBoolValue("hasBirthday", HasBirthday);
            writer.WriteBoolValue("hasEmail", HasEmail);
            writer.WriteBoolValue("hasLoggedInFromClient", HasLoggedInFromClient);
            writer.WriteBoolValue("hasPendingEmail", HasPendingEmail);
            writer.WriteBoolValue("hideContentFilterSettings", HideContentFilterSettings);
            writer.WriteStringValue("homeLocation", HomeLocation);
            writer.WriteStringValue("id", Id);
            writer.WriteBoolValue("isAdult", IsAdult);
            writer.WriteBoolValue("isBoopingEnabled", IsBoopingEnabled);
            writer.WriteBoolValue("isFriend", IsFriend);
            writer.WriteDateTimeOffsetValue("last_activity", LastActivity);
            writer.WriteDateTimeOffsetValue("last_login", LastLogin);
            writer.WriteDateTimeOffsetValue("last_mobile", LastMobile);
            writer.WriteStringValue("last_platform", LastPlatform);
            writer.WriteStringValue("obfuscatedEmail", ObfuscatedEmail);
            writer.WriteStringValue("obfuscatedPendingEmail", ObfuscatedPendingEmail);
            writer.WriteStringValue("oculusId", OculusId);
            writer.WriteCollectionOfPrimitiveValues<string>("offlineFriends", OfflineFriends);
            writer.WriteCollectionOfPrimitiveValues<string>("onlineFriends", OnlineFriends);
            writer.WriteCollectionOfObjectValues<global::VRCZ.VRChatApi.Generated.Models.PastDisplayName>("pastDisplayNames", PastDisplayNames);
            writer.WriteStringValue("picoId", PicoId);
            writer.WriteCollectionOfObjectValues<global::VRCZ.VRChatApi.Generated.Models.CurrentUser_platform_history>("platform_history", PlatformHistory);
            writer.WriteObjectValue<global::VRCZ.VRChatApi.Generated.Models.CurrentUserPresence>("presence", Presence);
            writer.WriteStringValue("profilePicOverride", ProfilePicOverride);
            writer.WriteStringValue("profilePicOverrideThumbnail", ProfilePicOverrideThumbnail);
            writer.WriteStringValue("pronouns", Pronouns);
            writer.WriteStringValue("queuedInstance", QueuedInstance);
            writer.WriteBoolValue("receiveMobileInvitations", ReceiveMobileInvitations);
            writer.WriteEnumValue<global::VRCZ.VRChatApi.Generated.Models.UserState>("state", State);
            writer.WriteEnumValue<global::VRCZ.VRChatApi.Generated.Models.UserStatus>("status", Status);
            writer.WriteStringValue("statusDescription", StatusDescription);
            writer.WriteBoolValue("statusFirstTime", StatusFirstTime);
            writer.WriteCollectionOfPrimitiveValues<string>("statusHistory", StatusHistory);
            writer.WriteObjectValue<global::VRCZ.VRChatApi.Generated.Models.CurrentUser_steamDetails>("steamDetails", SteamDetails);
            writer.WriteStringValue("steamId", SteamId);
            writer.WriteCollectionOfPrimitiveValues<string>("tags", Tags);
            writer.WriteBoolValue("twoFactorAuthEnabled", TwoFactorAuthEnabled);
            writer.WriteDateTimeOffsetValue("twoFactorAuthEnabledDate", TwoFactorAuthEnabledDate);
            writer.WriteBoolValue("unsubscribe", Unsubscribe);
            writer.WriteDateTimeOffsetValue("updated_at", UpdatedAt);
            writer.WriteStringValue("userIcon", UserIcon);
            writer.WriteStringValue("userLanguage", UserLanguage);
            writer.WriteStringValue("userLanguageCode", UserLanguageCode);
            writer.WriteStringValue("username", Username);
            writer.WriteStringValue("viveId", ViveId);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
