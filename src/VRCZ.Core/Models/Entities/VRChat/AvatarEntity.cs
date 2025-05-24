using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace VRCZ.Core.Models.Entities.VRChat;

[ExcludeFromCodeCoverage]
public class AvatarEntity : BaseEntityWithoutId
{
    [Key]
    public required string Id { get; set; }

    public required string Name { get; set; }
    public required string Description { get; set; }

    public required string AuthorId { get; set; }
    public required string AuthorName { get; set; }

    public required DateTimeOffset AvatarCreatedAt { get; set; }
    public required DateTimeOffset AvatarUpdatedAt { get; set; }

    public required string ImageUrl { get; set; }
    public required string ThumbnailImageUrl { get; set; }

    public string? PrimaryStyle { get; set; }
    public string? SecondaryStyle { get; set; }
}
