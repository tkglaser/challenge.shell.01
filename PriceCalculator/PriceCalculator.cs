using System;
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
        public void Run()
        {
            throw new NotImplementedException();
        }
    }
}