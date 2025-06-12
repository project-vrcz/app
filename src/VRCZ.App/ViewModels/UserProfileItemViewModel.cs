using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using VRCZ.App.Services;
using VRCZ.App.ViewMessages;
using VRCZ.Core.Models;
using VRCZ.Core.Services;
using VRCZ.Core.Services.Profile;
using VRCZ.VRChatApi.Generated.Models;

namespace VRCZ.App.ViewModels;

public partial class UserProfileItemViewModel(
    UserProfile userProfile,
    UserProfileLifetimeService userProfileLifetimeService,
    WeakReferenceMessenger weakReferenceMessenger,
    Func<TwoFactorRequired_requiresTwoFactorAuth[], Task> handleTwoFactor) : ViewModelBase
{
    public UserProfile UserProfile => userProfile;

    public event EventHandler<Exception>? ErrorOccured;

    [RelayCommand]
    private async Task LoadProfile()
    {
        try
        {
            await userProfileLifetimeService.LoadProfileAsync(UserProfile.Id, handleTwoFactor);

            weakReferenceMessenger.Send<ShowMainViewMessage>();
        }
        catch (Exception ex)
        {
            ErrorOccured?.Invoke(this, ex);
        }
    }
}
