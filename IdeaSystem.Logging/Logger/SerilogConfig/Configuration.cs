namespace IdeaSystem.Logger.SerilogConfig;
public record Configuration
{
    public string LogFilePath { get; set; } = string.Empty;
    public string? ApplicationName { get; set; } = string.Empty;
    public RollingDuration RollingInterval { get; set; } = RollingDuration.Day;
    public long? FileSizeLimitBytes { get; set; } = 31457280;
}

public enum RollingDuration : byte
{
    //
    // Summary:
    //     The log file will never roll; no time period information will be appended to
    //     the log file name.
    Infinite,
    //
    // Summary:
    //     Roll every year. Filenames will have a four-digit year appended in the pattern
    //
    //
    //     yyyy
    //
    //     .
    Year,
    //
    // Summary:
    //     Roll every calendar month. Filenames will have
    //
    //     yyyyMM
    //
    //     appended.
    Month,
    //
    // Summary:
    //     Roll every day. Filenames will have
    //
    //     yyyyMMdd
    //
    //     appended.
    Day,
    //
    // Summary:
    //     Roll every hour. Filenames will have
    //
    //     yyyyMMddHH
    //
    //     appended.
    Hour,
    //
    // Summary:
    //     Roll every minute. Filenames will have
    //
    //     yyyyMMddHHmm
    //
    //     appended.
    Minute
}
