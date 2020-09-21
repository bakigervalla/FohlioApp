using System;
using System.Diagnostics;

namespace Fohlio.RevitReportsIntegration.Utils.Extensions
{
    public static class DoubleExtensions
    {
        private const double DefaultTolerance = 0.00000001;

        [DebuggerStepThrough]
        public static bool IsAlmostEqualTo(this double x, double value)
        {
            return IsAlmostEqualTo(x, value, DefaultTolerance);
        }

        [DebuggerStepThrough]
        public static bool IsAlmostEqualTo(this double x, double value, double tolerance)
        {
            return Math.Abs(x - value) < tolerance;
        }

        [DebuggerStepThrough]
        public static bool IsAlmostEqualToOrMoreThan(this double x, double value)
        {
            return IsAlmostEqualTo(x, value) || x > value;
        }

        [DebuggerStepThrough]
        public static bool IsAlmostEqualToOrLessThan(this double x, double value)
        {
            return IsAlmostEqualToOrLessThan(x, value, DefaultTolerance);
        }

        [DebuggerStepThrough]
        public static bool IsAlmostEqualToOrLessThan(this double x, double value, double tolerance)
        {
            return IsAlmostEqualTo(x, value, tolerance) || x < value;
        }

        [DebuggerStepThrough]
        public static bool IsAlmostBetween(this double x, double min, double max)
        {
            return IsAlmostEqualToOrMoreThan(x, min) && IsAlmostEqualToOrLessThan(x, max);
        }
    }
}