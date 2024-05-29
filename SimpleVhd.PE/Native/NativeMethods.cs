﻿using System.Runtime.InteropServices;

namespace SimpleVhd.PE.Native;

internal static class NativeMethods {
    [DllImport("VirtDisk.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
    public static extern uint CreateVirtualDisk(
        in VirtualStorageType virtualStorageType,
        [MarshalAs(UnmanagedType.LPWStr)] string path,
        VirtualDiskAccessMask virtualDiskAccessMask,
        nint securityDescriptor,
        CreateVirtualDiskOptions flags,
        uint providerSpecificFlags,
        in CreateVirtualDiskParameters parameters,
        nint overlapped,
        out nint handle);
}
