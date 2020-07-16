using System.Linq;
using System.Text;

namespace PriceCalculator.Models
{
    public class CalculationResult
    {
        public decimal SubTotal { get; }
        public AppliedOffer[] Offers { get; }
        public decimal Total => SubTotal - Offers.Sum(o => o.PriceDelta);

        public CalculationResult(decimal subTotal, AppliedOffer[] offers)
        {
            SubTotal = subTotal;
            Offers = offers;
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