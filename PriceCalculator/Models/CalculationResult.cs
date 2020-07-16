using System.Collections.Generic;
using System.Linq;
using System.Text;
using PriceCalculator.Extensions;

namespace PriceCalculator.Models
{
    /// <summary>
    /// Represent the result of a basket calculation
    /// </summary>
    public class CalculationResult
    {
        public decimal SubTotal { get; private set; } = 0.0M;
        public List<AppliedOffer> Offers { get; } = new List<AppliedOffer>();
        public decimal Total => SubTotal + Offers.Sum(o => o.PriceDelta);

        /// <summary>
        /// Add a value to the SubTotal
        /// </summary>
        /// <param name="value">The value</param>
        public void Add(decimal value)
        {
            SubTotal += value;
        }

        /// <summary>
        /// Apply an offer to this calculation
        /// </summary>
        /// <param name="offer">The offer</param>
        /// <param name="value">The total value that this offer has changed the basket</param>
        public void AddOffer(Offer offer, decimal value)
        {
            var appliedOffer = new AppliedOffer();
            appliedOffer.Description = offer.Description;
            appliedOffer.PriceDelta = value;
            Offers.Add(appliedOffer);
        }

        /// <summary>
        /// Format the result according to the spec
        /// </summary>
        /// <returns>The formatted result</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Subtotal: {SubTotal.ToCustomFormatString()}");
            foreach (var offer in Offers)
            {
                sb.AppendLine($"{offer.Description}: {offer.PriceDelta.ToCustomFormatString()}");
            }
            if (!Offers.Any())
            {
                sb.AppendLine("(No offers available)");
            }
            sb.AppendLine($"Total: {Total.ToCustomFormatString()}");
            return sb.ToString();
        }
    }
}