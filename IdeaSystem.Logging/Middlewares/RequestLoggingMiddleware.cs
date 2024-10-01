using System.Text;

namespace IdeaSystem.Logging.Serilig.Logger.Middlewares;
public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;
    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        var extraFields = new Dictionary<LogExtraKeys, object> { };
        try
        {
            extraFields[LogExtraKeys.RequestBody] = await GetRequestBody(httpContext);
            _logger.LogDebug(LogCategory.StartRequest, extraFields);

            var originalBodyStream = httpContext.Response.Body;
            using (var responseBody = new MemoryStream())
            {
                httpContext.Response.Body = responseBody;

                await _next(httpContext);

                extraFields[LogExtraKeys.ResponseBody] = await GetResponseBody(httpContext);
                await responseBody.CopyToAsync(originalBodyStream);
            }

            _logger.LogDebug(LogCategory.EndRequest, extraFields);
        }
        catch (Exception ex) 
        {
            extraFields[LogExtraKeys.ExceptionTitle] = ex;
            _logger.LogDebug(LogCategory.Exception, extraFields);
        }
    }

    private static Dictionary<string, string> GetHeaders(HttpContext context) => context.Request.Headers.ToDictionary(h => h.Key, h => h.Value.ToString());

    private static Dictionary<string, string> GetQueryParams(HttpContext context) => context.Request.Query.ToDictionary(q => q.Key, q => q.Value.ToString());

    private static async Task<string> GetRequestBody(HttpContext context)
    {
        context.Request.EnableBuffering();
        using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8, true, 1024, true))
        {
            var body = await reader.ReadToEndAsync();
            context.Request.Body.Seek(0, SeekOrigin.Begin);
            return body;
        }
    }

    private static async Task<string> GetResponseBody(HttpContext context)
    {
        context.Response.Body.Seek(0, SeekOrigin.Begin);
        var text = await new StreamReader(context.Response.Body).ReadToEndAsync();
        context.Response.Body.Seek(0, SeekOrigin.Begin);
        return text;
    }
}

public static class RequestLoggingMiddlewareExtensions
{
    public static IApplicationBuilder UseRequestLoggingMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequestLoggingMiddleware>();
    }
}
