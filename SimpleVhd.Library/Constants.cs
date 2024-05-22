﻿namespace SimpleVhd;

public static class Constants {
    public const ulong BuildNumber = 0;
    public const string SVDirName = "SimpleVhd";
    public const string BackupDirName = "SimpleVhd-Backup";
    public const string SettingsFileName = "Settings.json";
    public const string SettingsSchemaUrl = "https://raw.githubusercontent.com/na1307/SimpleVhd/main/SimpleVhd/Settings.schema.json";

    public static string SVPath { get; internal set; } = string.Empty;
}