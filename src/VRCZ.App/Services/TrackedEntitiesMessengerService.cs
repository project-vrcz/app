using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Hosting;
using VRCZ.App.ViewMessages.TrackedEntities;
using VRCZ.Core.Services.Tracking;

namespace VRCZ.App.Services;

public class TrackedEntitiesMessengerService(
    VRChatTrackedEntitiesService trackedEntitiesService,
    WeakReferenceMessenger weakReferenceMessenger) : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        trackedEntitiesService.LoggedInUserUpdated += (_, user) =>
        {
            weakReferenceMessenger.Send(new LoggedInUserUpdatedMessage(user));
        };

        trackedEntitiesService.FriendAdded += (_, user) =>
        {
            weakReferenceMessenger.Send(new FriendAddedMessage(user));
        };

        trackedEntitiesService.FriendRemoved += (_, user) =>
        {
            weakReferenceMessenger.Send(new FriendRemovedMessage(user));
        };

        trackedEntitiesService.FriendUpdated += (_, user) =>
        {
            weakReferenceMessenger.Send(new FriendUpdateEvent(user));
        };

        trackedEntitiesService.UserLocationUpdated += (_, args) =>
        {
            weakReferenceMessenger.Send(new UserLocationUpdatedMessage(args.UserId, args.UserLocation));
        };

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
