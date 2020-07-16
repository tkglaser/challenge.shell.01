using System;
using System.Collections.Generic;

namespace PriceCalculator.Models
{
    public class Basket : Dictionary<string, int>
    {
        public Basket()
        {
            // for deserialisation
        }

        public Basket(Basket other) : base(other)
        {

        }

        public Basket(string[] items)
        {
            foreach (var item in items)
            {
                var safeitem = item.ToLowerInvariant();
                if (!ContainsKey(safeitem))
                {
                    Add(safeitem, 1);
                }
                else
                {
                    ++this[safeitem];
                }
            }
        }

        public int FitsTimes(Basket other)
        {
            int contained = int.MaxValue;
            foreach (var kvp in other)
            {
                var safeKey = kvp.Key.ToLowerInvariant();
                if (!ContainsKey(safeKey))
                {
                    return 0;
                }

                // avoid div by zero
                if (kvp.Value > 0)
                {
                    int itemTimesContained = this[safeKey] / kvp.Value;
                    contained = Math.Min(contained, itemTimesContained);
                }

                // cut loop short when possible
                if (contained == 0)
                {
                    return 0;
                }
            }
            return contained == int.MaxValue ? 0 : contained;
        }

        public void SubtractTimes(Basket other, int times)
        {
            foreach (var kvp in other)
            {
                var safeKey = kvp.Key.ToLowerInvariant();
                this[safeKey] -= kvp.Value * times;
                if (this[safeKey] < 1)
                {
                    this.Remove(safeKey);
                }
            }
        }
    }
}