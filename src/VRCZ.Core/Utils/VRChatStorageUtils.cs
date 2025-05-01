﻿using System.Runtime.InteropServices;

namespace VRCZ.Core.Utils;

public static partial class VRChatStorageUtils
{
    // https://learn.microsoft.com/windows/win32/shell/knownfolderid
    // FOLDERID_LocalAppDataLow
    private static readonly Guid LocalLowFolderGuid = new("A520A1A4-1780-4FF6-BD18-167343C5AF16");

    public static string GetVRChatStorageRootPath()
    {
        // TODO: Add support for other platforms
        if (!OperatingSystem.IsWindows())
            throw new PlatformNotSupportedException(
                "VRChatStorageUtils.GetVRChatStorageRootPath() is only supported on Windows.");

        var hr = SHGetKnownFolderPath(LocalLowFolderGuid, 0, IntPtr.Zero, out var path);

        if (hr != 0)
            throw new COMException("Failed to get known folder path.", hr);

        var vrchatPath = Path.Join(path, "VRChat", "VRChat");

        return vrchatPath;
    }

    // TODO: Move this to a separate file

    // https://learn.microsoft.com/windows/win32/api/shlobj_core/nf-shlobj_core-shgetknownfolderpath
    [LibraryImport("shell32.dll", SetLastError = false, StringMarshalling = StringMarshalling.Utf16)]
    private static partial int SHGetKnownFolderPath(
        in Guid rfid,
        uint dwFlags,
        IntPtr hToken,
        out string ppszPath);
}
