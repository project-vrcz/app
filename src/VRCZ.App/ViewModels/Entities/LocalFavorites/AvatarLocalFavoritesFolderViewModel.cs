using CommunityToolkit.Mvvm.Input;
using VRCZ.App.Services;
using VRCZ.App.ViewModels.Pages.My.LocalFavorites;
using VRCZ.Core.Models.Entities.LocalFavorites;

namespace VRCZ.App.ViewModels.Entities.LocalFavorites;

public partial class AvatarLocalFavoritesFolderViewModel(
    AvatarFavoritesFolder folder,
    NavigationService navigationService,
    AvatarLocalFavoritesFolderPageViewModelFactory pageViewModelFactory)
    : ViewModelBase
{
    public AvatarFavoritesFolder Folder => folder;

    [RelayCommand]
    private void View()
    {
        var viewModel = pageViewModelFactory.Create(folder.Id);
        navigationService.Navigate(viewModel);
    }
}

public class AvatarFavoritesFolderViewModelFactory(
    NavigationService navigationService,
    AvatarLocalFavoritesFolderPageViewModelFactory pageViewModelFactory)
{
    public AvatarLocalFavoritesFolderViewModel Create(AvatarFavoritesFolder folder)
    {
        return new AvatarLocalFavoritesFolderViewModel(folder, navigationService, pageViewModelFactory);
    }
}
