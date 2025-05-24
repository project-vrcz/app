using VRCZ.Core.Models.VRChat.Logging;

namespace VRCZ.Core.Tests.Unit.GameLogging;

public class VRChatLogEntityTest
{
    private const string InvalidLogString = "Invalid log format";

    private static readonly VRChatLogEntity[] SingleLineLogEntities =
    [
        new(DateTime.Parse("2025.05.24 11:57:56"), "Warning",
            "NativeAppAPI.GetAnalyticsSessionId() called but no native app API implementation is available"),
        new(DateTime.Parse("2025.05.24 11:57:57"), "Debug",
            "No local config found at 'C:/Users/lipww/AppData/LocalLow/VRChat/VRChat\\config.json'"),
        new(DateTime.Parse("2025.05.24 11:57:58"), "Error",
            "SteamVR instance or camera is null. Disabling culling fix."),
    ];

    private static readonly VRChatLogEntity[] MultiLineLogEntities =
    [
        new(DateTime.Parse("2025.05.24 11:58:01"), "Error",
            "AmplitudeAPI: upload failed with exception - Could not resolve host 'api2.amplitude.com'\n" +
            "at System.Net.Dns.Error_11001 (System.String hostName) [0x00000] in <00000000000000000000000000000000>:0 \n" +
            "at System.Net.Dns.hostent_to_IPHostEntry (System.String originalHostName, System.String h_name, System.String[] h_aliases, System.String[] h_addrlist) [0x00000] in <00000000000000000000000000000000>:0 \n" +
            "at System.Net.Dns.GetHostByName (System.String hostName) [0x00000] in <00000000000000000000000000000000>:0 \n" +
            "at System.Net.Dns.GetHostEntry (System.String hostNameOrAddress) [0x00000] in <00000000000000000000000000000000>:0 \n" +
            "at System.Net.Dns.GetHostAddresses (System.String hostNameOrAddress) [0x00000] in <00000000000000000000000000000000>:0 \n" +
            "at System.Runtime.Remoting.Messaging.AsyncResult.System.Threading.IThreadPoolWorkItem.ExecuteWorkItem () [0x00000] in <00000000000000000000000000000000>:0 \n" +
            "at System.Threading.ThreadPoolWorkQueue.Dispatch () [0x00000] in <00000000000000000000000000000000>:0 "
        ),
        new(DateTime.Parse("2025.05.24 11:57:59"), "Debug",
            "[UserInfoLogger] User Settings Info:\n" +
            "General Settings:\n" +
            "    Clear cache on start: False\n" +
            "    Show go button on load: False\n" +
            "    Show community labs: True\n" +
            "    Show community worlds in search: True\n" +
            "    Hide notification photos: False\n" +
            "    Selected network region: Japan\n" +
            "    OSC enabled: False\n" +
            "    AFK enabled: False\n" +
            "Graphics Settings:\n" +
            "    Quality: High\n" +
            "    Antialiasing: x8\n" +
            "Input Settings:\n" +
            "    Mouse sensitivity: 1\n" +
            "    Inverted mouse: False\n" +
            "    UI Haptics enabled: True\n" +
            "Audio Settings:\n" +
            "    Legacy Talk toggle: False\n" +
            "    Legacy Talk default on: False\n" +
            "    Legacy Disable mic button: False\n" +
            "    Mic Mode: PushToTalk\n" +
            "    Mic Behavior On Join: DefaultOff\n" +
            "    Mic level (VR): 0.8\n" +
            "    Mic level (Desktop): 1\n" +
            "    Noise suppression: True\n" +
            "    Noise gate: 0.01\n" +
            "UI Settings:\n" +
            "    Show tooltips: True\n" +
            "    Desktop reticle: True\n" +
            "    Show social rank: False\n" +
            "    Main Menu free placement: True\n" +
            "    Nameplate display: Standard\n" +
            "    QM Info: on\n" +
            "    Fallback Icon: visible\n" +
            "    Status mode: ShowOnMenu\n" +
            "    Nameplate scale: 100%\n" +
            "    Nameplate opacity: 80%\n" +
            "Avatar Settings:\n" +
            "    Fallback hidden: False\n" +
            "    Maximum download size: No limit\n" +
            "    Performance rating minimum to display: VeryPoor\n" +
            "    Allow avatar copying: False\n" +
            "    Disable avatar cloning on enter world: True\n" +
            "    Avatar interaction level: All\n" +
            "    Avatar self-interaction: True\n" +
            "Accessibility Settings:\n" +
            "    Gesture bar enabled: False\n" +
            "    Earmuff mode: False\n" +
            "    Earmuff mode (avatars): True\n" +
            "    Earmuff mode, Always show visual aid: True\n" +
            "    Earmuff mode radius: 0.3\n" +
            "    Earmuff mode falloff: 0\n" +
            "    Earmuff mode reduced volume: 0.15\n" +
            "    Chat bubble visibility: Everyone\n" +
            "    Chat bubble scale: 1.5\n" +
            "    Chat bubble opacity: 0.75\n" +
            "    Chat bubble timeout: 30\n" +
            "Comfort & Safety Settings:\n" +
            "    Personal space: False\n" +
            "    Voice prioritization: True\n" +
            "    Third person rotation: False\n" +
            "    Comfort turning: False\n" +
            "    Head look walking: True\n" +
            "    Locomotion method: Gamelike\n" +
            "    Vive advanced: False\n" +
            "    Streamer mode enabled: False\n" +
            "    Limit particle systems: False\n" +
            "    Safety level: None\n" +
            "    Allow untrusted URLs: True\n" +
            "    Ask to portal: True\n" +
            "    Mirror resolution: Full\n" +
            "    Portal Mode: PlaceManually\n"
        ),
    ];

