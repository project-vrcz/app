﻿using CommunityToolkit.Mvvm.Messaging;
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

        serviceCollection.AddSingleton<TrayMenuViewModel>();
        serviceCollection.AddTransient<MainViewModel>();
        serviceCollection.AddTransient<ProfileSelectionViewModel>();
        serviceCollection.AddTransient<CreateProfileDialogViewModel>();
        serviceCollection.AddTransient<FriendsPanelViewModel>();
        serviceCollection.AddSingleton<MainWindowViewModel>();

        serviceCollection.AddTransient<MainNavMenuViewModel>();
        serviceCollection.AddTransient<HomeViewModel>();

        serviceCollection.AddTransient<MyAvatarPageViewModel>();
        serviceCollection.AddTransient<AvatarBrowserIndexPageViewModel>();

        serviceCollection.AddTransient<GameLoggingPageViewModel>();

        serviceCollection.AddTransient<AvatarLocalFavoritesFolderPageViewModelFactory>();

        serviceCollection.AddTransient<AvatarViewModelFactory>();
        serviceCollection.AddTransient<AvatarFavoritesFolderViewModelFactory>();

        serviceCollection.AddTransient<AddAvatarToFavoritesDialogViewModelFactory>();

        serviceCollection.AddSingleton<WeakReferenceMessenger>();

        serviceCollection.AddHostedService<TrackedEntitiesMessengerService>();

        return serviceCollection;
    }
}
