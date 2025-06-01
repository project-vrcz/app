using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using VRCZ.App.ViewMessages.TrackedEntities;
using VRCZ.Core.Services.Tracking;
using VRCZ.VRChatApi.Generated.Models;

namespace VRCZ.App.ViewModels.Entities;

public partial class LimitedUserViewModel : ViewModelBase
{
    public LimitedUserViewModel(LimitedUser user,
        WeakReferenceMessenger weakReferenceMessenger)
    {
        User = user;

        weakReferenceMessenger.Register<LimitedUserViewModel, FriendUpdateEvent>(this, (recipient, message) =>
        {
            if (recipient.User.Id != message.Value.Id)
                return;

            recipient.User = message.Value;
        });
    }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(UserIconUrl))]
    public partial LimitedUser User { get; private set; }

    public string? UserIconUrl =>
        !string.IsNullOrEmpty(User.UserIcon) ? User.UserIcon :
        !string.IsNullOrEmpty(User.CurrentAvatarThumbnailImageUrl) ? User.CurrentAvatarThumbnailImageUrl :
        null;
}

public class LimitedUserViewModelFactory(
    WeakReferenceMessenger weakReferenceMessenger)
{
    public LimitedUserViewModel Create(LimitedUser user)
    {
        return new LimitedUserViewModel(user, weakReferenceMessenger);
    }
}
