using Microsoft.Azure.WebJobs;

namespace ApplicationInsightsInApiAndWebJob.WebJobs
{
    public class LogHelloWorldWebJob
    {
        private readonly ILogger<LogHelloWorldWebJob> _logger;

        public LogHelloWorldWebJob(ILogger<LogHelloWorldWebJob> logger)
        {
            _logger = logger;
        }

        [FunctionName("LogHelloWorldWebJob")]
        [Singleton(Mode = SingletonMode.Listener)]
        public async Task Job(
            // Run every minute
            [TimerTrigger("0 * * * * *")] TimerInfo timerInfo
        )
        {
            _logger.LogInformation("Hello world from web job!");
        }
    }
}
