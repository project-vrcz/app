using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using VRCZ.App.ViewModels.Entities;
using VRCZ.Core.Mappers;
using VRCZ.Core.Models.Entities.LocalFavorites;
using VRCZ.Core.Services.LocalFavorites;

namespace VRCZ.App.ViewModels.Pages.My.LocalFavorites;

public partial class AvatarLocalFavoritesFolderPageViewModel(
    Guid folderId,
    AvatarLocalFavoritesService avatarLocalFavoritesService,
    AvatarViewModelFactory avatarViewModelFactory) : PageViewModelBase
{
    [ObservableProperty] private AvatarFavoritesFolder? _folder;

    [ObservableProperty] private ObservableCollection<AvatarViewModel> _avatars = [];

    [RelayCommand]
    private async Task Load()
    {
        var folder = await avatarLocalFavoritesService.GetAvatarFavoritesFolderAsync(folderId);
        Folder = folder;

        if (folder is null)
        {
            Avatars = [];
            return;
        }

        Avatars = new ObservableCollection<AvatarViewModel>(folder.Avatars.Select(avatar =>
        {
            var avatarModel = VRChatDatabaseEntitiesMapper.MapToAvatar(avatar);
            return avatarViewModelFactory.Create(avatarModel);
        }));
    }
}

public class AvatarLocalFavoritesFolderPageViewModelFactory(
    AvatarLocalFavoritesService avatarLocalFavoritesService,
    AvatarViewModelFactory avatarViewModelFactory)
{
    public AvatarLocalFavoritesFolderPageViewModel Create(Guid folderId)
    {
        return new AvatarLocalFavoritesFolderPageViewModel(folderId, avatarLocalFavoritesService, avatarViewModelFactory);
    }
}
