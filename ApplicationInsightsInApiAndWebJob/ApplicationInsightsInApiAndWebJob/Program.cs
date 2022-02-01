using ApplicationInsightsInApiAndWebJob.Extensions;
using ApplicationInsightsInApiAndWebJob.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging.ApplicationInsights;

namespace ApplicationInsightsInApiAndWebJob;

public static class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }
    
    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebJobsWithoutAppConfiguration(webJobsBuilder =>
            {
                webJobsBuilder
                    .AddTimers()
                    .AddAzureStorageCoreServices()
                    .AddExecutionContextBinding();
            })
            .ConfigureLogging((context, builder) =>
            {
                // Hint: If you comment out the following code, Requests and Dependencies of API will start appearing in AI.

                var instrumentationKey = context
                    .Configuration[nameof(ApplicationInsights) + ":" + nameof(ApplicationInsights.InstrumentationKey)];
                if (!string.IsNullOrEmpty(instrumentationKey))
                {
                    builder.AddApplicationInsightsWebJobs(options => options.InstrumentationKey = instrumentationKey);

                    var logLevelString = context
                        .Configuration[nameof(ApplicationInsights) + ":" + nameof(ApplicationInsights.MinimumLogLevel)];
                    if (!string.IsNullOrWhiteSpace(logLevelString) && Enum.TryParse(logLevelString, out LogLevel logLevel))
                    {
                        builder.AddFilter(null, logLevel);
                        builder.AddFilter<ApplicationInsightsLoggerProvider>(null, logLevel);
                        builder.AddFilter<Microsoft.Azure.WebJobs.Logging.ApplicationInsights.ApplicationInsightsLoggerProvider>(null, logLevel);
                    }
                }
            })
            .ConfigureWebHostDefaults(builder =>
            {
                builder.UseStartup<Startup>();
            });
}