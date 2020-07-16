namespace PriceCalculator.Models
{
    public class PriceCalculatorConfig
    {
        public const string Section = "PriceCalculator";
        public Product[] Products { get; set; }
        public Offer[] Offers { get; set; }
    }
}