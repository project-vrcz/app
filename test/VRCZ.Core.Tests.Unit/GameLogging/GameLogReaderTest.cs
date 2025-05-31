using System.Text;
using Microsoft.Kiota.Abstractions;
using VRCZ.Core.GameLogging;
using VRCZ.Core.Models.VRChat.Logging;

namespace VRCZ.Core.Tests.Unit.GameLogging;

public class GameLogReaderTest
{
    private static readonly VRChatLogEntity[] SingleLineEntities =
    [
        new(
            DateTime.Parse("2025.05.24 22:23:53"),
            "Debug",
            "Odin Serializer ArchitectureInfo initialization with defaults (all unaligned read/writes disabled)."
        ),
        new(
            DateTime.Parse("2025.05.24 22:23:53"),
            "Debug",
            "Odin Serializer detected whitelisted runtime platform WindowsPlayer and memory read test succeeded; enabling all unaligned memory read/writes."
        ),
        new(
            DateTime.Parse("2025.05.24 22:23:55"),
            "Debug",
            "initialized Steam connection"
        ),
        new(
            DateTime.Parse("2025.05.24 22:23:55"),
            "Debug",
            "Using server environment: Release, bf0942f7"
        ),
        new(
            DateTime.Parse("2025.05.24 22:23:55"),
            "Debug",
            "[VRCInputManager] Safety level set to: None"
        ),
        new(
            DateTime.Parse("2025.05.24 22:23:55"),
            "Debug",
            "Launching with args: 5"
        ),
        new(
            DateTime.Parse("2025.05.24 22:23:55"),
            "Debug",
            "Arg: E:\\Program Files (x86)\\Steam\\steamapps\\common\\VRChat\\VRChat.exe"
        ),
        new(
            DateTime.Parse("2025.05.24 22:23:55"),
            "Debug",
            "Arg: --no-vr"
        ),
        new(
            DateTime.Parse("2025.05.24 22:23:55"),
            "Debug",
            "Arg: --enable-debug-gui"
        ),
    ];

    private static string SingleLineEntityLog => SingleLineEntityLogWithoutLastEmptyLine + Environment.NewLine;

    private static string SingleLineEntityLogWithoutLastEmptyLine => GetLogText(SingleLineEntities);

    private static readonly VRChatLogEntity[] MultiLineEntities =
    [
        new(
            DateTime.Parse("2025.05.24 22:23:58"),
            "Warning",
            "<b>[SteamVR]</b> Initialization failed. Please verify that you have SteamVR installed, your hmd is functioning, and OpenVR Loader is checked in the XR Plugin Management section of Project Settings."
        ),
        new(
            DateTime.Parse("2025.05.24 22:23:58"),
            "Error",
            "SteamVR instance or camera is null. Disabling culling fix."
        ),
        new(
            DateTime.Parse("2025.05.24 22:23:58"),
            "Error",
            "AmplitudeAPI: upload failed with exception - Could not resolve host 'api2.amplitude.com'\n" +
            "at System.Net.Dns.Error_11001 (System.String hostName) [0x00000] in <00000000000000000000000000000000>:0 " +
            "at System.Net.Dns.hostent_to_IPHostEntry (System.String originalHostName, System.String h_name, System.String[] h_aliases, System.String[] h_addrlist) [0x00000] in <0000000000000000000000000000000 \n" +
            "at System.Net.Dns.GetHostByName (System.String hostName) [0x00000] in <00000000000000000000000000000000>:0 \n" +
            "at System.Net.Dns.GetHostEntry (System.String hostNameOrAddress) [0x00000] in <00000000000000000000000000000000>:0 \n" +
            "at System.Net.Dns.GetHostAddresses (System.String hostNameOrAddress) [0x00000] in <00000000000000000000000000000000>:0 \n" +
            "at System.Runtime.Remoting.Messaging.AsyncResult.System.Threading.IThreadPoolWorkItem.ExecuteWorkItem () [0x00000] in <00000000000000000000000000000000>:0 \n" +
            "at System.Threading.ThreadPoolWorkQueue.Dispatch () [0x00000] in <00000000000000000000000000000000>:0 "
        )
    ];

    private static string MultiLineEntityLog => MultiLineEntityLogWithoutLastEmptyLine + Environment.NewLine;

    private static readonly string MultiLineEntityLogWithoutLastEmptyLine = GetLogText(MultiLineEntities);

    private static string GetLogText(IEnumerable<VRChatLogEntity> entities)
    {
        return entities.Select(entity =>
                $"{entity.Timestamp:yyyy.MM.dd HH:mm:ss} {entity.LogLevel,-11}-  {entity.Message}")
            .Aggregate((current, next) => $"{current}{Environment.NewLine}{next}");
    }

