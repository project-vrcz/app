using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using SqlSugar;
#pragma warning disable CS8618

namespace VRCZ.Core.Models.Entities.VRChat;

[ExcludeFromCodeCoverage]
[SugarTable]
public class AvatarEntity : BaseEntityWithoutId
{
    [SugarColumn(IsPrimaryKey = true)]
    public string Id { get; set; }

    public string Name { get; set; }
    public string Description { get; set; }

    public string AuthorId { get; set; }
    public string AuthorName { get; set; }

    public DateTimeOffset AvatarCreatedAt { get; set; }
    public DateTimeOffset AvatarUpdatedAt { get; set; }

    public string ImageUrl { get; set; }
    public string ThumbnailImageUrl { get; set; }

    public string? PrimaryStyle { get; set; }
    public string? SecondaryStyle { get; set; }
}
