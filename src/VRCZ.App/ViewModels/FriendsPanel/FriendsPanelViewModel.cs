using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Messaging;
using VRCZ.App.Services;
using VRCZ.App.ViewMessages.TrackedEntities;
using VRCZ.Core.Services;

namespace VRCZ.App.ViewModels.FriendsPanel;

public class FriendsPanelViewModel : ViewModelBase
{
    public ObservableCollection<FriendItemViewModel> Friends { get; }

    private readonly WeakReferenceMessenger _weakReferenceMessenger;

    public FriendsPanelViewModel(VRChatTrackedEntitiesService trackedEntitiesService,
        WeakReferenceMessenger weakReferenceMessenger)
    {
        _weakReferenceMessenger = weakReferenceMessenger;
        var friends = trackedEntitiesService.GetFriends()
            .Select(user =>
                new FriendItemViewModel(weakReferenceMessenger, user, trackedEntitiesService))
            .ToArray();

        Friends = new ObservableCollection<FriendItemViewModel>(friends);

        weakReferenceMessenger.Register<FriendsPanelViewModel, FriendAddedMessage>(this,
            (r, message) =>
            {
                if (r.Friends.Any(f => f.User.Id == message.Value.Id))
                    return;

                r.Friends.Add(new FriendItemViewModel(r._weakReferenceMessenger, message.Value, trackedEntitiesService));
            });

        weakReferenceMessenger.Register<FriendsPanelViewModel, FriendRemovedMessage>(this, (r, message) =>
        {
            if (r.Friends.FirstOrDefault(f => f.User.Id == message.Value.Id) is not { } friend)
                return;

            r.Friends.Remove(friend);
        });
    }
}
