using System;
using System.Linq;

namespace Fohlio.RevitReportsIntegration.Utils.Extensions
{
    public static class EnumExtensions
    {
        public static bool In<T>(this T item, params T[] other) where T : struct => other.Any(x => Equals(x, item));

        public static bool EqualsIgnoreCase(this string source, string value)
        {
            return source.Equals(value, StringComparison.OrdinalIgnoreCase);
        }

        public static bool ContainsIgnoreCase(this string source, string value)
        {
            return source.IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0;
        }

    }
}