    private async ValueTask<VRChatLogEntity[]> GetLogEntities(VRChatGameLogReader reader, int expectedCount)
    {
        List<VRChatLogEntity> entities = [];
        for (var count = 0; count < expectedCount; count++)
        {
            var entity = await reader.ReadAsync(TestContext.Current.CancellationToken);
            entities.Add(entity);
        }

        return entities.ToArray();
    }

    #region Simple Read Entities Tests

    [Fact(Timeout = 1000)]
    public async Task Read_SingleLineEntities_ShouldParseCorrectly()
    {
        await Read_Common_EntitiesTest_ShouldParseCorrectly(SingleLineEntities, SingleLineEntityLog);
    }

    [Fact(Timeout = 1000)]
    public async Task Read_SingleLineEntitiesWithoutLastEmptyLine_ShouldParseCorrectly()
    {
        await Read_Common_EntitiesTest_ShouldParseCorrectly(SingleLineEntities,
            SingleLineEntityLogWithoutLastEmptyLine);
    }

    [Fact(Timeout = 1000)]
    public async Task Read_MultiLineEntities_ShouldParseCorrectly()
    {
        await Read_Common_EntitiesTest_ShouldParseCorrectly(MultiLineEntities, MultiLineEntityLog);
    }

    [Fact(Timeout = 1000)]
    public async Task Read_MultiLineEntitiesWithoutLastEmptyLine_ShouldParseCorrectly()
    {
        await Read_Common_EntitiesTest_ShouldParseCorrectly(MultiLineEntities, MultiLineEntityLogWithoutLastEmptyLine);
    }

    private async Task Read_Common_EntitiesTest_ShouldParseCorrectly(VRChatLogEntity[] expectedEntities,
        string rawLogText)
    {
        // Arrange
        using var stream = new MemoryStream();
        await using var writer = new StreamWriter(stream);

        await writer.WriteAsync(rawLogText);
        await writer.FlushAsync(TestContext.Current.CancellationToken);
        stream.Position = 0;

        using var reader = new VRChatGameLogReader(stream, jumpToEnd: false);

        // Act
        var entities = await GetLogEntities(reader, expectedEntities.Length);

        // Assert
        Assert.Equal(expectedEntities, entities);
    }

    #endregion

    #region JumpToEnd Tests

    [Fact(Timeout = 1000)]
    public async Task Read_JumpToEnd_ShouldParseCorrectly()
    {
        // Arrange
        var expectedEntities = MultiLineEntities;
        var expectedRawLogText = MultiLineEntityLog;

        var firstRawLogText = MultiLineEntityLog;

        using var stream = new MemoryStream();
        await using var writer = new StreamWriter(stream);

        await writer.WriteAsync(firstRawLogText);
        await writer.FlushAsync(TestContext.Current.CancellationToken);
        stream.Position = 0;

        using var reader = new VRChatGameLogReader(stream, jumpToEnd: true);

        using var cts = new CancellationTokenSource();
        cts.CancelAfter(TimeSpan.FromMilliseconds(100));

        // Act
        TestContext.Current.TestOutputHelper?.WriteLine("Try Jump to end");
        await Assert.ThrowsAsync<OperationCanceledException>(async () => await reader.ReadAsync(cts.Token));
        TestContext.Current.TestOutputHelper?.WriteLine("Jump to end completed");

        var lastStreamPosition = stream.Position;

        await writer.WriteAsync(expectedRawLogText);
        await writer.FlushAsync(TestContext.Current.CancellationToken);

        stream.Position = lastStreamPosition;

        TestContext.Current.TestOutputHelper?.WriteLine("Read MultiLineEntities after jump to end");

        var entities = await GetLogEntities(reader, expectedEntities.Length);

        // Assert
        Assert.Equal(expectedEntities, entities);
    }

