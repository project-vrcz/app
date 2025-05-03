using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Ursa.Controls;
using VRCZ.App.Dialogs.LocalFavorites;
using VRCZ.App.ViewModels.Dialogs.LocalFavorites;
using VRCZ.App.ViewModels.Entities.LocalFavorites;
using VRCZ.Core.Services.LocalFavorites;

namespace VRCZ.App.ViewModels.Pages.My;

public partial class MyAvatarPageViewModel(
    AvatarLocalFavoritesService localFavoritesService,
    AvatarFavoritesFolderViewModelFactory viewModelFactory) : PageViewModelBase
{
    [ObservableProperty]
    public partial ObservableCollection<AvatarLocalFavoritesFolderViewModel> LocalAvatarFavoritesFolders
    {
        get;
        private set;
    } = [];

    [RelayCommand]
    private async Task LoadAvatarFavoritesAsync()
    {
        var folders = await localFavoritesService.GetAvatarFavoritesFoldersAsync();
        LocalAvatarFavoritesFolders =
            new ObservableCollection<AvatarLocalFavoritesFolderViewModel>(folders.Select(viewModelFactory.Create));
    }

    [RelayCommand]
    private void OpenCreateLocalDialog()
    {
        OverlayDialog
            .ShowModal<CreateAvatarLocalFavoritesFolderDialog, CreateAvatarLocalFavoritesFolderDialogViewModel>(
                new CreateAvatarLocalFavoritesFolderDialogViewModel(localFavoritesService),
                options: new OverlayDialogOptions
                {
                    Title = "Create Local Avatar Favorites Folder",
                    CanDragMove = false,
                    CanResize = false,
                    IsCloseButtonVisible = true,
                    Buttons = DialogButton.None
                }
            );
    }
}
