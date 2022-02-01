using Microsoft.Azure.WebJobs;

namespace ApplicationInsightsInApiAndWebJob.Extensions
{
    public static class HostingExtensions
    {
        /// <summary>
        /// Configures WebJobs without overwriting application configuration 
        /// See: https://github.com/Azure/azure-webjobs-sdk/issues/1931
        /// </summary>
        /// <param name="hostBuilder"></param>
        /// <param name="configure"></param>
        /// <param name="configureOptions"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IHostBuilder ConfigureWebJobsWithoutAppConfiguration(
            this IHostBuilder hostBuilder,
            Action<IWebJobsBuilder> configure,
            Action<JobHostOptions> configureOptions = null)
        {
            if (hostBuilder is null)
            {
                throw new ArgumentNullException(nameof(hostBuilder));
            }

            IHostBuilder hostBuilderWithoutAppConfiguration = new HostBuilderWithoutAppConfiguration(hostBuilder);

            hostBuilderWithoutAppConfiguration.ConfigureWebJobs(
                configure ?? (webJobsBuilder => { }),
                configureOptions ?? (configureOpts => { }));

            return hostBuilder;
        }

        private class HostBuilderWithoutAppConfiguration : IHostBuilder
        {
            private readonly IHostBuilder _hostBuilder;

            public HostBuilderWithoutAppConfiguration(IHostBuilder hostBuilder)
            {
                _hostBuilder = hostBuilder ?? throw new ArgumentNullException(nameof(hostBuilder));
            }

            public IDictionary<object, object> Properties => _hostBuilder.Properties;

            public IHost Build() => _hostBuilder.Build();

            public IHostBuilder ConfigureAppConfiguration(
                Action<HostBuilderContext, IConfigurationBuilder> configureDelegate)
            {
                return this;
            }

            public IHostBuilder ConfigureContainer<TContainerBuilder>(
                Action<HostBuilderContext, TContainerBuilder> configureDelegate)
            {
                _hostBuilder.ConfigureContainer(configureDelegate);
                return this;
            }

            public IHostBuilder ConfigureHostConfiguration(Action<IConfigurationBuilder> configureDelegate)
            {
                _hostBuilder.ConfigureHostConfiguration(configureDelegate);
                return this;
            }

            public IHostBuilder ConfigureServices(Action<HostBuilderContext, IServiceCollection> configureDelegate)
            {
                _hostBuilder.ConfigureServices(configureDelegate);
                return this;
            }

            public IHostBuilder UseServiceProviderFactory<TContainerBuilder>(
                IServiceProviderFactory<TContainerBuilder> factory)
            {
                _hostBuilder.UseServiceProviderFactory(factory);
                return this;
            }

            public IHostBuilder UseServiceProviderFactory<TContainerBuilder>(
                Func<HostBuilderContext, IServiceProviderFactory<TContainerBuilder>> factory)
            {
                _hostBuilder.UseServiceProviderFactory(factory);
                return this;
            }
        }
    }
}
