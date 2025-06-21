using System.Text.RegularExpressions;

namespace VRCZ.Core.Models.VRChat.Logging.LogEvent;

public sealed record JoiningInstanceEvent(VRChatLogEntity LogEntity, string WorldId, string InstanceId)
    : VRChatGameLogEventBase(LogEntity);

// [Behaviour] Joining wrld_0e548e5f-1e5a-46df-a0d4-e852d5fcf11c:94748~group(grp_56e7ec11-1178-48fc-aa16-3d8341632854)~groupAccessType(plus)~region(jp)

public sealed partial class JoiningInstanceEventParser : IVRChatGameLogEventParser<JoiningInstanceEvent>
{
    // [Behaviour] Joining wrld_a2ed57ba-2749-4800-90f6-0e32e4527a55:17757~private(usr_c6806bef-b885-427d-acaf-e24a66acb829)~region(jp)
    // NOT: [Behaviour] Joining or Creating Room: World Localization Test

    [GeneratedRegex(@"^\[Behaviour\] Joining (?!or )(?<WorldId>.+):(?<InstanceId>.+)")]
    private static partial Regex JoiningInstanceRegex();

    public JoiningInstanceEvent? Parse(VRChatLogEntity logEntity)
    {
        var regex = JoiningInstanceRegex();

        var match = regex.Match(logEntity.Message);

        if (!match.Success)
            return null;

        var worldId = match.Groups["WorldId"].Value;
        var instanceId = match.Groups["InstanceId"].Value;

        return new JoiningInstanceEvent(logEntity, worldId, instanceId);
    }

    VRChatGameLogEventBase? IVRChatGameLogEventParser.Parse(VRChatLogEntity entity)
    {
        return Parse(entity);
    }
}

public sealed record LeftInstanceEvent(VRChatLogEntity LogEntity) : VRChatGameLogEventBase(LogEntity);

public sealed partial class LeftInstanceEventParser : IVRChatGameLogEventParser<LeftInstanceEvent>
{
    [GeneratedRegex(@"^\[Behaviour\] OnLeftRoom$")]
    private static partial Regex LeftInstanceRegex();

    public LeftInstanceEvent? Parse(VRChatLogEntity logEntity)
    {
        var regex = LeftInstanceRegex();

        if (!regex.IsMatch(logEntity.Message))
            return null;

        return new LeftInstanceEvent(logEntity);
    }

    VRChatGameLogEventBase? IVRChatGameLogEventParser.Parse(VRChatLogEntity entity)
    {
        return Parse(entity);
    }
}
