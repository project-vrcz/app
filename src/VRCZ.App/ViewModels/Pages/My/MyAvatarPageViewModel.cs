using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Ursa.Controls;
using VRCZ.App.Dialogs.LocalFavorites;
using VRCZ.App.ViewModels.Dialogs.LocalFavorites;
using VRCZ.Core.Models.Entities.LocalFavorites;
using VRCZ.Core.Services.LocalFavorites;

namespace VRCZ.App.ViewModels.Pages.Favorites;

public partial class MyAvatarPageViewModel(AvatarLocalFavoritesService localFavoritesService) : PageViewModelBase
{
    [ObservableProperty] private ObservableCollection<AvatarFavoritesFolder> _localAvatarFavoritesFolders = [];

    [RelayCommand]
    private async Task LoadAvatarFavoritesAsync()
    {
        var folders = await localFavoritesService.GetAvatarFavoritesFoldersAsync();
        LocalAvatarFavoritesFolders = new ObservableCollection<AvatarFavoritesFolder>(folders);
    }

    [RelayCommand]
    private void OpenCreateLocalDialog()
    {
        OverlayDialog.ShowModal<CreateAvatarLocalFavoritesFolderDialog, CreateAvatarLocalFavoritesFolderDialogViewModel>(
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
