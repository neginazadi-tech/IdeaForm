using IdeaSubmisionSystem.Logger.SerilogConfig;
using Serilog;

namespace IdeaSubmisionSystem.Logging.Serilig.Logger;

public static class Registration
{
    public static WebApplicationBuilder UseSerilog(this WebApplicationBuilder webAppBuilder, Action<Configuration> configurator)
    {
        var config = new Configuration();
        configurator(config);

        ArgumentException.ThrowIfNullOrWhiteSpace(config.LogFilePath);
        ArgumentException.ThrowIfNullOrWhiteSpace(config.ApplicationName);

        var configuration = new ConfigurationBuilder()
            .AddJsonFile(path: "appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile(path: $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true)
            .Build();

        Log.Logger = new LoggerConfiguration()
          .ReadFrom.Configuration(configuration)
          .Enrich.FromLogContext()
          .Enrich.WithMachineName()
          .Enrich.WithCorrelationId()
          .Enrich.WithCorrelationIdHeader()
          .Enrich.WithThreadId()
          .Enrich.WithClientIp()
          .Enrich.With<HttpContextLogEventEnricher>()
          .WriteTo.Async(cfg =>
          {
              if (webAppBuilder.Environment.IsDevelopment())
              {
                  cfg.Console();
              }

              cfg.File(formatter: new CustomJsonFormatter(),
                    path: config.LogFilePath,
                    buffered: false,
                    rollingInterval: (RollingInterval)config.RollingInterval,
                    retainedFileCountLimit: 30,
                    rollOnFileSizeLimit: true,
                    fileSizeLimitBytes: config.FileSizeLimitBytes);
          }
          ).CreateLogger();

        webAppBuilder.Host.UseSerilog();

        return webAppBuilder;
    }
}
