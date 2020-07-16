namespace PriceCalculator.Models
{
    /// <summary>
    /// Representation of a product
    /// The name is also the key, I would
    /// normally use a product id but decided to keep it simple
    /// </summary>
    public class Product
    {
        public string Name { get; set; }
        public decimal PricePerUnit { get; set; }
    }
}