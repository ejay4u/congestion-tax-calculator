using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Api
{
    public class Program
    {
        /// <summary>
        ///     This is the entry point of the service host process.
        /// </summary>
        /// <param name="args">Arguments.</param>
        public static void Main(string[] args)
        {
            var configuration = BuildConfiguration();

            CreateHostBuilder(args, configuration).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args, IConfiguration configuration)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices(services => services.AddSingleton(configuration))
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>().UseConfiguration(configuration);
                });
        }

        private static IConfiguration BuildConfiguration()
        {
            var configBuilder = new ConfigurationBuilder().AddEnvironmentVariables();

            var configuration = configBuilder.Build();

            var environment = configuration["ASPNETCORE_ENVIRONMENT"];

            _ = configBuilder.AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{environment}.json", true, true);

            return configBuilder.Build();
        }
    }
}