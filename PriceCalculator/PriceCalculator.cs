using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using PriceCalculator.Models;

namespace PriceCalculator
{
    public class PriceCalculator
    {
        private readonly PriceCalculatorConfig _config;
        private readonly Dictionary<string, Product> _products;

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
        public CalculationResult Calculate(string[] items)
        {
            var basket = new Basket(items);
            var result = new CalculationResult();
            foreach (var kvp in basket)
            {
                result.Add(GetTotal(kvp.Key, kvp.Value));
            }

            // copy basket because we're taking stuff out
            // after applying the offer
            var offerBasket = new Basket(basket);
            foreach (var offer in _config.Offers)
            {
                decimal offerTotal = 0;
                // TODO: Endless loop?
                while (offerBasket.SubtractIfPossible(offer.Condition))
                {
                    offerTotal += offer.PriceDelta;
                }
                if (offerTotal != 0)
                {
                    result.AddOffer(offer, offerTotal);
                }
            }

            return result;
        }

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