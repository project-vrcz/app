using System.Collections.ObjectModel;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using VRCZ.Core.Models.VRChat.Logging;
using VRCZ.Core.Services.Tracking;

namespace VRCZ.App.ViewModels.Pages.Tracking;

public partial class GameLoggingPageViewModel(VRChatLoggingService gameLoggingService) : PageViewModelBase
{
    [ObservableProperty] private ObservableCollection<string> _logPaths = [];

    [ObservableProperty] private ObservableCollection<VRChatLogEntity> _logEntities = [];

    [RelayCommand]
    private void Load()
    {
        LogPaths = new ObservableCollection<string>(gameLoggingService.GetVRChatLogPaths());

        _ = gameLoggingService.ParseLogLoop(LogPaths.Last(), async logEntity =>
            {
                await Dispatcher.UIThread.InvokeAsync(() =>
                {
                    LogEntities.Add(logEntity);
                });
            },
            CancellationToken.None);
    }
}
