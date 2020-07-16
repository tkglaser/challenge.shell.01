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
        static void Main(string[] args)
        {
            // TODO: Write custom formatter for "-10p"?
            CultureInfo.CurrentCulture = new CultureInfo("en-GB", false);

            // Load configuration
            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                .Build();

            // Setup DI
            RegisterServices();

            // Run app
            IServiceScope scope = _serviceProvider.CreateScope();
            var result = scope.ServiceProvider.GetRequiredService<PriceCalculator>().Calculate(args);

            Console.WriteLine(result.ToString());

            // Tear down DI
            DisposeServices();
        }

        private static void RegisterServices()
        {
            IServiceCollection services = new ServiceCollection();
            services.Configure<PriceCalculatorConfig>(_configuration.GetSection(PriceCalculatorConfig.Section));
            services.AddSingleton<PriceCalculator>();
            _serviceProvider = services.BuildServiceProvider(true);
        }
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
