using Api.ControllerHandlers;
using Api.Providers;
using congestion.calculator.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.EventLog;

namespace Api
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Startup" /> class.
        /// </summary>
        /// <param name="configuration">The standard <see cref="IConfiguration" /> used by .Net applications.</param>
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <summary>
        ///     This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">Services collection that is used for DI.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            _ = services.AddControllers();

            _ = services.AddLogging(
                loggingBuilder => loggingBuilder.AddFilter<EventLogLoggerProvider>(level => level >= LogLevel.Warning)
                    .AddConfiguration(configuration.GetSection("Logging"))
                    .AddConsole()
                    .AddDebug());


            _ = services.AddSingleton<ICongestionTaxesControllerHandler, CongestionTaxesControllerHandler>();
            _ = services.AddSingleton<IVehicleProvider, VehicleProvider>();
            _ = services.AddCongestionTaxCalculator();
        }

        /// <summary>
        ///     This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseRouting();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}