    [Fact(Timeout = 5000)]
    public async Task Read_JumpToEnd_InSingleReadCall_ShouldParseCorrectly()
    {
        // use MemoryStream will cause a race condition, so use FileStream instead
        var tempLogFilePath = Path.GetTempFileName();

        await using var tempLogFileStreamToWrite = File.Open(tempLogFilePath, FileMode.Open, FileAccess.Write,
            FileShare.ReadWrite | FileShare.Delete);
        await using var tempLogFileStreamToRead = File.Open(tempLogFilePath, FileMode.Open, FileAccess.Read,
            FileShare.ReadWrite | FileShare.Delete);

        await using var writer = new StreamWriter(tempLogFileStreamToWrite);

        // Write Exist Log Content To File
        await writer.WriteAsync(SingleLineEntityLog);
        await writer.FlushAsync(TestContext.Current.CancellationToken);

        using var reader = new VRChatGameLogReader(tempLogFileStreamToRead, jumpToEnd: true);

        // Start Reader Read Operation
        var tcs = new TaskCompletionSource();
        _ = Task.Run(async () =>
        {
            try
            {
                foreach (var expectedEntity in MultiLineEntities)
                {
                    // ReSharper disable once AccessToDisposedClosure
                    // reader.Dispose() will only be called after this task finish
                    var entity = await reader.ReadAsync(TestContext.Current.CancellationToken);
                    Assert.Equal(expectedEntity, entity);
                }

                tcs.SetResult();
            }
            catch (Exception ex)
            {
                tcs.SetException(ex);
            }
        }, TestContext.Current.CancellationToken);

        // Wait for reader to jump to end
        TestContext.Current.TestOutputHelper?.WriteLine("Wait a second to let reader jump to end");

        await Task.Delay(TimeSpan.FromSeconds(1), TestContext.Current.CancellationToken);

        // Write New Log Entities To File
        TestContext.Current.TestOutputHelper?.WriteLine("Start writing MultiLineEntities to stream");

        await writer.WriteAsync(MultiLineEntityLog);
        await writer.FlushAsync(TestContext.Current.CancellationToken);

        // Wait Reader Finish Reading or Throw Exception
        await tcs.Task;
    }

    #endregion

    [Fact(Timeout = 1000)]
    public async Task Read_NewLogEntityAfterEndOfStream_ShouldParseCorrectly()
    {
        // Arrange
        var expectedEntities = SingleLineEntities;
        var rawLogText = SingleLineEntityLog;

        using var stream = new MemoryStream();
        await using var writer = new StreamWriter(stream);

        await writer.WriteAsync(rawLogText);
        await writer.FlushAsync(TestContext.Current.CancellationToken);
        stream.Position = 0;

        // Act
        using var reader = new VRChatGameLogReader(stream, jumpToEnd: false);

        // First Call
        var firstEntities = await GetLogEntities(reader, expectedEntities.Length);

        TestContext.Current.TestOutputHelper?.WriteLine("Write New Log Entity To Stream");

        var lastStreamPosition = stream.Position;

        // Last Call
        await writer.WriteAsync(rawLogText);
        await writer.FlushAsync(TestContext.Current.CancellationToken);

        stream.Position = lastStreamPosition;

        // Assert
        var lastEntities = await GetLogEntities(reader, expectedEntities.Length);

        TestContext.Current.TestOutputHelper?.WriteLine("Assert First Part");
        Assert.Equal(expectedEntities, firstEntities);
        TestContext.Current.TestOutputHelper?.WriteLine("Assert Last Part");
        Assert.Equal(expectedEntities, lastEntities);
    }

    [Fact(Timeout = 5000)]
    public async Task Read_EmptyStream_CancelCancellationTokenAfterStart_ShouldOperationCanceledException()
    {
        // Arrange
        using var stream = new MemoryStream();

        using var reader = new VRChatGameLogReader(stream, jumpToEnd: false);

        var cts = new CancellationTokenSource();
        cts.CancelAfter(TimeSpan.FromSeconds(2));

        // Act & Assert
        await Assert.ThrowsAsync<OperationCanceledException>(async () =>
            await reader.ReadAsync(cts.Token));
    }

    [Fact(Timeout = 1000)]
    public async Task Read_EmptyStream_CancelCancellationTokenBeforeStart_ShouldOperationCanceledException()
    {
        // Arrange
        using var stream = new MemoryStream();

        using var reader = new VRChatGameLogReader(stream, jumpToEnd: false);

        var cts = new CancellationTokenSource();
        await cts.CancelAsync();

        // Act & Assert
        await Assert.ThrowsAsync<TaskCanceledException>(async () =>
            await reader.ReadAsync(cts.Token));
    }

    [Fact(Timeout = 1000)]
    public async Task Read_ShouldThrowObjectDisposedException_WhenDisposed()
    {
        // Arrange
        using var stream = new MemoryStream();
        var reader = new VRChatGameLogReader(stream, jumpToEnd: false);

        reader.Dispose();

        // Act & Assert
        await Assert.ThrowsAsync<ObjectDisposedException>(async () =>
            await reader.ReadAsync(TestContext.Current.CancellationToken));
    }
}
