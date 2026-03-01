using VRCZ.Core.GameLogging.LogEvent;
using VRCZ.Core.GameLogging.Models;

namespace VRCZ.Core.GameLogging;

public sealed class GameLogWatcher(string pathToLogFile) : IDisposable, IAsyncDisposable
{
    public event EventHandler<GameLogEntityWithEvent>? OnLog;
    public event EventHandler<Exception>? OnException;

    public bool IsRunning { get; private set; }

    private bool _isDisposed;
    private CancellationTokenSource _loopCts = new();

    public void Start()
    {
        ObjectDisposedException.ThrowIf(_isDisposed, this);
        if (IsRunning)
            throw new InvalidOperationException("GameLog watcher has already been started.");

        IsRunning = true;
        _ = Task.Factory.StartNew(() => WorkerLoopAsync(_loopCts.Token), TaskCreationOptions.LongRunning);
    }

    public async Task StopAsync()
    {
        ObjectDisposedException.ThrowIf(_isDisposed, this);
        if (!IsRunning)
            throw new InvalidOperationException("GameLog watcher has not been started.");

        IsRunning = false;
        await _loopCts.CancelAsync();
        _loopCts.Dispose();

        _loopCts = new CancellationTokenSource();
    }

    private async Task WorkerLoopAsync(CancellationToken cancellationToken)
    {
        await using var logFileStream =
            File.Open(pathToLogFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete);

        using var logReader = new GameLogReader(logFileStream);
        var logParser = new GameLogEventParser();

        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                var logEntity = await logReader.ReadAsync(cancellationToken);
                var logEvent = logParser.Parse(logEntity);

                OnLog?.Invoke(this, new GameLogEntityWithEvent(logEntity, logEvent));
            }
            catch (OperationCanceledException)
            {
                break;
            }
            catch (Exception ex)
            {
                OnException?.Invoke(this, ex);
            }
        }
    }

    public void Dispose()
    {
        _isDisposed = true;

        _loopCts.Cancel();
        _loopCts.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        _isDisposed = true;

        await _loopCts.CancelAsync();
        _loopCts.Dispose();
    }
}