using Messages.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Messages.UnitTests
{
    public static class RegisterServices
    {
        public static void Register(IServiceCollection services)
        {
            var config = RegisterConfig(services);
            RegisterLogging(services, config);
            services.AddMessagesDatabaseServices(config);
        }

        public static IConfiguration RegisterConfig(IServiceCollection services)
        {
            var config = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();

            services.AddSingleton(config);
            return config;
        }

        public static void RegisterLogging(IServiceCollection services, IConfiguration config)
        {
            services.AddLogging(logging =>
            {
                logging.AddConfiguration(config.GetSection("Logging"));
                logging.AddConsole();
            }).Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Information);
        }
    }
}
