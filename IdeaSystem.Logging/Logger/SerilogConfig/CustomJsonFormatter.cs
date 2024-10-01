using Serilog.Events;
using Serilog.Formatting;
using Serilog.Formatting.Json;

namespace IdeaSystem.Logger.SerilogConfig;
public class CustomJsonFormatter(JsonValueFormatter valueFormatter = null) : ITextFormatter
{
    private readonly JsonValueFormatter _valueFormatter = valueFormatter ?? new JsonValueFormatter("$type");

    private static readonly List<string> MainKeys = ["Logger", "MachineName", "ThreadId", "CorrelationId", "ClientIp", "ApplicationName", "ApplicationType"];

    public void Format(LogEvent logEvent, TextWriter output)
    {
        CustomFormatEvent(logEvent, output, this._valueFormatter);
        output.WriteLine();
    }

    public static void CustomFormatEvent(LogEvent logEvent, TextWriter output, JsonValueFormatter valueFormatter)
    {
        ArgumentNullException.ThrowIfNull(logEvent);
        ArgumentNullException.ThrowIfNull(output);
        ArgumentNullException.ThrowIfNull(valueFormatter);

        output.Write("{\"time\":\"");
        output.Write(logEvent.Timestamp.DateTime.ToString("yyyy-MM-dd HH:mm:ss.ffff zzz"));

        output.Write('"');
        output.Write(",\"level\":\"");
        output.Write(logEvent.Level);
        output.Write('"');

        foreach (var property in logEvent.Properties.Where(le => MainKeys.Contains(le.Key)))
        {
            string str = property.Key;
            if (str.Length > 0 && str[0] == '@')
            {
                str = "@" + str;
            }

            WriteProperty(str, logEvent, output, valueFormatter);
        }

        if (logEvent.Exception != null)
        {
            output.Write(",\"@x\":");
            JsonValueFormatter.WriteQuotedJsonString(logEvent.Exception.ToString(), output);
        }

        output.Write(",\"message\":");
        output.Write('{');

        var isFirst = true;
        foreach (KeyValuePair<string, LogEventPropertyValue> property in logEvent.Properties.Where(le => !MainKeys.Contains(le.Key)))
        {
            string str = property.Key;
            if (str.Length > 0 && str[0] == '@')
            {
                str = "@" + str;
            }

            if (!isFirst)
            {
                output.Write(',');
            }

            isFirst = false;

            JsonValueFormatter.WriteQuotedJsonString(str, output);
            output.Write(':');
            valueFormatter.Format(property.Value, output);
        }

        output.Write('}');
        output.Write('}');
    }

    private static void WriteProperty(string key, LogEvent logEvent, TextWriter output, JsonValueFormatter valueFormatter)
    {
        if (logEvent.Properties.ContainsKey(key))
        {
            output.Write($",\"{key[0].ToString().ToLower()}{key[1..]}\":");
            valueFormatter.Format(logEvent.Properties[key], output);
        }
    }
}
