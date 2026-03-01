using Avalonia.Collections;
using Avalonia.Threading;
using AvaloniaEdit.Document;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using VRCZ.Core.GameLogging;
using VRCZ.Core.GameLogging.Models;

namespace VRCZ.App.ViewModel;

public sealed partial class GameLogViewModel(string pathToLogFile) : ObservableObject, IViewModel
{
    private GameLogWatcher? _logWatcher;

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

    [RelayCommand]
    private async Task Load()
    {
        await TryCreateLogWatcherAsync();
    }

    [RelayCommand]
    private async Task Unload()
    {
        await TryDisposeLogWatcherAsync();
    }

    private void OnLogCreated(object? sender, GameLogEntityWithEvent e)
    {
        Dispatcher.UIThread.Invoke(() => LogEntities.Add(e));
    }

    private async Task TryCreateLogWatcherAsync()
    {
        await TryDisposeLogWatcherAsync();
        if (!File.Exists(pathToLogFile))
            return;

        _logWatcher = new GameLogWatcher(pathToLogFile);
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
}

public sealed class GameLogViewModelFactory
{
    public GameLogViewModel Create(string pathToLogFile)
    {
        return new GameLogViewModel(pathToLogFile);
    }
}