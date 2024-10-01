using IdeaSystem.Logging.Serilig.Logger;

namespace IdeaSystem.Logging.Serilig.Extensions;

public static partial class Extensions
{
    public static Dictionary<LogExtraKeys, object> ExtraItemsToDictionary(this ILogger _, params (LogExtraKeys, object)[] extraItems)
         => extraItems.ToDictionary(item => item.Item1, item => item.Item2);
}
