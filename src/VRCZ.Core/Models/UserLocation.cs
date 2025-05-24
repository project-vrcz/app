namespace VRCZ.Core.Models;

public record UserLocation(
    UserLocationType LocationType,
    string? WorldId = null,
    string? InstanceId = null)
{
    public static UserLocation Parse(string locationString)
    {
        switch (locationString)
        {
            case "offline":
                return new UserLocation(UserLocationType.Offline);
            case "private":
                return new UserLocation(UserLocationType.Private);
            case "traveling":
                return new UserLocation(UserLocationType.Traveling);
        }


        var locationStringParts = locationString.Split(':');
        if (locationStringParts.Length != 2 || string.IsNullOrWhiteSpace(locationStringParts[0]) || string.IsNullOrWhiteSpace(locationStringParts[1]))
            throw new ArgumentException("Invalid location string", nameof(locationString));

        if (locationStringParts[0] == "offline" || locationStringParts[1] == "offline")
        {
            return new UserLocation(UserLocationType.Offline);
        }

        if (locationStringParts[0] == "private" || locationStringParts[1] == "private")
        {
            return new UserLocation(UserLocationType.Private);
        }

        if (locationStringParts[0] == "traveling" || locationStringParts[1] == "traveling")
        {
            return new UserLocation(UserLocationType.Traveling);
        }

        return new UserLocation(UserLocationType.Online, locationStringParts[0], locationStringParts[1]);
    }

    public string ToLocationString()
    {
        return LocationType switch
        {
            UserLocationType.Offline => "offline",
            UserLocationType.Private => "private",
            UserLocationType.Traveling => "traveling",
            UserLocationType.Online => $"{WorldId}:{InstanceId}",
            UserLocationType.Unknown => "offline",
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}

public enum UserLocationType
{
    Unknown,
    Traveling,
    Private,
    Online,
    Offline
}
