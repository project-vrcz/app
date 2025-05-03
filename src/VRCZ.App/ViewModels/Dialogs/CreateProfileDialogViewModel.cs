using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Ursa.Controls;
using VRCZ.App.ViewModels.Views.Dialogs;
using VRCZ.App.Views.Dialogs;
using VRCZ.App.Views.Dialogs.Profile;
using VRCZ.Core.Models;
using VRCZ.Core.Services;
using VRCZ.VRChatApi.Generated;
using VRCZ.VRChatApi.Generated.Models;

namespace VRCZ.App.ViewModels.Dialogs;

public partial class CreateProfileDialogViewModel : ViewModelBase
{
    [ObservableProperty] public partial UserControl CurrentView { get; private set; }

    [ObservableProperty] public partial string Username { get; set; } = "";

    [ObservableProperty] public partial string Password { get; set; } = "";

    [ObservableProperty]
    public partial TwoFactorRequired_requiresTwoFactorAuth[] Available2FAMethods { get; private set; } = [];

    private readonly VRChatAuthService _vrchatAuthService;
    private readonly VRChatApiClient _vrchatApiClient;
    private readonly ManagedUserProfileService _managedUserProfileService;

    public CreateProfileDialogViewModel(VRChatAuthService vrchatAuthService, VRChatApiClient vrchatApiClient,
        ManagedUserProfileService managedUserProfileService)
    {
        _vrchatAuthService = vrchatAuthService;
        _vrchatApiClient = vrchatApiClient;
        _managedUserProfileService = managedUserProfileService;

        CurrentView = new ProfileDialogLoginView
        {
            DataContext = this
        };
    }

    [RelayCommand]
    private async Task Login()
    {
        try
        {
            var loginResult = await _vrchatAuthService.LoginAsync(Username, Password);

            Available2FAMethods = loginResult.Available2FAMethods ?? [];
            if (loginResult.ResultType == LoginResultType.TwoFactorRequired)
            {
                ToOtpView();
            }
        }
        catch
        {
            throw;
        }
    }

    [RelayCommand]
    private void ToLoginView()
    {
        CurrentView = new ProfileDialogLoginView
        {
            DataContext = this
        };
    }

    [RelayCommand]
    private void ToOtpView()
    {
        CurrentView = new DialogOtpView
        {
            DataContext = new DialogOtpViewModel(VerifyTotp)
        };
    }

    private async Task VerifyTotp(string code, TwoFactorRequired_requiresTwoFactorAuth method)
    {
        await _vrchatAuthService.VerifyTwoFactorAsync(code, method);

        var user = await _vrchatApiClient.Auth.User.GetAsUserGetResponseAsync();

        await MessageBox.ShowAsync($"[{user?.CurrentUser?.Id}] {user?.CurrentUser?.DisplayName}", "Login Success");

        await _vrchatAuthService.CreateProfileForCurrentAccountAsync(Password);
        // await _managedUserProfileService.LoadProfileAsync();
    }
}
