using System.Globalization;

namespace VRCZ.Core.Shared.Attributes;

[AttributeUsage(AttributeTargets.Assembly)]
public sealed class BuildDateTimeAttribute(string value) : Attribute
{
    public DateTimeOffset DateTime { get; } =
        DateTimeOffset.FromUnixTimeSeconds(long.Parse(value, CultureInfo.InvariantCulture));
}