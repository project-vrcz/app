using Avalonia.Collections;
using Avalonia.Threading;
using AvaloniaEdit.Document;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using VRCZ.Core.GameLogging;
using VRCZ.Core.GameLogging.Models;
using VRCZ.Core.GameLogging.Services;
using VRCZ.Core.Shared;

namespace VRCZ.App.ViewModel;

public partial class MainWindowViewModel(GameLogFileSystemWatcherService logFileSystemWatcherService) : ObservableObject
{
    public AvaloniaList<LogFileInfo> LogFilePaths { get; } = [];
    public AvaloniaList<GameLogEntityWithEvent> LogEntities { get; } = [];

    [NotifyPropertyChangedFor(nameof(SelectedLogEntityDocument))]
    [ObservableProperty]
    public partial GameLogEntityWithEvent? SelectedLogEntity { get; set; }

    public TextDocument SelectedLogEntityDocument
    {
        get
        {
            if (SelectedLogEntity is null)
                return new TextDocument("");

            var message = SelectedLogEntity.LogEntity.Message;
            // trim lines with super long text

            var lines = message.Split('\n');
            if (!lines.Any(x => x.Length > 10000))
                return new TextDocument(SelectedLogEntity.LogEntity.Message);

            var trimmedLines = lines.Select(x =>
                x.Length > 10000 ? x.Substring(0, 10000) + "... (line trimmed due to line too long)" : x);
            var trimmedMessage = string.Join('\n', trimmedLines);
            return new TextDocument(trimmedMessage);
        }
    }

    public LogFileInfo? SelectedLogFile
    {
        get;
        set
        {
            if (!SetProperty(ref field, value))
                return;

            _ = TryCreateLogWatcherAsync();
        }
    }

    private GameLogWatcher? _logWatcher;

    [RelayCommand]
    private async Task Load()
    {
        LogFilePaths.Clear();
        LogFilePaths.AddRange(VRChatStorageUtils.GetVRChatLogPaths()
            .Select(path => new LogFileInfo(path, Path.GetFileName(path))));

        logFileSystemWatcherService.LogFileCreated += OnLogFileCreated;
        logFileSystemWatcherService.LogFileDeleted += OnLogFileDeleted;

        await TryCreateLogWatcherAsync();
    }

    [RelayCommand]
    private async Task Unload()
    {
        logFileSystemWatcherService.LogFileCreated -= OnLogFileCreated;
        logFileSystemWatcherService.LogFileDeleted -= OnLogFileDeleted;

        await TryDisposeLogWatcherAsync();
    }

    private void OnLogCreated(object? sender, GameLogEntityWithEvent e)
    {
        Dispatcher.UIThread.Invoke(() => LogEntities.Add(e));
    }

    #region LogWatcher Lifetime

    private async Task TryCreateLogWatcherAsync()
    {
        await TryDisposeLogWatcherAsync();

        if (SelectedLogFile is null)
            return;

        if (!File.Exists(SelectedLogFile.FullPath))
            return;

        _logWatcher = new GameLogWatcher(SelectedLogFile.FullPath);
        _logWatcher.OnLog += OnLogCreated;

        _logWatcher.Start();
    }

    private async Task TryDisposeLogWatcherAsync()
    {
        LogEntities.Clear();

        if (_logWatcher is not null)
        {
            _logWatcher.OnLog -= OnLogCreated;
            await _logWatcher.DisposeAsync();
            _logWatcher = null;
        }
    }

    #endregion

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