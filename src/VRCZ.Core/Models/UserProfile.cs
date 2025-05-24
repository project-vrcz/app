using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Text.Json.Serialization;

namespace VRCZ.Core.Models;

[ExcludeFromCodeCoverage]
public class UserProfile
{
    public string Id { get; set; } = "";
    public string DisplayName { get; set; } = "";
    public string Username { get; set; } = "";
    public string AvatarUrl { get; set; } = "";
}

[ExcludeFromCodeCoverage]
public class UserProfileSecret
{
    public List<Cookie> Cookies { get; set; } = [];
    public required string Username { get; set; }
    public required string Password { get; set; }
}

[JsonSerializable(typeof(UserProfile))]
[JsonSerializable(typeof(UserProfileSecret))]
public partial class UserProfileContext : JsonSerializerContext
{
}
