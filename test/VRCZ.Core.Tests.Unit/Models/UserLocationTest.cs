using VRCZ.Core.Models;

namespace VRCZ.Core.Tests.Unit.Models;

public class UserLocationTest
{
    private const string OfflineLocationString = "offline";
    private const string PrivateLocationString = "private";
    private const string TravelingLocationString = "traveling";

    private const string InvalidLocationString = "invalid-location";
    private const string InvalidEmptyLocationStringWithSingleColon = ":";
    private const string InvalidLocationStringWithDoubleColon = "invalid::location";

    private const string WorldId = "wrld_dcea41a7-d0ae-4daf-8edb-1b42f3588d02";
    private const string InstanceId = "hidden(usr_99de39a3-b1ef-4579-af5c-fc28b8c1c6bc)";

    private const string LocationString =
        $"{WorldId}:{InstanceId}";

    #region Parse

    [Fact]
    public void Parse_Offline_ReturnsOfflineLocation()
    {
        // Arrange & Act
        var location = UserLocation.Parse(OfflineLocationString);

        // Assert
        Assert.Equal(UserLocationType.Offline, location.LocationType);

        Assert.Null(location.WorldId);
        Assert.Null(location.InstanceId);
    }

    [Fact]
    public void Parse_OfflineDouble_ReturnsOfflineLocation()
    {
        // Arrange & Act
        var location = UserLocation.Parse("offline:offline");

        // Assert
        Assert.Equal(UserLocationType.Offline, location.LocationType);
        Assert.Null(location.WorldId);
        Assert.Null(location.InstanceId);
    }

    [Fact]
    public void Parse_Private_ReturnsPrivateLocation()
    {
        // Arrange & Act
        var location = UserLocation.Parse(PrivateLocationString);

        // Assert
        Assert.Equal(UserLocationType.Private, location.LocationType);
        Assert.Null(location.WorldId);
        Assert.Null(location.InstanceId);
    }

    [Fact]
    public void Parse_PrivateDouble_ReturnsPrivateLocation()
    {
        // Arrange & Act
        var location = UserLocation.Parse("private:private");

        // Assert
        Assert.Equal(UserLocationType.Private, location.LocationType);
        Assert.Null(location.WorldId);
        Assert.Null(location.InstanceId);
    }

    [Fact]
    public void Parse_Traveling_ReturnsTravelingLocation()
    {
        // Arrange & Act
        var location = UserLocation.Parse(TravelingLocationString);

        // Assert
        Assert.Equal(UserLocationType.Traveling, location.LocationType);
        Assert.Null(location.WorldId);
        Assert.Null(location.InstanceId);
    }

    [Fact]
    public void Parse_TravelingDouble_ReturnsTravelingLocation()
    {
        // Arrange & Act
        var location = UserLocation.Parse("traveling:traveling");

        // Assert
        Assert.Equal(UserLocationType.Traveling, location.LocationType);
        Assert.Null(location.WorldId);
        Assert.Null(location.InstanceId);
    }

    [Fact]
    public void Parse_ValidLocationString_ReturnsOnlineLocation()
    {
        // Arrange & Act
        var location = UserLocation.Parse(LocationString);

        // Assert
        Assert.Equal(UserLocationType.Online, location.LocationType);
        Assert.Equal(WorldId, location.WorldId);
        Assert.Equal(InstanceId, location.InstanceId);
    }

    [Fact]
    public void Parse_InvalidLocationString_ThrowsArgumentException()
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentException>(() => UserLocation.Parse(InvalidLocationString));
    }

    [Fact]
    public void Parse_InvalidLocationStringWithSingleColon_ThrowsArgumentException()
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentException>(() => UserLocation.Parse(InvalidEmptyLocationStringWithSingleColon));
    }

    [Fact]
    public void Parse_InvalidLocationStringWithDoubleColon_ThrowsArgumentException()
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentException>(() => UserLocation.Parse(InvalidLocationStringWithDoubleColon));
    }

    #endregion

    #region ToLocationString

    [Fact]
    public void ToLocationString_Offline_ReturnsOfflineString()
    {
        // Arrange
        var location = new UserLocation(UserLocationType.Offline);

        // Act
        var locationString = location.ToLocationString();

        // Assert
        Assert.Equal(OfflineLocationString, locationString);
    }

    [Fact]
    public void ToLocationString_Private_ReturnsPrivateString()
    {
        // Arrange
        var location = new UserLocation(UserLocationType.Private);

        // Act
        var locationString = location.ToLocationString();

        // Assert
        Assert.Equal(PrivateLocationString, locationString);
    }

    [Fact]
    public void ToLocationString_Traveling_ReturnsTravelingString()
    {
        // Arrange
        var location = new UserLocation(UserLocationType.Traveling);

        // Act
        var locationString = location.ToLocationString();

        // Assert
        Assert.Equal(TravelingLocationString, locationString);
    }

    [Fact]
    public void ToLocationString_Online_ReturnsOnlineString()
    {
        // Arrange
        var location = new UserLocation(UserLocationType.Online, WorldId, InstanceId);

        // Act
        var locationString = location.ToLocationString();

        // Assert
        Assert.Equal(LocationString, locationString);
    }

    [Fact]
    public void ToLocationString_Unknown_ReturnsOfflineString()
    {
        // Arrange
        var location = new UserLocation(UserLocationType.Unknown);

        // Act
        var locationString = location.ToLocationString();

        // Assert
        Assert.Equal(OfflineLocationString, locationString);
    }

    [Fact]
    public void ToLocationString_UnknownWithWorldAndInstance_ReturnsOfflineString()
    {
        // Arrange
        var location = new UserLocation(UserLocationType.Unknown, WorldId, InstanceId);

        // Act
        var locationString = location.ToLocationString();

        // Assert
        Assert.Equal(OfflineLocationString, locationString);
    }

    [Fact]
    public void ToLocationString_OutOfRange_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var location = new UserLocation((UserLocationType)999, WorldId, InstanceId);

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => location.ToLocationString());
    }

    #endregion
}
