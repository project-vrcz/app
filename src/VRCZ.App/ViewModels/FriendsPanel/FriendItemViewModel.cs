using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using VRCZ.App.Services;
using VRCZ.App.ViewMessages.TrackedEntities;
using VRCZ.Core.Models;
using VRCZ.Core.Services;
using VRCZ.VRChatApi.Generated.Models;

namespace VRCZ.App.ViewModels.FriendsPanel;

public partial class FriendItemViewModel : ViewModelBase
{
    [ObservableProperty] private LimitedUser _user;
    [ObservableProperty] private UserLocation? _location;

    public string? UserAvatarUrl =>
        !string.IsNullOrEmpty(User.UserIcon) ? User.UserIcon :
        !string.IsNullOrEmpty(User.CurrentAvatarThumbnailImageUrl) ? User.CurrentAvatarThumbnailImageUrl :
        null;

    [ObservableProperty] private Instance? _instance;
    [ObservableProperty] private World? _world;

    private readonly VRChatTrackedEntitiesService _trackedEntitiesService;

    public FriendItemViewModel(WeakReferenceMessenger weakReferenceMessenger, LimitedUser limitedUser,
        VRChatTrackedEntitiesService trackedEntitiesService)
    {
        _user = limitedUser;
        _trackedEntitiesService = trackedEntitiesService;

        if (limitedUser.Id is not { } userId)
            throw new ArgumentException("User ID is required", nameof(limitedUser));

        _location = trackedEntitiesService.GetUserLocation(userId) ??
                    (limitedUser.Location is not null ? UserLocation.Parse(limitedUser.Location) : null);

        weakReferenceMessenger.Register<FriendItemViewModel, FriendUpdateEvent>(this, (recipient, message) =>
        {
            if (recipient.User.Id != message.Value.Id)
                return;

            recipient.User = message.Value;
        });

        weakReferenceMessenger.Register<FriendItemViewModel, UserLocationUpdatedMessage>(this, (recipient, message) =>
        {
            if (recipient.User.Id != message.Value.UserId)
                return;

            recipient.Location = message.Value.UserLocation;

            LoadDataCommand.Execute(null);
        });

        PropertyChanged += (_, args) =>
        {
            if (args.PropertyName == nameof(User))
                OnPropertyChanged(nameof(UserAvatarUrl));
        };
    }

    [RelayCommand]
    private async Task LoadData()
    {
        if (Location?.WorldId is null || Location?.InstanceId is null)
        {
            Instance = null;
            World = null;
            return;
        }

        Instance = await _trackedEntitiesService.GetCachedInstanceAsync(Location.WorldId, Location.InstanceId);
        World = Instance.World;
    }
}
