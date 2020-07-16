namespace PriceCalculator.Models
{
    /// <summary>
    /// Model class for an offer that has been applied and has a total value
    /// </summary>
    public class AppliedOffer
    {
        public string Description { get; set; }
        public decimal PriceDelta { get; set; }
    }
}