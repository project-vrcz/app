using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using VRCZ.App.Services;
using VRCZ.App.ViewModels.Pages.Browser;
using VRCZ.VRChatApi.Generated.Models;

namespace VRCZ.App.ViewModels.Entities;

public partial class AvatarViewModel(Avatar avatar, NavigationService navigationService) : ViewModelBase
{
    [ObservableProperty] private Avatar _avatar = avatar;

    [RelayCommand]
    private void View()
    {
        navigationService.Navigate(new AvatarPageViewModel(this));
    }
}

public class AvatarViewModelFactory(NavigationService navigationService)
{
    public AvatarViewModel Create(Avatar avatar)
    {
        return new AvatarViewModel(avatar, navigationService);
    }
}
