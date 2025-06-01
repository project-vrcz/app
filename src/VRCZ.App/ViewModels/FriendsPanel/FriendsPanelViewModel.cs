using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Messaging;
using VRCZ.App.ViewMessages.TrackedEntities;
using VRCZ.App.ViewModels.Entities;
using VRCZ.Core.Services.Tracking;

namespace VRCZ.App.ViewModels.FriendsPanel;

public class FriendsPanelViewModel : ViewModelBase
{
    public ObservableCollection<FriendUserViewModel> Friends { get; }

    public FriendsPanelViewModel(
        VRChatTrackedEntitiesService trackedEntitiesService,
        WeakReferenceMessenger weakReferenceMessenger,
        FriendUserViewModelFactory friendUserViewModelFactory
        )
    {
        var friends = trackedEntitiesService.GetFriends()
            .Select(friendUserViewModelFactory.Create)
            .ToArray();

        Friends = new ObservableCollection<FriendUserViewModel>(friends);

        weakReferenceMessenger.Register<FriendsPanelViewModel, FriendAddedMessage>(this,
            (r, message) =>
            {
                if (r.Friends.Any(f => f.User.Id == message.Value.Id))
                    return;

                var friendViewModel = friendUserViewModelFactory.Create(message.Value);
                r.Friends.Add(friendViewModel);
            });

        weakReferenceMessenger.Register<FriendsPanelViewModel, FriendRemovedMessage>(this, (r, message) =>
        {
            if (r.Friends.FirstOrDefault(f => f.User.Id == message.Value.Id) is not { } friend)
                return;

            r.Friends.Remove(friend);
        });
    }
}
