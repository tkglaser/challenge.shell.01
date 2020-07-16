using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using PriceCalculator.Models;

namespace PriceCalculator
{
    /// <summary>
    /// The pricing calculator
    /// </summary>
    public class PriceCalculator
    {
        private readonly PriceCalculatorConfig _config;
        private readonly Dictionary<string, Product> _products;

        /// <summary>
        /// The constructor that takes the config from the DI container
        /// </summary>
        /// <param name="config">The config</param>
        public PriceCalculator(IOptions<PriceCalculatorConfig> config)
        {
            _config = config.Value;
            _products = new Dictionary<string, Product>();
            foreach (var product in _config.Products)
            {
                // copy into dictionary for quicker lookup
                _products.Add(product.Name.ToLowerInvariant(), product);
            }
        }

        /// <summary>
        /// Calculate the basket value from the given input
        /// </summary>
        /// <param name="items">The items in string array form</param>
        /// <returns>The result of the calculation</returns>
        public CalculationResult Calculate(string[] items)
        {
            var basket = new Basket(items);
            var result = new CalculationResult();
            foreach (var kvp in basket)
            {
                result.Add(GetTotal(kvp.Key, kvp.Value));
            }

            // copy basket because we're taking stuff out
            // This is not strictly necessary but still a good idea for future
            // extensions, i.e. in case we need to list the initial basket
            var offerBasket = new Basket(basket);
            foreach (var offer in _config.Offers)
            {
                // How many times do the required items fit?
                int fitsTimes = offerBasket.FitsTimes(offer.Condition);
                if (fitsTimes > 0)
                {
                    // take items out and apply the offer
                    offerBasket.SubtractTimes(offer.Condition, fitsTimes);
                    result.AddOffer(offer, offer.PriceDelta * fitsTimes);
                }
            }

            return result;
        }

        /// <summary>
        /// Returns the total price for a given product
        /// </summary>
        /// <param name="productId">The product</param>
        /// <param name="count">How many times it is in the basket</param>
        /// <returns>Total price</returns>
        private decimal GetTotal(string productId, int count)
        {
            if (!_products.ContainsKey(productId))
            {
                throw new Exception($"Unknown product [{productId}]");
            }
            return _products[productId].PricePerUnit * count;
        }
    }
}