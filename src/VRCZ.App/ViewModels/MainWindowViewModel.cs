using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using VRCZ.App.ViewMessages;
using VRCZ.App.ViewModels.Views;
using VRCZ.App.ViewModels.Views.MainView;
using VRCZ.Core.Services;
using VRCZ.Core.Services.Profile;

namespace VRCZ.App.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    public partial ViewModelBase CurrentView { get; private set; }

    public MainWindowViewModel(CurrentUserProfileService currentUserProfileService, IServiceProvider serviceProvider)
    {
        CurrentView = currentUserProfileService.IsProfileLoaded
            ? serviceProvider.GetRequiredService<MainViewModel>()
            : serviceProvider.GetRequiredService<ProfileSelectionViewModel>();

        currentUserProfileService.ProfileLoaded += (_, _) =>
        {
            CurrentView = serviceProvider.GetRequiredService<MainViewModel>();
        };

        currentUserProfileService.ProfileUnloading += (_, _) =>
        {
            CurrentView = serviceProvider.GetRequiredService<ProfileSelectionViewModel>();
        };
    }
}
