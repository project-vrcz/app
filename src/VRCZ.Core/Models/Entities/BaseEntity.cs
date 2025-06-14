using System.Diagnostics.CodeAnalysis;
using SqlSugar;

namespace VRCZ.Core.Models.Entities;

[ExcludeFromCodeCoverage]
public abstract class BaseEntity : BaseEntityWithoutId
{
    [SugarColumn(IsPrimaryKey = true)]
    public Guid Id { get; set; } = Guid.CreateVersion7();
}

[ExcludeFromCodeCoverage]
public abstract class BaseEntityWithoutId
{
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.Now;
}
