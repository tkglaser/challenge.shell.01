using System;
using System.Globalization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PriceCalculator.Models;

namespace PriceCalculator
{
    class Program
    {
        private static ServiceProvider _serviceProvider;
        private static IConfiguration _configuration;

        /// <summary>
        /// Main entry point
        /// </summary>
        /// <param name="args">Command line args expected as `Milk Bread Apple` etc.</param>
        static void Main(string[] args)
        {
            // Make sure, the currency output is formatted like £1.23
            CultureInfo.CurrentCulture = new CultureInfo("en-GB", false);

            // Load configuration
            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                .Build();

            // I normally would think twice about setting up DI for such a
            // small project but an emphasis on extensibility was given in the spec.

            // Setup DI
            RegisterServices();

            // Run app
            IServiceScope scope = _serviceProvider.CreateScope();
            var result = scope.ServiceProvider.GetRequiredService<PriceCalculator>().Calculate(args);

            // Write result out to the console
            Console.WriteLine(result.ToString());

            // Tear down DI
            DisposeServices();
        }

        /// <summary>
        /// Registers all the DI services
        /// </summary>
        private static void RegisterServices()
        {
            IServiceCollection services = new ServiceCollection();
            services.Configure<PriceCalculatorConfig>(_configuration.GetSection(PriceCalculatorConfig.Section));
            services.AddSingleton<PriceCalculator>();
            _serviceProvider = services.BuildServiceProvider(true);
        }
        
        /// <summary>
        /// Disposes all DI services
        /// </summary>
        private static void DisposeServices()
        {
            if (_serviceProvider == null)
            {
                return;
            }
            if (_serviceProvider is IDisposable)
            {
                ((IDisposable)_serviceProvider).Dispose();
            }
        }
    }


}
