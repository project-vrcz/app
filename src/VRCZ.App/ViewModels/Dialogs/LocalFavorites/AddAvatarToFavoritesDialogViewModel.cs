using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using VRCZ.Core.Mappers;
using VRCZ.Core.Models.Entities.LocalFavorites;
using VRCZ.Core.Services.LocalFavorites;
using VRCZ.VRChatApi.Generated.Models;

namespace VRCZ.App.ViewModels.Dialogs.LocalFavorites;

public partial class AddAvatarToFavoritesDialogViewModel(Avatar avatar, AvatarLocalFavoritesService avatarLocalFavoritesService)
    : ViewModelBase
{
    [ObservableProperty]
    public partial ObservableCollection<AvatarFavoritesFolder> LocalFolders { get; private set; } = [];

    [RelayCommand]
    private async Task Load()
    {
        var folders = await avatarLocalFavoritesService.GetAvatarFavoritesFoldersAsync();

        LocalFolders = new ObservableCollection<AvatarFavoritesFolder>(folders);
    }

    [RelayCommand]
    private async Task AddToFavorites(AvatarFavoritesFolder folder)
    {
        var avatarEntity = VRChatDatabaseEntitiesMapper.MapToAvatarEntity(avatar);
        await avatarLocalFavoritesService.AddAvatarToFavoritesAsync(avatarEntity, folder.Id);
    }
}

public class AddAvatarToFavoritesDialogViewModelFactory(AvatarLocalFavoritesService avatarLocalFavoritesService)
{
    public AddAvatarToFavoritesDialogViewModel Create(Avatar avatar)
    {
        return new AddAvatarToFavoritesDialogViewModel(avatar, avatarLocalFavoritesService);
    }
}
