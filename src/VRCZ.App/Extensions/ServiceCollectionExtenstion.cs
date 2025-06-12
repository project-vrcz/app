using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using VRCZ.App.Services;
using VRCZ.App.ViewModels;
using VRCZ.App.ViewModels.Dialogs;
using VRCZ.App.ViewModels.Dialogs.LocalFavorites;
using VRCZ.App.ViewModels.Entities;
using VRCZ.App.ViewModels.Entities.LocalFavorites;
using VRCZ.App.ViewModels.FriendsPanel;
using VRCZ.App.ViewModels.Pages;
using VRCZ.App.ViewModels.Pages.Browser;
using VRCZ.App.ViewModels.Pages.My;
using VRCZ.App.ViewModels.Pages.My.LocalFavorites;
using VRCZ.App.ViewModels.Pages.Tracking;
using VRCZ.App.ViewModels.Views;
using VRCZ.App.ViewModels.Views.MainView;

namespace VRCZ.App.Extenstions;

public static class ServiceCollectionExtenstion
{
    public static IServiceCollection AddVRCZApp(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<AppWebImageLoader>();
        serviceCollection.AddSingleton<NavigationService>();
        serviceCollection.AddHostedService<TrackedEntitiesMessengerService>();
        serviceCollection.AddSingleton<WeakReferenceMessenger>();

        serviceCollection.AddSingleton<MainWindowViewModel>();

        #region Controls

        serviceCollection.AddSingleton<TrayMenuViewModel>();
        serviceCollection.AddTransient<FriendsPanelViewModel>();
        serviceCollection.AddTransient<MainNavMenuViewModel>();

        #endregion

        #region MainWindow Views

        serviceCollection.AddTransient<ProfileSelectionViewModel>();
        serviceCollection.AddTransient<MainViewModel>();

        #endregion

        #region Entities

        serviceCollection.AddTransient<AvatarViewModelFactory>();
        serviceCollection.AddTransient<LimitedUserViewModelFactory>();
        serviceCollection.AddTransient<FriendUserViewModelFactory>();

        serviceCollection.AddTransient<AvatarFavoritesFolderViewModelFactory>();

        #endregion

        #region Dialogs

        serviceCollection.AddTransient<CreateProfileDialogViewModel>();
        serviceCollection.AddTransient<AddAvatarToFavoritesDialogViewModelFactory>();

        #endregion

        #region Pages

        // Home
        serviceCollection.AddTransient<HomePageViewModel>();

        // Home -> Tracking
        serviceCollection.AddTransient<GameLoggingPageViewModel>();

        // Home -> My
        serviceCollection.AddTransient<MyAvatarPageViewModel>();
        serviceCollection.AddTransient<AvatarLocalFavoritesFolderPageViewModelFactory>();

        // Browser
        serviceCollection.AddTransient<AvatarBrowserIndexPageViewModel>();

        #endregion

        return serviceCollection;
    }
}
