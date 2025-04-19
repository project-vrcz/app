using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using VRCZ.App.ViewModels.Entities;
using VRCZ.VRChatApi.Generated;

namespace VRCZ.App.ViewModels.Pages.Browser;

public partial class AvatarBrowserIndexPageViewModel(
    VRChatApiClient apiClient,
    AvatarViewModelFactory avatarViewModelFactory) : PageViewModelBase
{
    [ObservableProperty] private ObservableCollection<AvatarViewModel> _avatars = [];

    [RelayCommand]
    private async Task LoadAvatarsAsync()
    {
        var avatars = await apiClient.Avatars.GetAsync(config => { config.QueryParameters.Featured = true; });

        if (avatars is null)
            return;

        Avatars = new ObservableCollection<AvatarViewModel>(avatars.Select(avatarViewModelFactory.Create));
    }
}