    private static readonly string[] SingleLineLogStrings =
    [
        "2025.05.24 11:57:56 Warning    -  NativeAppAPI.GetAnalyticsSessionId() called but no native app API implementation is available",
        "2025.05.24 11:57:57 Debug      -  No local config found at 'C:/Users/lipww/AppData/LocalLow/VRChat/VRChat\\config.json'",
        "2025.05.24 11:57:58 Error      -  SteamVR instance or camera is null. Disabling culling fix.",
    ];

    private static readonly string[] MultiLineLogStrings =
    [
        "2025.05.24 11:58:01 Error      -  AmplitudeAPI: upload failed with exception - Could not resolve host 'api2.amplitude.com'\n" +
        "at System.Net.Dns.Error_11001 (System.String hostName) [0x00000] in <00000000000000000000000000000000>:0 \n" +
        "at System.Net.Dns.hostent_to_IPHostEntry (System.String originalHostName, System.String h_name, System.String[] h_aliases, System.String[] h_addrlist) [0x00000] in <00000000000000000000000000000000>:0 \n" +
        "at System.Net.Dns.GetHostByName (System.String hostName) [0x00000] in <00000000000000000000000000000000>:0 \n" +
        "at System.Net.Dns.GetHostEntry (System.String hostNameOrAddress) [0x00000] in <00000000000000000000000000000000>:0 \n" +
        "at System.Net.Dns.GetHostAddresses (System.String hostNameOrAddress) [0x00000] in <00000000000000000000000000000000>:0 \n" +
        "at System.Runtime.Remoting.Messaging.AsyncResult.System.Threading.IThreadPoolWorkItem.ExecuteWorkItem () [0x00000] in <00000000000000000000000000000000>:0 \n" +
        "at System.Threading.ThreadPoolWorkQueue.Dispatch () [0x00000] in <00000000000000000000000000000000>:0 ",

        "2025.05.24 11:57:59 Debug      -  [UserInfoLogger] User Settings Info:\n" +
        "General Settings:\n" +
        "    Clear cache on start: False\n" +
        "    Show go button on load: False\n" +
        "    Show community labs: True\n" +
        "    Show community worlds in search: True\n" +
        "    Hide notification photos: False\n" +
        "    Selected network region: Japan\n" +
        "    OSC enabled: False\n" +
        "    AFK enabled: False\n" +
        "Graphics Settings:\n" +
        "    Quality: High\n" +
        "    Antialiasing: x8\n" +
        "Input Settings:\n" +
        "    Mouse sensitivity: 1\n" +
        "    Inverted mouse: False\n" +
        "    UI Haptics enabled: True\n" +
        "Audio Settings:\n" +
        "    Legacy Talk toggle: False\n" +
        "    Legacy Talk default on: False\n" +
        "    Legacy Disable mic button: False\n" +
        "    Mic Mode: PushToTalk\n" +
        "    Mic Behavior On Join: DefaultOff\n" +
        "    Mic level (VR): 0.8\n" +
        "    Mic level (Desktop): 1\n" +
        "    Noise suppression: True\n" +
        "    Noise gate: 0.01\n" +
        "UI Settings:\n" +
        "    Show tooltips: True\n" +
        "    Desktop reticle: True\n" +
        "    Show social rank: False\n" +
        "    Main Menu free placement: True\n" +
        "    Nameplate display: Standard\n" +
        "    QM Info: on\n" +
        "    Fallback Icon: visible\n" +
        "    Status mode: ShowOnMenu\n" +
        "    Nameplate scale: 100%\n" +
        "    Nameplate opacity: 80%\n" +
        "Avatar Settings:\n" +
        "    Fallback hidden: False\n" +
        "    Maximum download size: No limit\n" +
        "    Performance rating minimum to display: VeryPoor\n" +
        "    Allow avatar copying: False\n" +
        "    Disable avatar cloning on enter world: True\n" +
        "    Avatar interaction level: All\n" +
        "    Avatar self-interaction: True\n" +
        "Accessibility Settings:\n" +
        "    Gesture bar enabled: False\n" +
        "    Earmuff mode: False\n" +
        "    Earmuff mode (avatars): True\n" +
        "    Earmuff mode, Always show visual aid: True\n" +
        "    Earmuff mode radius: 0.3\n" +
        "    Earmuff mode falloff: 0\n" +
        "    Earmuff mode reduced volume: 0.15\n" +
        "    Chat bubble visibility: Everyone\n" +
        "    Chat bubble scale: 1.5\n" +
        "    Chat bubble opacity: 0.75\n" +
        "    Chat bubble timeout: 30\n" +
        "Comfort & Safety Settings:\n" +
        "    Personal space: False\n" +
        "    Voice prioritization: True\n" +
        "    Third person rotation: False\n" +
        "    Comfort turning: False\n" +
        "    Head look walking: True\n" +
        "    Locomotion method: Gamelike\n" +
        "    Vive advanced: False\n" +
        "    Streamer mode enabled: False\n" +
        "    Limit particle systems: False\n" +
        "    Safety level: None\n" +
        "    Allow untrusted URLs: True\n" +
        "    Ask to portal: True\n" +
        "    Mirror resolution: Full\n" +
        "    Portal Mode: PlaceManually\n"
    ];

    [Fact]
    public void ParseSingleLine()
    {
        foreach (var (expected, logString) in SingleLineLogEntities.Zip(SingleLineLogStrings))
        {
            var logEntity = VRChatLogEntity.Parse(logString);
            Assert.Equal(expected.Timestamp, logEntity.Timestamp);
            Assert.Equal(expected.LogLevel, logEntity.LogLevel);
            Assert.Equal(expected.Message, logEntity.Message);
        }
    }

    [Fact]
    public void ParseMultiLine()
    {
        foreach (var (expected, logString) in MultiLineLogEntities.Zip(MultiLineLogStrings))
        {
            var logEntity = VRChatLogEntity.Parse(logString);
            Assert.Equal(expected.Timestamp, logEntity.Timestamp);
            Assert.Equal(expected.LogLevel, logEntity.LogLevel);
            Assert.Equal(expected.Message, logEntity.Message);
        }
    }

    [Fact]
    public void ParseEmptyString()
    {
        Assert.Throws<FormatException>(() => VRChatLogEntity.Parse(string.Empty));
    }

    [Fact]
    public void ParseInvalidFormat()
    {
        Assert.Throws<FormatException>(() => VRChatLogEntity.Parse(InvalidLogString));
    }
}
