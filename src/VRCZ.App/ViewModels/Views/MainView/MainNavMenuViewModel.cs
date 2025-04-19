using CommunityToolkit.Mvvm.ComponentModel;
using VRCZ.App.Pages.Favorites;
using VRCZ.App.Services;
using VRCZ.App.ViewModels.Pages;
using VRCZ.App.ViewModels.Pages.Browser;
using VRCZ.App.ViewModels.Pages.Favorites;

namespace VRCZ.App.ViewModels.Views.MainView;

public partial class MainNavMenuViewModel : ViewModelBase
{
    [ObservableProperty] private NavMenuItemViewModel[] _items = [];
    [ObservableProperty] private TopNavMenuItemViewModel[] _topItems = [];

    private readonly NavigationService _navigationService;
    private readonly IServiceProvider _serviceProvider;

    public MainNavMenuViewModel(NavigationService navigationService, IServiceProvider serviceProvider)
    {
        _navigationService = navigationService;
        _serviceProvider = serviceProvider;

        TopItems =
        [
            new TopNavMenuItemViewModel("Home", "SemiIconHome", SetMenuItems, [
                new NavMenuItemViewModel("Dashboard", "SemiIconCommand", Navigate, typeof(HomeViewModel),
                    isDefault: true),
                new NavMenuItemViewModel("Tracking", isSeparator: true),
                new NavMenuItemViewModel("Tracker", "SemiIconMapPin", Navigate, typeof(HomeViewModel)),
                new NavMenuItemViewModel("Gameplay History", "SemiIconAlignLeft", Navigate, typeof(HomeViewModel)),
                new NavMenuItemViewModel("My", isSeparator: true),
                new NavMenuItemViewModel("Avatars", "SemiIconUserCircle", Navigate, typeof(MyAvatarPageViewModel)),
                new NavMenuItemViewModel("Worlds", "SemiIconGlobe", Navigate, typeof(HomeViewModel)),
                new NavMenuItemViewModel("Friends", "SemiIconUserGroup", Navigate, typeof(HomeViewModel)),
                new NavMenuItemViewModel("Groups", "SemiIconApartment", Navigate, typeof(HomeViewModel))
            ], true),
            new TopNavMenuItemViewModel("Browser", "SemiIconHelm", SetMenuItems, [
                new NavMenuItemViewModel("Worlds", "SemiIconGlobe", Navigate, typeof(HomeViewModel), isDefault: true),
                new NavMenuItemViewModel("Avatars", "SemiIconUserCircle", Navigate, typeof(AvatarBrowserIndexPageViewModel)),
                new NavMenuItemViewModel("Groups", "SemiIconApartment", Navigate, typeof(HomeViewModel)),
            ]),
            new TopNavMenuItemViewModel("Tools", "SemiIconWrench", SetMenuItems, [])
        ];
    }

    public void Init()
    {
        SetMenuItems(TopItems[0].Items);
    }

    private void SetMenuItems(NavMenuItemViewModel[] navMenuItemViewModels)
    {
        Items = navMenuItemViewModels;
        Enumerable.FirstOrDefault<NavMenuItemViewModel>(Items, item => item.IsDefault)?.Navigate();
    }

    private void Navigate(Type viewModelType)
    {
        if (_serviceProvider.GetService(viewModelType) is not PageViewModelBase viewModel)
            return;

        _navigationService.Navigate(viewModel);
    }
}
