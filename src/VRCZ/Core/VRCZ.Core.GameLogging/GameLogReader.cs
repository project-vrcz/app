using System.Text;
using VRCZ.Core.GameLogging.Models;

namespace VRCZ.Core.GameLogging;

public sealed class GameLogReader(Stream logStream, bool jumpToEnd = false) : IDisposable
{
    private readonly StreamReader _logStreamReader = new(logStream);

    private readonly StringBuilder _logEntityStringBuilder = new();

    private bool _isLastLoopReachEnd;
    private bool _isLastLoopReachEndHandled;

    private bool _isDisposed;

    private bool _allowCreateLogEntity = !jumpToEnd;

    public async ValueTask<GameLogEntity> ReadAsync(CancellationToken cancellationToken = default)
    {
        ObjectDisposedException.ThrowIf(_isDisposed, this);

        return await Task.Run(() => ReadAsyncCore(cancellationToken), cancellationToken);
    }

    private GameLogEntity ReadAsyncCore(CancellationToken cancellationToken)
    {
        ObjectDisposedException.ThrowIf(_isDisposed, this);
        cancellationToken.ThrowIfCancellationRequested();

        // How this mess works:
        //
        // During each loop, if lineStringBuffer is not empty, check is this line a log entity.
        // If it is, create a log entity from logEntityBuffer (not lineStringBuffer) and write lineStringBuffer to logEntityBuffer.
        // If not, just write lineStringBuffer to logEntityBuffer.
        //
        // Why not just create a log entity from lineStringBuffer: to handle situations when the log entity is split across multiple lines.
        //
        // How the end of stream is handled:
        // (To make sure the EndOfStream was caused by the end of the log file, not file was still being written to)
        //
        // ## When EndOfStream first time:
        //  Mark last loop EndOfStream:
        //  - _isLastLoopReachEndHandled = false
        //  - _isLastLoopReachEnd = true
        // ## When EndOfStream second time:
        //  If current line is a valid log entity, do nothing and continue to next loop.
        //
        //  If not, try to create a log entity from logEntityBuffer and lineStringBuffer,
        //  and set flags: (which will prevent handle EndOfStream logic from running again)
        //  - _isLastLoopReachEndHandled = true
        //  - _isLastLoopReachEnd = true
        //
        // Note: if the stream have new characters after the EndOfStream, both flags will be set to false and continue to next loop.

        var lineBuilder = new StringBuilder();
        string? lineStringBuffer = null;

        while (true)
        {
            ObjectDisposedException.ThrowIf(_isDisposed, this);
            cancellationToken.ThrowIfCancellationRequested();

            if (lineStringBuffer is not null)
            {
                var logEntity = TryCreateLogEntity();

                if (_logEntityStringBuilder.Length > 0)
                    _logEntityStringBuilder.Append(Environment.NewLine);

                _logEntityStringBuilder.Append(lineStringBuffer);
                lineStringBuffer = null;

                if (logEntity is not null)
                    return logEntity;
            }

            var character = _logStreamReader.Read();

            if (character == -1)
            {
                if (_isLastLoopReachEndHandled)
                    continue;

                if (!_isLastLoopReachEnd)
                {
                    _isLastLoopReachEnd = true;
                    _isLastLoopReachEndHandled = false;
                    continue;
                }

                if (HandleEndOfStream() is { } logEntity)
                    return logEntity;

                continue;
            }

            _isLastLoopReachEnd = false;
            _isLastLoopReachEndHandled = false;

            if (character is '\n' or '\r')
            {
                if (_logStreamReader.Peek() == '\n')
                    _logStreamReader.Read();

                lineStringBuffer = lineBuilder.ToString();
                lineBuilder.Clear();
                continue;
            }

            lineBuilder.Append((char)character);
        }

        GameLogEntity? TryCreateLogEntity()
        {
            if (!GameLogEntity.LogRegex.IsMatch(lineStringBuffer))
                return null;

            if (_logEntityStringBuilder.Length <= 0)
                return null;

            if (!_allowCreateLogEntity)
            {
                _logEntityStringBuilder.Clear();
                return null;
            }

            var logEntity = GameLogEntity.Parse(_logEntityStringBuilder.ToString());
            _logEntityStringBuilder.Clear();

            return logEntity;
        }

        GameLogEntity? HandleEndOfStream()
        {
            lineStringBuffer = lineBuilder.ToString();

            if (GameLogEntity.LogRegex.IsMatch(lineStringBuffer))
            {
                return null;
            }

            var currentLogEntityStringBuffer =
                _logEntityStringBuilder +
                (_logEntityStringBuilder.Length != 0 && lineStringBuffer.Length != 0
                    ? Environment.NewLine
                    : "") +
                lineStringBuffer;

            _isLastLoopReachEndHandled = true;

            if (!_allowCreateLogEntity)
            {
                _allowCreateLogEntity = true;
                _logEntityStringBuilder.Clear();
                return null;
            }

            if (!GameLogEntity.LogRegex.IsMatch(currentLogEntityStringBuffer))
                return null;

            _logEntityStringBuilder.Clear();
            return GameLogEntity.Parse(currentLogEntityStringBuffer);
        }
    }

    public void Dispose()
    {
        _isDisposed = true;

        _logStreamReader.Dispose();
        logStream.Dispose();
    }
}