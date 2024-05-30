﻿using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;

namespace Bluehill.Vhd;

public static partial class VhdFunctions {
    public static SafeFileHandle CreateVhd(string path, VhdSize size, bool isFixed = false) {
        ArgumentException.ThrowIfNullOrWhiteSpace(path);

        return createVhdCore(path, null, null, size, isFixed);
    }

    public static SafeFileHandle CreateChildVhd(string child, string parent) {
        ArgumentException.ThrowIfNullOrWhiteSpace(child);
        ArgumentException.ThrowIfNullOrWhiteSpace(parent);

        return createVhdCore(child, parent, null, default, false);
    }

    public static SafeFileHandle CloneVhd(string destination, string source, VhdSize size = default, bool isFixed = false) {
        ArgumentException.ThrowIfNullOrWhiteSpace(destination);
        ArgumentException.ThrowIfNullOrWhiteSpace(source);

        return createVhdCore(destination, null, source, size, isFixed);
    }

    private static SafeFileHandle createVhdCore(string path, string? parent, string? source, VhdSize size, bool isFixed) {
        if (parent is not null && source is not null) {
            throw new ArgumentException("Both");
        }

        if (parent is not null &&
            !Path.GetExtension(path).TrimStart('.').Equals(Path.GetExtension(parent).TrimStart('.'), StringComparison.OrdinalIgnoreCase)) {
            throw new ArgumentException("The parent and child file extensions must be the same.");
        }

        var vst = getVst(path);

        CreateVirtualDiskParameters cvdp = new() {
            UniqueId = Guid.Empty,
            MaximumSize = (ulong)(long)size,
            BlockSizeInBytes = 0,
            SectorSizeInBytes = 512,
            ParentPath = parent,
            SourcePath = source
        };

        var result = NativeMethods.CreateVirtualDisk(
            in vst,
            path,
            VirtualDiskAccessMask.Create,
            nint.Zero,
            parent is not null && isFixed ? CreateVirtualDiskOptions.FullPhysicalAllocation : CreateVirtualDiskOptions.None,
            0,
            in cvdp,
            nint.Zero,
            out var handle);

        return result == 0 ? new(handle, true) : throw new OperationFailedException(Marshal.GetPInvokeErrorMessage((int)result));
    }
}