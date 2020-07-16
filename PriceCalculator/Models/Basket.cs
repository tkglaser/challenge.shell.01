using System.Collections.Generic;

namespace PriceCalculator.Models
{
    public class Basket : Dictionary<string, int>
    {
        public Basket()
        {

        }

        public Basket(Basket other) : base(other)
        {

        }

        public Basket(string[] items)
        {
            foreach (var item in items)
            {
                if (!ContainsKey(item))
                {
                    Add(item, 1);
                }
                else
                {
                    ++this[item];
                }
            }
        }

        public bool SubtractIfPossible(Basket other)
        {
            if (!CanSubtract(other))
            {
                return false;
            }
            foreach (var kvp in other)
            {
                this[kvp.Key] -= kvp.Value;
            }
            return true;
        }

        private bool CanSubtract(Basket other)
        {
            foreach (var kvp in other)
            {
                if (!ContainsKey(kvp.Key))
                {
                    return false;
                }
                else if (this[kvp.Key] < kvp.Value)
                {
                    return false;
                }
            }
            return true;
        }
    }
}