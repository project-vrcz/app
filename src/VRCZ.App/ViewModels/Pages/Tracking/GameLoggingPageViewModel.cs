using System.Collections.ObjectModel;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using VRCZ.Core.GameLogging;
using VRCZ.Core.Models.VRChat.Logging;
using VRCZ.Core.Services.Tracking;

namespace VRCZ.App.ViewModels.Pages.Tracking;

public partial class GameLoggingPageViewModel(VRChatLoggingService gameLoggingService) : PageViewModelBase
{
    [ObservableProperty] public partial ObservableCollection<string> LogPaths { get; private set; } = [];

    [ObservableProperty] public partial ObservableCollection<VRChatLogEntity> LogEntities { get; private set; } = [];

    private readonly CancellationTokenSource _cancellationTokenSource = new();

    private Stream? _logStream;
    private VRChatGameLogReader? _logReader;

    [RelayCommand]
    private void Load()
    {
        LogPaths = new ObservableCollection<string>(gameLoggingService.GetVRChatLogPaths());

        _logStream = File.Open(LogPaths.Last(), FileMode.Open, FileAccess.Read,
            FileShare.ReadWrite | FileShare.Delete);
        _logReader = new VRChatGameLogReader(_logStream);

        _ = Task.Run(async () =>
        {
            while (!_cancellationTokenSource.IsCancellationRequested)
            {
                var logEntity = await _logReader.ReadAsync();
                await Dispatcher.UIThread.InvokeAsync(() => { LogEntities.Add(logEntity); });
            }
        });
    }

    [RelayCommand]
    private void Unload()
    {
        _cancellationTokenSource.Cancel();
        _cancellationTokenSource.Dispose();
    }
}
