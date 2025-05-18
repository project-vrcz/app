using System.Text;
using System.Threading.Channels;
using Microsoft.Extensions.Logging;
using VRCZ.Core.GameLogging;
using VRCZ.Core.Models.VRChat.Logging;
using VRCZ.Core.Utils;

namespace VRCZ.Core.Services.Tracking;

public class VRChatLoggingService(ILogger<VRChatLoggingService> logger) : IAsyncDisposable, IDisposable
{
    private const string LogFileNamePattern = "output_log_*.txt";

    private FileSystemWatcher? _fileSystemWatcher;
    public string? CurrentLogFilePath { get; private set; }

    private CancellationTokenSource? _cancellationTokenSource;
    private Channel<VRChatLogEntity>? _logChannel;

    private bool _disposed;

    #region Start & Stop

    public Task StartAsync()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);

        _fileSystemWatcher = new FileSystemWatcher(VRChatStorageUtils.GetVRChatStorageRootPath());

        _fileSystemWatcher.Created += async (_, args) =>
        {
            if (args.ChangeType != WatcherChangeTypes.Created)
                return;

            await StartAsyncCore();
        };

        _fileSystemWatcher.NotifyFilter = NotifyFilters.FileName;
        _fileSystemWatcher.Filter = LogFileNamePattern;

        _fileSystemWatcher.IncludeSubdirectories = false;
        _fileSystemWatcher.EnableRaisingEvents = true;

        return Task.CompletedTask;
    }

    private async Task StartAsyncCore()
    {
        if (_cancellationTokenSource is not null)
        {
            await _cancellationTokenSource.CancelAsync();
            _cancellationTokenSource.Dispose();
        }

        var logPaths = GetVRChatLogPaths();

        if (logPaths.Length == 0)
        {
            logger.LogInformation("No VRChat log files found");

            CurrentLogFilePath = null;
            return;
        }

        var latestLogPath = logPaths.OrderByDescending(x => x).First();

        CurrentLogFilePath = latestLogPath;

        logger.LogInformation("Starting to monitor log file: {LogFilePath}", latestLogPath);

        _cancellationTokenSource = new CancellationTokenSource();

        _logChannel = Channel.CreateUnbounded<VRChatLogEntity>();

        _ = HandleLogEntityLoop(_cancellationTokenSource.Token);

        _ = ParseLogLoop(latestLogPath, _logChannel, true, _cancellationTokenSource.Token);
    }

    public async Task StopAsync()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);

        if (_cancellationTokenSource is not null)
        {
            await _cancellationTokenSource.CancelAsync();
            _cancellationTokenSource.Dispose();
        }

        _fileSystemWatcher?.Dispose();

        _logChannel = null;

        CurrentLogFilePath = null;
    }

    #endregion

    #region Log Monitoring

    private async Task HandleLogEntityLoop(CancellationToken cancellationToken)
    {
        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                if (_logChannel is null)
                {
                    throw new InvalidOperationException(
                        "Log channel is null. Ensure that StartAsyncCore has been called.");
                }

                var logEntity = await _logChannel.Reader.ReadAsync(cancellationToken);

                logger.LogDebug("New log entity: {LogEntity}", logEntity);

                // TODO: Log Event Handle
            }
        }
        catch (OperationCanceledException)
        {
            logger.LogInformation("Stopped log monitoring");
        }
    }

    #endregion

    #region Streaming Log Parse

    private async Task ParseLogLoop(string filePath, Channel<VRChatLogEntity> channel, bool jumpToEnd = false,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await Task.Run(
                async () => { await ParseLogLoopCore(filePath, jumpToEnd, channel, cancellationToken); },
                cancellationToken);
        }
        catch (OperationCanceledException)
        {
            // ignore
        }
        catch (Exception ex)
        {
            logger.LogError(ex,
                "Error in log file parsing, log file will be monitored until next log file is created");
            await StopAsync();
        }
    }

    private async Task ParseLogLoopCore(string filePath, bool jumpToEnd, Channel<VRChatLogEntity> channel,
        CancellationToken cancellationToken = default)
    {
        await using var logFileStream =
            File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete);

        using var logReader = new VRChatGameLogReader(logFileStream, jumpToEnd);

        while (!cancellationToken.IsCancellationRequested)
        {
            VRChatLogEntity logEntity;
            try
            {
                logEntity = await logReader.ReadAsync(cancellationToken);
            }
            catch (OperationCanceledException)
            {
                break;
            }

            await channel.Writer.WriteAsync(logEntity, cancellationToken);
        }
    }

    #endregion

    public string[] GetVRChatLogPaths()
    {
        var storageRoot = VRChatStorageUtils.GetVRChatStorageRootPath();

        return Directory.GetFiles(storageRoot, LogFileNamePattern, SearchOption.TopDirectoryOnly);
    }

    public void Dispose()
    {
        _disposed = true;

        _cancellationTokenSource?.Cancel();

        _fileSystemWatcher?.Dispose();
        _cancellationTokenSource?.Dispose();

        CurrentLogFilePath = null;

        GC.SuppressFinalize(this);
    }

    public async ValueTask DisposeAsync()
    {
        _disposed = true;

        if (_cancellationTokenSource is not null)
            await _cancellationTokenSource.CancelAsync();

        Dispose();
    }
}
