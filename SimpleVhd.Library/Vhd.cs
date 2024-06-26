﻿namespace SimpleVhd;

public sealed record class Vhd : IEquatable<Vhd?> {
    public required string Name { get; set; }
    public required string Directory { get; init; }
    public required string FileName { get; init; }
    public required Style Style { get; set; }
    public required VhdType Type { get; set; }
    public required VhdFormat Format { get; set; }
    public required Guid ParentGuid { get; init; }
    public required Guid Child1Guid { get; init; }
    public required Guid Child2Guid { get; init; }

    public bool Equals(Vhd? other) => other is not null && Directory == other.Directory && FileName == other.FileName;
    public override int GetHashCode() => HashCode.Combine(Directory, FileName);
}
