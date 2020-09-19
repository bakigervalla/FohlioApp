using System.Linq;

namespace Fohlio.RevitReportsIntegration.Utils.Extensions
{
    public static class EnumExtensions
    {
        public static bool In<T>(this T item, params T[] other) where T : struct => other.Any(x => Equals(x, item));
    }
}