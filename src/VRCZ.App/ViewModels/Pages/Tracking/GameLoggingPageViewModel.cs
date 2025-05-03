using System.Collections.ObjectModel;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using VRCZ.Core.Models.VRChat.Logging;
using VRCZ.Core.Services.Tracking;

namespace VRCZ.App.ViewModels.Pages.Tracking;

public partial class GameLoggingPageViewModel(VRChatLoggingService gameLoggingService) : PageViewModelBase
{
    [ObservableProperty] public partial ObservableCollection<string> LogPaths { get; private set; } = [];

    [ObservableProperty] public partial ObservableCollection<VRChatLogEntity> LogEntities { get; private set; } = [];

    [RelayCommand]
    private void Load()
    {
        LogPaths = new ObservableCollection<string>(gameLoggingService.GetVRChatLogPaths());

        _ = gameLoggingService.ParseLogLoop(LogPaths.Last(),
            async logEntity => { await Dispatcher.UIThread.InvokeAsync(() => { LogEntities.Add(logEntity); }); },
            CancellationToken.None);
    }
}
