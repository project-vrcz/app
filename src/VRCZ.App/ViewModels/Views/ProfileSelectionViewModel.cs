using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Ursa.Controls;
using VRCZ.App.Dialogs;
using VRCZ.App.Services;
using VRCZ.App.ViewModels.Dialogs;
using VRCZ.App.ViewModels.Views.Dialogs;
using VRCZ.App.Views.Dialogs;
using VRCZ.Core.Services;
using VRCZ.VRChatApi.Generated.Models;

namespace VRCZ.App.ViewModels.Views;

public partial class ProfileSelectionViewModel(
    UserProfileService userProfileService,
    ManagedUserProfileService managedUserProfileService,
    VRChatAuthService vrchatAuthService,
    WeakReferenceMessenger weakReferenceMessenger,
    IServiceProvider serviceProvider) : ViewModelBase
{
    [ObservableProperty] private UserProfileItemViewModel[] _profiles = [];

    [ObservableProperty] private string? _errorMessage;

    [RelayCommand]
    private async Task LoadProfiles()
    {
        var profiles = await userProfileService.GetProfilesAsync();

        Profiles = profiles
            .Select(profile =>
                new UserProfileItemViewModel(profile, managedUserProfileService,
                    weakReferenceMessenger, HandleTwoFactor))
            .ToArray();

        foreach (var userProfileItemViewModel in Profiles)
        {
            userProfileItemViewModel.ErrorOccured += (_, exception) =>
            {
                if (exception is Error { ErrorProp: not null } apiError)
                {
                    ErrorMessage = apiError.ErrorProp.Message;
                    return;
                }

                ErrorMessage = exception.ToString();
            };
        }
    }

    private async Task HandleTwoFactor(TwoFactorRequired_requiresTwoFactorAuth[] availableTwoFactor)
    {
        await OverlayDialog.ShowModal(new DialogOtpView(), new DialogOtpViewModel(async (code, method) =>
        {
            var result = await vrchatAuthService.VerifyTwoFactorAsync(code, method);

            if (!result)
            {
                throw new InvalidOperationException("Invalid OTP/TOTP code");
            }
        }), options: new OverlayDialogOptions
        {
            Buttons = DialogButton.None,
            CanDragMove = false,
            CanResize = false,
            Title = "Two-factor Authentication"
        });
    }

    [RelayCommand]
    private async Task CreateProfile()
    {
        var createProfileDialogViewModel = serviceProvider.GetRequiredService<CreateProfileDialogViewModel>();

        await OverlayDialog.ShowModal<CreateProfileDialog, CreateProfileDialogViewModel>(createProfileDialogViewModel,
            options: new OverlayDialogOptions
            {
                Buttons = DialogButton.None,
                CanDragMove = false,
                CanResize = false,
                Title = "Login your VRChat account"
            });
    }
}
