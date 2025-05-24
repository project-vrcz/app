using VRCZ.Core.Services;

namespace VRCZ.Core.Tests.Unit.Services;

public class MessengerServiceTest
{
    [Fact]
    public void Register_ShouldAddSubscriber()
    {
        // Arrange
        var messenger = new MessengerService();
        var called = false;
        Action<string> action = _ => called = true;

        // Act
        messenger.Register(action);
        messenger.Send("Test");

        // Assert
        Assert.True(called);
    }

    // TODO: Implement Unregister test

    // [Fact]
    // public void Unregister_ShouldRemoveSubscriber()
    // {
    //     // Arrange
    //     var messenger = new MessengerService();
    //     var called = false;
    //     Action<string> action = _ => called = true;
    //
    //     messenger.Register(action);
    //     messenger.Unregister(action);
    //
    //     // Act
    //     messenger.Send("Test");
    //
    //     // Assert
    //     Assert.False(called);
    // }

    [Fact]
    public void Send_ShouldThrowArgumentNullException_WhenMessageIsNull()
    {
        // Arrange
        var messenger = new MessengerService();

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => messenger.Send<string>(null!));
    }
}
