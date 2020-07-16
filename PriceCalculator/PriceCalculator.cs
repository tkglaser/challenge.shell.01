using System;
using System.Linq;
using Microsoft.Extensions.Options;
using PriceCalculator.Models;

namespace PriceCalculator
{
    public class PriceCalculator
    {
        private readonly PriceCalculatorConfig _config;

        public PriceCalculator(IOptions<PriceCalculatorConfig> config)
        {
            _config = config.Value;
        }
        public void Run(string[] items)
        {
            var basket = new Basket(items);
            var copy = new Basket(basket);
            Console.WriteLine(string.Join(",", items));
            throw new NotImplementedException();
        }
    }
}