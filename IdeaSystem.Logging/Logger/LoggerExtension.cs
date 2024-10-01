using IdeaSystem.Logging.Serilig.Extensions;
using Serilog.Context;

namespace IdeaSystem.Logging.Serilig.Logger;
public partial class LoggerExtensions
{
    public static void LogTrace(this ILogger logger,
        LogCategory category,
        Dictionary<LogExtraKeys, object> extraFields)
    {
        AddCorrelationId(extraFields);

        logger.LogTrace(category: category.ToString(),
            extraFields: extraFields.ToDictionary(kv => kv.Key.ToString(), kv => kv.Value));
    }

    public static void LogDebug(this ILogger logger,
        LogCategory category,
        Dictionary<LogExtraKeys, object> extraFields)
    {
        AddCorrelationId(extraFields);

        logger.LogDebug(
            category: category.ToString(),
            extraFields: extraFields.ToDictionary(kv => kv.Key.ToString(), kv => kv.Value));
    }

    public static void LogInformation(this ILogger logger,
        LogCategory category,
        Dictionary<LogExtraKeys, object> extraFields)
    {
        AddCorrelationId(extraFields);

        logger.LogInformation(category: category.ToString(),
            extraFields: extraFields.ToDictionary(kv => kv.Key.ToString(), kv => kv.Value));
    }

    public static void LogInformation(this ILogger logger,
        string message)
    {
        logger.LogInformation(category: LogCategory.Message,
            extraFields: logger.ExtraItemsToDictionary((LogExtraKeys.Extra, message)));
    }

    public static void LogWarning(this ILogger logger,
        LogCategory category,
        Dictionary<LogExtraKeys, object> extraFields)
    {
        AddCorrelationId(extraFields);

        logger.LogWarning(category: category.ToString(),
            extraFields: extraFields.ToDictionary(kv => kv.Key.ToString(), kv => kv.Value));
    }

    public static void LogError(this ILogger logger,
        Exception ex,
        LogCategory category,
        Dictionary<LogExtraKeys, object> extraFields)
    {
        AddCorrelationId(extraFields);

        logger.LogError(ex: ex,
            category: category.ToString(),
            extraFields: extraFields.ToDictionary(kv => kv.Key.ToString(), kv => kv.Value));
    }

    public static void LogError(this ILogger logger,
        LogCategory category,
        Dictionary<LogExtraKeys, object> extraFields)
    {
        AddCorrelationId(extraFields);

        logger.LogError(category: category.ToString(),
            extraFields: extraFields.ToDictionary(kv => kv.Key.ToString(), kv => kv.Value));
    }

    public static void LogCritical(this ILogger logger,
        Exception ex,
        LogCategory category,
        Dictionary<LogExtraKeys, object> extraFields)
    {
        AddCorrelationId(extraFields);

        logger.LogCritical(ex: ex,
            category: category.ToString(),
            extraFields: extraFields.ToDictionary(kv => kv.Key.ToString(), kv => kv.Value));
    }

    public static void LogCritical(this ILogger logger,
        LogCategory category,
        Dictionary<LogExtraKeys, object> extraFields)
    {
        AddCorrelationId(extraFields);

        logger.LogCritical(category: category.ToString(),
            extraFields: extraFields.ToDictionary(kv => kv.Key.ToString(), kv => kv.Value));
    }

    private static void AddCorrelationId(Dictionary<LogExtraKeys, object> extraFields)
    {
        if (!extraFields.ContainsKey(LogExtraKeys.CorrelationId))
        {
            extraFields[LogExtraKeys.CorrelationId] = LogContext.PushProperty("CorrelationId", Guid.NewGuid().ToString());
        }
    }
}
