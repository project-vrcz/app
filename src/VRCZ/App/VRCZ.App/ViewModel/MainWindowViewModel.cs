using Avalonia.Collections;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using VRCZ.Core.GameLogging.Services;
using VRCZ.Core.Shared;

namespace VRCZ.App.ViewModel;

public partial class MainWindowViewModel(
    GameLogFileSystemWatcherService logFileSystemWatcherService,
    GameLogViewModelFactory gameLogViewModelFactory
) : ObservableObject
{
    public AvaloniaList<LogFileInfo> LogFilePaths { get; } = [];

    public LogFileInfo? SelectedLogFile
    {
        get;
        set
        {
            if (!SetProperty(ref field, value))
                return;

            if (value is null)
            {
                SelectedGameLogView = null;
                return;
            }

            SelectedGameLogView = gameLogViewModelFactory.Create(value.FullPath);
        }
    }

    [ObservableProperty] public partial GameLogViewModel? SelectedGameLogView { get; set; }

    [RelayCommand]
    private async Task Load()
    {
        LogFilePaths.Clear();
        LogFilePaths.AddRange(VRChatStorageUtils.GetVRChatLogPaths()
            .Select(path => new LogFileInfo(path, Path.GetFileName(path))));

        logFileSystemWatcherService.LogFileCreated += OnLogFileCreated;
        logFileSystemWatcherService.LogFileDeleted += OnLogFileDeleted;
    }

    [RelayCommand]
    private async Task Unload()
    {
        logFileSystemWatcherService.LogFileCreated -= OnLogFileCreated;
        logFileSystemWatcherService.LogFileDeleted -= OnLogFileDeleted;
    }

    #region Event Handler

    private void OnLogFileDeleted(object? sender, string e)
    {
        Dispatcher.UIThread.Invoke(() => LogFilePaths.RemoveAll(LogFilePaths.Where(x => x.FullPath == e)));
    }

    private void OnLogFileCreated(object? sender, string e)
    {
        Dispatcher.UIThread.Invoke(() =>
        {
            if (LogFilePaths.Any(x => x.FullPath == e))
                return;

            LogFilePaths.Add(new LogFileInfo(e, Path.GetFileName(e)));
        });
    }

    #endregion
}

public record LogFileInfo(string FullPath, string FileName);