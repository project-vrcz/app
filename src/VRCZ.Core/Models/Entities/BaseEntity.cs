using System.ComponentModel.DataAnnotations;

namespace VRCZ.Core.Models.Entities;

public abstract class BaseEntity : BaseEntityWithoutId
{
    [Key]
    public Guid Id { get; set; } = Guid.CreateVersion7();
}

public abstract class BaseEntityWithoutId
{
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.Now;
}
