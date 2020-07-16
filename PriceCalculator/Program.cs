using System;
using Microsoft.Extensions.Configuration;

namespace PriceCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfiguration Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                .Build();
            var products = Configuration.GetSection("products").Value;
        }
    }
}
