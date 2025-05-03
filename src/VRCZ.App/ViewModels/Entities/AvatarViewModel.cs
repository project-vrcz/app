using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using VRCZ.VRChatApi.Generated.Models;
using Ursa.Controls;
using VRCZ.App.Dialogs.LocalFavorites;
using VRCZ.App.Services;
using VRCZ.App.ViewModels.Dialogs.LocalFavorites;
using VRCZ.App.ViewModels.Pages.Browser;
using Avatar = VRCZ.VRChatApi.Generated.Models.Avatar;

namespace VRCZ.App.ViewModels.Entities;

public partial class AvatarViewModel(
    Avatar avatar,
    NavigationService navigationService,
    AddAvatarToFavoritesDialogViewModelFactory addToFavoritesFactory) : ViewModelBase
{
    [ObservableProperty]
    public partial Avatar Avatar { get; private set; } = avatar;

    [RelayCommand]
    private void View()
    {
        navigationService.Navigate(new AvatarPageViewModel(this));
    }

    [RelayCommand]
    private void OpenAddFavoritesDialog()
    {
        var viewModel = addToFavoritesFactory.Create(Avatar);

        OverlayDialog.ShowModal<AddAvatarToFavoritesDialog, AddAvatarToFavoritesDialogViewModel>(viewModel,
            options: new OverlayDialogOptions
            {
                Title = "Add To Favorites",
                CanDragMove = false,
                Buttons = DialogButton.None,
                IsCloseButtonVisible = true
            });
    }
}

public class AvatarViewModelFactory(
    NavigationService navigationService,
    AddAvatarToFavoritesDialogViewModelFactory addToFavoritesFactory)
{
    public AvatarViewModel Create(Avatar avatar)
    {
        return new AvatarViewModel(avatar, navigationService, addToFavoritesFactory);
    }
}
