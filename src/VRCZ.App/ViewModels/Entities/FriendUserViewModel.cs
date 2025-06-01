using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using VRCZ.App.ViewMessages.TrackedEntities;
using VRCZ.Core.Models;
using VRCZ.Core.Services.Tracking;
using VRCZ.VRChatApi.Generated.Models;

namespace VRCZ.App.ViewModels.Entities;

public partial class FriendUserViewModel : LimitedUserViewModel
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(LocationWorld))]
    [NotifyPropertyChangedFor(nameof(LocationInstance))]
    public partial UserLocation? Location { get; private set; }

    [ObservableProperty] public partial World? LocationWorld { get; private set; }

    [ObservableProperty] public partial Instance? LocationInstance { get; private set; }

    private readonly VRChatTrackedEntitiesService _trackedEntitiesService;

    public FriendUserViewModel(
        LimitedUser user,
        WeakReferenceMessenger weakReferenceMessenger,
        VRChatTrackedEntitiesService trackedEntitiesService
    ) :
        base(user, weakReferenceMessenger)
    {
        ArgumentNullException.ThrowIfNull(user.Id);

        _trackedEntitiesService = trackedEntitiesService;

        Location = _trackedEntitiesService.GetUserLocation(user.Id);

        weakReferenceMessenger.Register<FriendUserViewModel, UserLocationUpdatedMessage>(this, (recipient, message) =>
        {
            if (recipient.User.Id != message.Value.UserId)
                return;

            recipient.Location = message.Value.UserLocation;

            LoadDataCommand.Execute(null);
        });
    }

    [RelayCommand]
    private async Task LoadData()
    {
        if (Location?.WorldId is null || Location?.InstanceId is null)
        {
            LocationInstance = null;
            LocationWorld = null;
            return;
        }

        LocationInstance = await _trackedEntitiesService.GetCachedInstanceAsync(Location.WorldId, Location.InstanceId);
        LocationWorld = LocationInstance.World;
    }
}

public class FriendUserViewModelFactory(
    WeakReferenceMessenger weakReferenceMessenger,
    VRChatTrackedEntitiesService trackedEntitiesService)
{
    public FriendUserViewModel Create(LimitedUser user)
    {
        return new FriendUserViewModel(user, weakReferenceMessenger, trackedEntitiesService);
    }
}
