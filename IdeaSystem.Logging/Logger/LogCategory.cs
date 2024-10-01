namespace IdeaSystem.Logging.Serilig.Logger;
public enum LogCategory
{
    StartRequest,
    EndRequest,
    Message,
    Exception
    //.....
}

public enum LogExtraKeys
{
    CorrelationId,
    Headers,
    Query,
    ActionName,
    ServiceName,
    StatusCode,
    ResponseTime,
    ResponseBody,
    RequestBody,
    ExceptionTitle,
    RealIp,
    Extra,
    //...
}
