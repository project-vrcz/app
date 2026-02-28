using Microsoft.Extensions.Hosting;
using VRCZ.Core.Shared;

namespace VRCZ.Core.GameLogging.Services;

public class GameLogFileSystemWatcherService : IHostedService, IDisposable
{
    public event EventHandler<string>? LogFileCreated;
    public event EventHandler<string>? LogFileDeleted;

    private FileSystemWatcher? _fileSystemWatcher;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _fileSystemWatcher = new FileSystemWatcher(VRChatStorageUtils.GetVRChatStorageRootPath());

        _fileSystemWatcher.Created += (_, args) => LogFileCreated?.Invoke(this, args.FullPath);
        _fileSystemWatcher.Deleted += (_, args) => LogFileDeleted?.Invoke(this, args.FullPath);
        _fileSystemWatcher.Renamed += (_, args) =>
        {
            LogFileDeleted?.Invoke(this, args.FullPath);
            LogFileCreated?.Invoke(this, args.FullPath);
        };

        _fileSystemWatcher.NotifyFilter = NotifyFilters.FileName;
        _fileSystemWatcher.Filter = VRChatStorageUtils.LogFileNamePattern;

        _fileSystemWatcher.IncludeSubdirectories = false;
        _fileSystemWatcher.EnableRaisingEvents = true;

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _fileSystemWatcher?.Dispose();
    }
}