using System;
using System.Collections.Generic;

namespace PriceCalculator.Models
{
    /// <summary>
    /// Represents a shopping basket with items as a dictionary of the
    /// item name and number of times it is in the basket.
    /// This is faster than a simple array because items can be looked up
    /// with O(1)
    /// </summary>
    public class Basket : Dictionary<string, int>
    {
        /// <summary>
        /// Constructor for deserialisation (config file)
        /// </summary>
        public Basket()
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="other">Other basket that is copied into this one</param>
        /// <returns></returns>
        public Basket(Basket other) : base(other)
        {
        }

        /// <summary>
        /// Constructs the basket from a string array
        /// </summary>
        /// <param name="items">The items in the basket, for example ["Bread", "Milk"]</param>
        /// <remarks>Multiple items are allowed like ["Milk", "Bread", "Milk"]</remarks>
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

        /// <summary>
        /// Returns how many times another basket fits into this one
        /// </summary>
        /// <param name="other">The other basket</param>
        /// <returns>number of times the other basket fits</returns>
        /// <example>If this basket is ["Milk", "Bread", "Milk"], the basket ["Milk", "Bread"] fits once</example>
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

                // If the item cannot be fit, we can abort
                if (contained == 0)
                {
                    return 0;
                }
            }
            return contained == int.MaxValue ? 0 : contained;
        }

        /// <summary>
        /// Take other basket out of this one
        /// </summary>
        /// <param name="other">The other basket</param>
        /// <param name="times">How many times the other basket is subtracted</param>
        /// <example>If this basket is ["Milk", "Bread", "Milk"] and we subtract ["Milk"] twice, what remains is ["Bread"]</example>
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