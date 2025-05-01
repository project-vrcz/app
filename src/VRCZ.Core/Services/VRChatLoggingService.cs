using System.Text;
using System.Threading.Channels;
using Microsoft.Extensions.Logging;
using VRCZ.Core.Models.VRChat.Logging;
using VRCZ.Core.Utils;

namespace VRCZ.Core.Services;

public class VRChatLoggingService(ILogger<VRChatLoggingService> logger) : IAsyncDisposable, IDisposable
{
    private const string LogFileNamePattern = "output_log_*.txt";

    private FileSystemWatcher? _fileSystemWatcher;
    public string? CurrentLogFilePath { get; private set; }

    private CancellationTokenSource? _cancellationTokenSource;
    private Channel<VRChatLogEntity>? _logChannel;

    #region Start & Stop

    public Task StartAsync()
    {
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

        _ = ParseLogLoop(latestLogPath, async entity => { await _logChannel.Writer.WriteAsync(entity); },
            _cancellationTokenSource.Token, true);
    }

    public async Task StopAsync()
    {
        if (_cancellationTokenSource is not null)
        {
            await _cancellationTokenSource.CancelAsync();
            _cancellationTokenSource.Dispose();
        }

        _fileSystemWatcher?.Dispose();

        _logChannel = null;
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
            }
        }
        catch (OperationCanceledException)
        {
            logger.LogInformation("Stopped log monitoring");
        }
        catch (Exception ex)
        {
            logger.LogError(ex,
                "Error in monitoring log, monitoring will be stopped until next log file is created");
        }
    }

    #endregion

    #region Streaming Log Parse

    public async Task ParseLogLoop(string filePath, Func<VRChatLogEntity, Task> onLogEntityCreated,
        CancellationToken cancellationToken, bool jumpToEnd = false)
    {
        try
        {
            await Task.Run(
                async () => { await ParseLogLoopCore(filePath, onLogEntityCreated, cancellationToken, jumpToEnd); },
                cancellationToken);
        }
        catch
        {
            // ignore
        }
    }

    private async Task ParseLogLoopCore(string filePath, Func<VRChatLogEntity, Task> onLogEntityCreated,
        CancellationToken cancellationToken, bool jumpToEnd)
    {
        await using var logFileStream =
            File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete);
        using var logStreamReader = new StreamReader(logFileStream);

        using var stringReader = new StreamReader(logFileStream);

        var logEntityStringBuilder = new StringBuilder();

        string? lineStringBuffer = null;
        var lineBuilder = new StringBuilder();

        var isLastLoopReachEnd = false;
        var isLastLoopReachEndHandled = false;

        var allowCreateLogEntity = !jumpToEnd;

        while (!cancellationToken.IsCancellationRequested)
        {
            if (lineStringBuffer is not null)
            {
                if (VRChatLogEntity.LogRegex.IsMatch(lineStringBuffer))
                {
                    if (logEntityStringBuilder.Length > 0)
                    {
                        if (allowCreateLogEntity)
                            await CreateLogEntity(logEntityStringBuilder.ToString());
                        else
                            logEntityStringBuilder.Clear();
                    }
                }

                if (logEntityStringBuilder.Length > 0)
                    logEntityStringBuilder.Append(Environment.NewLine);

                logEntityStringBuilder.Append(lineStringBuffer);
                lineStringBuffer = null;
            }

            var character = stringReader.Read();

            if (character == -1)
            {
                if (!allowCreateLogEntity)
                    allowCreateLogEntity = true;

                if (!isLastLoopReachEnd)
                {
                    isLastLoopReachEnd = true;
                    isLastLoopReachEndHandled = false;
                    continue;
                }

                if (isLastLoopReachEndHandled)
                    continue;

                var currentLineStringBuffer = lineBuilder.ToString();
                var currentLogEntityStringBuffer =
                    logEntityStringBuilder +
                    (logEntityStringBuilder.Length != 0 && currentLineStringBuffer.Length != 0
                        ? Environment.NewLine
                        : "") +
                    currentLineStringBuffer;

                if (VRChatLogEntity.LogRegex.IsMatch(currentLogEntityStringBuffer))
                {
                    await CreateLogEntity(currentLogEntityStringBuffer);
                }

                isLastLoopReachEndHandled = true;

                continue;
            }

            isLastLoopReachEnd = false;
            isLastLoopReachEndHandled = false;

            if (character is '\n' or '\r')
            {
                if (stringReader.Peek() == '\n')
                    stringReader.Read();

                lineStringBuffer = lineBuilder.ToString();
                lineBuilder.Clear();
                continue;
            }

            lineBuilder.Append((char)character);
        }

        async Task CreateLogEntity(string logEntityString)
        {
            var logEntity = VRChatLogEntity.Parse(logEntityString);
            await onLogEntityCreated(logEntity);

            logEntityStringBuilder.Clear();
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
        _cancellationTokenSource?.Cancel();

        _fileSystemWatcher?.Dispose();
        _cancellationTokenSource?.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        if (_cancellationTokenSource is not null)
            await _cancellationTokenSource.CancelAsync();

        Dispose();
    }
}
