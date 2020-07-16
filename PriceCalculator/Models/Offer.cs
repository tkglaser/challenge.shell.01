namespace PriceCalculator.Models
{
    /// <summary>
    /// Represents an offer
    /// </summary>
    public class Offer
    {
        public Basket Condition { get; set; }
        public string Description { get; set; }
        public decimal PriceDelta { get; set; }
    }
}