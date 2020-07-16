using System;
using System.Text;

namespace PriceCalculator.Extensions
{
    public static class DecimalFormatExtensions
    {
        /// <summary>
        /// Formats currency according to spec.
        /// Values less than £1 are formatted like 99p
        /// </summary>
        /// <param name="value">The value to format</param>
        /// <returns>The formatted string</returns>
        public static string ToCustomFormatString(this decimal value)
        {
            if (Math.Abs(value) < 1.0M)
            {
                var sb = new StringBuilder();
                sb.Append((value * 100M).ToString("0"));
                sb.Append("p");
                return sb.ToString();
            }
            else
            {
                // fall back to normal formatting
                return value.ToString("C");
            }
        }
    }
}