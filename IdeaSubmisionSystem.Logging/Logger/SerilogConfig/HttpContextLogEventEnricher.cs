using Serilog.Core;
using Serilog.Events;

namespace IdeaSubmisionSystem.Logger.SerilogConfig;
public class HttpContextLogEventEnricher(IHttpContextAccessor httpContextAccessor) : ILogEventEnricher
{
    public HttpContextLogEventEnricher() : this(new HttpContextAccessor())
    {
    }

    public virtual void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        if (httpContextAccessor?.HttpContext?.Request.RouteValues.Count != 0)
        {
            string? controllerName = httpContextAccessor?.HttpContext?.Request.RouteValues["controller"]?.ToString();
            string? actionName = httpContextAccessor?.HttpContext?.Request.RouteValues["action"]?.ToString();
            string? realIp = httpContextAccessor?.HttpContext?.Request?.Headers["x-forwarded-for"].ToString();
            string? correlationId = httpContextAccessor?.HttpContext?.Request?.Headers["X-Correlation-ID"].ToString();

            if (!string.IsNullOrEmpty(correlationId))
            {
                var correlation = propertyFactory.CreateProperty("CorrelationId", correlationId);
                logEvent.AddPropertyIfAbsent(correlation);
            }
            if (!string.IsNullOrEmpty(controllerName))
            {
                var controllerNameProperty = propertyFactory.CreateProperty("ServiceName", controllerName);
                logEvent.AddPropertyIfAbsent(controllerNameProperty);
            }

            if (!string.IsNullOrEmpty(realIp))
            {
                var realIpProperty = propertyFactory.CreateProperty("RealIp", realIp);
                logEvent.AddPropertyIfAbsent(realIpProperty);
            }

            if (!string.IsNullOrEmpty(actionName))
            {
                var actionNameProperty = propertyFactory.CreateProperty("ActionName", actionName);
                logEvent.AddPropertyIfAbsent(actionNameProperty);
            }
        }
    }
}
