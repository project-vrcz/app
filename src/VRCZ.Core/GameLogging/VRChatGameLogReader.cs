using System.Text;
using VRCZ.Core.Models.VRChat.Logging;

namespace VRCZ.Core.GameLogging;

public sealed class VRChatGameLogReader : IDisposable
{
    private readonly Stream _logStream;
    private readonly StreamReader _logStreamReader;

    private readonly StringBuilder _logEntityStringBuilder = new ();

    private bool _isLastLoopReachEnd;
    private bool _isLastLoopReachEndHandled;

    private bool _isDisposed;

    private bool _jumpToEnd;

    public VRChatGameLogReader(Stream logStream, bool jumpToEnd = false)
    {
        _logStream = logStream;
        _logStreamReader = new StreamReader(logStream);
        _jumpToEnd = jumpToEnd;
    }

    public async ValueTask<VRChatLogEntity> ReadAsync(CancellationToken cancellationToken = default)
    {
        ObjectDisposedException.ThrowIf(_isDisposed, this);

        return await Task.Run(Read, cancellationToken);
    }

    public VRChatLogEntity Read()
    {
        ObjectDisposedException.ThrowIf(_isDisposed, this);

        var lineBuilder = new StringBuilder();
        string? lineStringBuffer = null;

        var allowCreateLogEntity = !_jumpToEnd;

        while (true)
        {
            ObjectDisposedException.ThrowIf(_isDisposed, this);

            if (lineStringBuffer is not null)
            {
                VRChatLogEntity? logEntity = null;
                if (VRChatLogEntity.LogRegex.IsMatch(lineStringBuffer))
                {
                    if (_logEntityStringBuilder.Length > 0 && allowCreateLogEntity)
                    {
                        logEntity = VRChatLogEntity.Parse(_logEntityStringBuilder.ToString());
                        _logEntityStringBuilder.Clear();
                    }
                }

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
                if (!allowCreateLogEntity)
                    allowCreateLogEntity = true;

                if (!_isLastLoopReachEnd)
                {
                    _isLastLoopReachEnd = true;
                    _isLastLoopReachEndHandled = false;
                    continue;
                }

                if (_isLastLoopReachEndHandled)
                    continue;

                var currentLineStringBuffer = lineBuilder.ToString();
                var currentLogEntityStringBuffer =
                    _logEntityStringBuilder +
                    (_logEntityStringBuilder.Length != 0 && currentLineStringBuffer.Length != 0
                        ? Environment.NewLine
                        : "") +
                    currentLineStringBuffer;

                _isLastLoopReachEndHandled = true;

                if (VRChatLogEntity.LogRegex.IsMatch(currentLogEntityStringBuffer))
                {
                    _logEntityStringBuilder.Clear();
                    return VRChatLogEntity.Parse(currentLogEntityStringBuffer);
                }

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
    }

    public void Dispose()
    {
        _isDisposed = true;

        _logStreamReader.Dispose();
    }
}
