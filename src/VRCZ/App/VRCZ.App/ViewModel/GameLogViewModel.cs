using System.Collections.Specialized;
using System.ComponentModel;
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

    [ObservableProperty] public partial bool ScrollToEnd { get; set; } = true;

    #region Filters

    public AvaloniaList<string> AvailableLogLevels { get; } = [];
    public AvaloniaList<string> SelectedLogLevels { get; } = [];
    [ObservableProperty] public partial string KeywordFilter { get; set; } = string.Empty;

    public AvaloniaList<GameLogEntityWithEvent> FilteredLogEntities { get; } = [];

    #endregion

    public AvaloniaList<GameLogEntityWithEvent> LogEntities { get; } = [];

    #region Selected Log Entity

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

    #endregion

    [RelayCommand]
    private async Task Load()
    {
        await TryCreateLogWatcherAsync();

        PropertyChanged += OnPropertyChanged;
        SelectedLogLevels.CollectionChanged += SelectedLogLevelsOnCollectionChanged;
    }

    [RelayCommand]
    private async Task Unload()
    {
        PropertyChanged -= OnPropertyChanged;
        SelectedLogLevels.CollectionChanged -= SelectedLogLevelsOnCollectionChanged;

        await TryDisposeLogWatcherAsync();
    }

    private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName is nameof(KeywordFilter))
        {
            RecalculateFilteredLogEntities();
        }
    }

    private void SelectedLogLevelsOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        RecalculateFilteredLogEntities();
    }

    private void OnLogCreated(object? sender, GameLogEntityWithEvent e)
    {
        Dispatcher.UIThread.Invoke(() =>
        {
            if (!AvailableLogLevels.Contains(e.LogEntity.LogLevel))
            {
                AvailableLogLevels.Add(e.LogEntity.LogLevel);
            }

            LogEntities.Add(e);
            OnNewLogEntityAdded(e);
        });
    }

    #region Log Watcher Lifetime

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
        AvailableLogLevels.Clear();
        FilteredLogEntities.Clear();

        if (_logWatcher is not null)
        {
            _logWatcher.OnLog -= OnLogCreated;
            await _logWatcher.DisposeAsync();
            _logWatcher = null;
        }
    }

    #endregion

    #region Filter Calculation

    private void RecalculateFilteredLogEntities()
    {
        var filtered = LogEntities.Where(e =>
            (SelectedLogLevels.Count == 0 || SelectedLogLevels.Contains(e.LogEntity.LogLevel)) &&
            (string.IsNullOrEmpty(KeywordFilter) ||
             e.LogEntity.Message.Contains(KeywordFilter, StringComparison.OrdinalIgnoreCase))
        ).ToList();

        FilteredLogEntities.Clear();
        FilteredLogEntities.AddRange(filtered);
    }

    private void OnNewLogEntityAdded(GameLogEntityWithEvent newEntity)
    {
        if ((SelectedLogLevels.Count == 0 || SelectedLogLevels.Contains(newEntity.LogEntity.LogLevel)) &&
            (string.IsNullOrEmpty(KeywordFilter) ||
             newEntity.LogEntity.Message.Contains(KeywordFilter, StringComparison.OrdinalIgnoreCase)))
        {
            FilteredLogEntities.Add(newEntity);
        }
    }

    #endregion
}

public sealed class GameLogViewModelFactory
{
    public GameLogViewModel Create(string pathToLogFile)
    {
        return new GameLogViewModel(pathToLogFile);
    }
}