using System.Text;

namespace IdeaSubmisionSystem.Logging.Serilig.Logger;

public static partial class LoggerExtensions
{
    private const string TemplateWithoutException = "Category: {Category}";
    private const string TemplateWithException = "Category: {Category} ExceptionMessage: {ExceptionMessage}";

    private static void LogTrace(this ILogger logger,
        string category,
        Dictionary<string, object> extraFields)
    {
        logger.LogTrace(TemplateWithoutException + GetExtraTemplate(extraFields),
            ConcatExtraFields([category], extraFields));
    }

    private static void LogDebug(this ILogger logger,
        string category,
        Dictionary<string, object> extraFields)
    {
        logger.LogDebug(TemplateWithoutException + GetExtraTemplate(extraFields),
            ConcatExtraFields([category], extraFields));
    }

    private static void LogInformation(this ILogger logger,
        string category,
        Dictionary<string, object> extraFields)
    {
        logger.LogInformation(TemplateWithoutException + GetExtraTemplate(extraFields),
            ConcatExtraFields([category], extraFields));
    }

    private static void LogWarning(this ILogger logger,
        string category,
        Dictionary<string, object> extraFields)
    {
        logger.LogWarning(TemplateWithoutException + GetExtraTemplate(extraFields),
            ConcatExtraFields([category], extraFields));
    }

    private static void LogError(this ILogger logger,
        Exception ex,
        string category,
        Dictionary<string, object> extraFields)
    {
        logger.LogError(ex,
            TemplateWithException + GetExtraTemplate(extraFields),
            ConcatExtraFields([category, ex.Message], extraFields));
    }

    private static void LogError(this ILogger logger,
        string category,
        Dictionary<string, object> extraFields = null)
    {
        logger.LogError(
            TemplateWithoutException + GetExtraTemplate(extraFields),
            ConcatExtraFields([category], extraFields));
    }

    private static void LogCritical(this ILogger logger,
        Exception ex,
        string category,
        Dictionary<string, object> extraFields)
    {
        logger.LogCritical(ex,
            TemplateWithException + GetExtraTemplate(extraFields),
            ConcatExtraFields([category, ex.Message], extraFields));
    }

    private static void LogCritical(this ILogger logger,
        string category,
        Dictionary<string, object> extraFields = null)
    {
        logger.LogCritical(
            TemplateWithoutException + GetExtraTemplate(extraFields),
            ConcatExtraFields([category], extraFields));
    }

    private static string GetExtraTemplate(Dictionary<string, object> extraFields)
    {
        return extraFields?.Keys.Aggregate(new StringBuilder(), (sb, k) => sb.Append($" {k}: {{{k}}}")).ToString();
    }

    private static object[] GetExtraParams(Dictionary<string, object> extraFields)
    {
        return extraFields?.Values.ToArray();
    }

    private static object[] ConcatExtraFields(object[] baseFields, Dictionary<string, object> extraFields)
    {
        return extraFields is null ? baseFields : ([.. baseFields, .. GetExtraParams(extraFields)]);
    }
}
