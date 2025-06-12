using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using VRCZ.App.Services;
using VRCZ.App.ViewMessages.TrackedEntities;
using VRCZ.App.ViewModels.FriendsPanel;
using VRCZ.App.ViewModels.Pages;
using VRCZ.Core.Services.Tracking;
using VRCZ.VRChatApi.Generated.Models;

namespace VRCZ.App.ViewModels.Views.MainView;

public partial class MainViewModel : ViewModelBase, INavigationHost
{
    [ObservableProperty] private PageViewModelBase? _currentPage;

    [ObservableProperty] private CurrentUser? _loggedInUser;

    public MainNavMenuViewModel NavMenu { get; }
    public FriendsPanelViewModel Friends { get; }

    private readonly NavigationService _navigationService;
    private readonly VRChatTrackedEntitiesService _trackedEntitiesService;
    private readonly WeakReferenceMessenger _weakReferenceMessenger;

    public MainViewModel(NavigationService navigationService,
        MainNavMenuViewModel mainNavMenuViewModel, VRChatTrackedEntitiesService trackedEntitiesService,
        FriendsPanelViewModel friends, WeakReferenceMessenger weakReferenceMessenger)
    {
        _navigationService = navigationService;
        NavMenu = mainNavMenuViewModel;
        _trackedEntitiesService = trackedEntitiesService;
        _weakReferenceMessenger = weakReferenceMessenger;
        Friends = friends;

        _navigationService.Register(this);

        NavMenu.Init();

        LoggedInUser = _trackedEntitiesService.GetLoggedInUser();

        _weakReferenceMessenger.Register<MainViewModel, LoggedInUserUpdatedMessage>(this,
            (recipient, message) => { recipient.LoggedInUser = message.Value; });
    }

    public void Navigate(PageViewModelBase pageViewModel)
    {
        CurrentPage = pageViewModel;
    }

    [RelayCommand]
    private void GoHome()
    {
        _navigationService.Navigate(new HomePageViewModel());
    }
}
