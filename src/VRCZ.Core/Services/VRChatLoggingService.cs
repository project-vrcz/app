using System.Text;
using System.Text.RegularExpressions;
using VRCZ.Core.Models.VRChat.Logging;
using VRCZ.Core.Utils;

namespace VRCZ.Core.Services;

public class VRChatLoggingService
{
    public async Task ParseLogLoop(string filePath, List<VRChatLogEntity> logEntities,
        Action<VRChatLogEntity> onLogEntityCreated,
        CancellationToken cancellationToken)
    {
        await Task.Run(async () =>
        {
            await ParseLogLoopInternal(filePath, logEntities, onLogEntityCreated, cancellationToken);
        }, cancellationToken);
    }

    private async Task ParseLogLoopInternal(string filePath, List<VRChatLogEntity> logEntities, Action<VRChatLogEntity> onLogEntityCreated,
        CancellationToken cancellationToken)
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

        while (!cancellationToken.IsCancellationRequested)
        {
            if (lineStringBuffer is not null)
            {
                if (VRChatLogEntity.LogRegex.IsMatch(lineStringBuffer))
                {
                    if (logEntityStringBuilder.Length > 0)
                    {
                        var currentLogEntityStringBuffer = logEntityStringBuilder.ToString();

                        CreateLogEntity(currentLogEntityStringBuffer);
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
                    (logEntityStringBuilder.Length != 0 && currentLineStringBuffer.Length != 0 ? Environment.NewLine : "") +
                    currentLineStringBuffer;

                if (VRChatLogEntity.LogRegex.IsMatch(currentLogEntityStringBuffer))
                {
                    CreateLogEntity(currentLogEntityStringBuffer);
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

        void CreateLogEntity(string logEntityString)
        {
            logEntities.Add(VRChatLogEntity.Parse(logEntityString));
            onLogEntityCreated(logEntities.Last());

            logEntityStringBuilder.Clear();
        }
    }

    public string[] GetVRChatLogPaths()
    {
        var storageRoot = VRChatStorageUtils.GetVRChatStorageRootPath();

        return Directory.GetFiles(storageRoot, "output_log_*.txt", SearchOption.TopDirectoryOnly);
    }

    public static async void Test()
    {
        var paths = new VRChatLoggingService().GetVRChatLogPaths();
        if (paths.Length == 0)
        {
            Console.WriteLine("No VRChat log files found.");
            return;
        }

        foreach (var path in paths)
        {
            Console.WriteLine($"Found log file: {path}");
        }

        await new VRChatLoggingService().ParseLogLoop(
            paths.Last(), [],
            log => { Console.WriteLine($"[{log.Timestamp:yyyy-MM-dd HH:mm:ss}][{log.LogLevel}] {log.Message}"); },
            CancellationToken.None);
    }
}
