namespace PriceCalculator.Models
{
    /// <summary>
    /// The configuration as loaded from appsettings.json
    /// Contains products and offers
    /// </summary>
    public class PriceCalculatorConfig
    {
        public const string Section = "PriceCalculator";
        public Product[] Products { get; set; }
        public Offer[] Offers { get; set; }
    }
}