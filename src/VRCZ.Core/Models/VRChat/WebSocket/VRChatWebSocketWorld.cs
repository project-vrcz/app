using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using VRCZ.VRChatApi.Generated.Models;

namespace VRCZ.Core.Models.VRChat.WebSocket;

[ExcludeFromCodeCoverage]
public class VRChatWebSocketWorld
{
    [JsonPropertyName("id")] public required string Id { get; set; }
    [JsonPropertyName("name")] public string? Name { get; set; }
    [JsonPropertyName("description")] public string? Description { get; set; }
    [JsonPropertyName("authorId")] public string? AuthorId { get; set; }
    [JsonPropertyName("authorName")] public string? AuthorName { get; set; }
    [JsonPropertyName("featured")] public bool? Featured { get; set; }
    [JsonPropertyName("capacity")] public long? Capacity { get; set; }
    [JsonPropertyName("recommendedCapacity")]
    public long? RecommendedCapacity { get; set; }
    [JsonPropertyName("imageUrl")] public string? ImageUrl { get; set; }
    [JsonPropertyName("thumbnailImageUrl")]
    public string? ThumbnailImageUrl { get; set; }
    [JsonPropertyName("version")] public long? Version { get; set; }
    [JsonPropertyName("organization")] public string? Organization { get; set; }
    [JsonPropertyName("previewYoutubeId")] public string? PreviewYoutubeId { get; set; }
    [JsonPropertyName("urlList")] public string[]? UrlList { get; set; }
    [JsonPropertyName("favorites")] public long? Favorites { get; set; }
    [JsonPropertyName("visits")] public long? Visits { get; set; }
    [JsonPropertyName("popularity")] public long? Popularity { get; set; }
    [JsonPropertyName("heat")] public long? Heat { get; set; }
    [JsonPropertyName("publicationDate")] public string? PublicationDate { get; set; }
    [JsonPropertyName("publicOccupants")] public long? PublicOccupants { get; set; }
    [JsonPropertyName("privateOccupants")] public long? PrivateOccupants { get; set; }
    [JsonPropertyName("occupants")] public long? Occupants { get; set; }
    [JsonPropertyName("tags")] public string[]? Tags { get; set; }
    [JsonPropertyName("created_at")] public DateTimeOffset? CreatedAt { get; set; }
    [JsonPropertyName("updated_at")] public DateTimeOffset? UpdatedAt { get; set; }
}
