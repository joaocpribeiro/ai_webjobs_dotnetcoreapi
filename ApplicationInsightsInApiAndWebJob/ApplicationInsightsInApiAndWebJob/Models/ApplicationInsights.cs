namespace ApplicationInsightsInApiAndWebJob.Models
{
    public class ApplicationInsights
    {
        public string InstrumentationKey { get; set; }

        public LogLevel? MinimumLogLevel { get; set; }
    }
}
