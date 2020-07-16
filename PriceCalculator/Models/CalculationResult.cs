using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PriceCalculator.Models
{
    public class CalculationResult
    {
        public decimal SubTotal { get; private set; } = 0.0M;
        public List<AppliedOffer> Offers { get; } = new List<AppliedOffer>();
        public decimal Total => SubTotal - Offers.Sum(o => o.PriceDelta);

        public void Add(decimal value)
        {
            SubTotal += value;
        }

        public void AddOffer(Offer offer, decimal value)
        {
            var appliedOffer = new AppliedOffer();
            appliedOffer.Description = offer.Description;
            appliedOffer.PriceDelta = value;
            Offers.Add(appliedOffer);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Subtotal: {SubTotal.ToString("C")}");
            foreach (var offer in Offers)
            {
                sb.AppendLine($"{offer.Description}: {offer.PriceDelta.ToString("C")}");
            }
            if (!Offers.Any())
            {
                sb.AppendLine("(No offers available)");
            }
            sb.AppendLine($"Total: {Total.ToString("C")}");
            return sb.ToString();
        }
    }
}