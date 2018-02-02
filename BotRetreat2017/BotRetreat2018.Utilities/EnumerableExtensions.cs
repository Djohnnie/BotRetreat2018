using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BotRetreat2018.Utilities
{
    public static class EnumerableExtensions
    {
        public static Double AverageOrDefault(this IEnumerable<Double> list, Double defaultValue = default(Double))
        {
            return list.Any() ? list.Average() : defaultValue;
        }
    }
}