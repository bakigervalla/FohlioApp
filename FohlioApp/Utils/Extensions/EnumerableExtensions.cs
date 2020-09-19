using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Fohlio.RevitReportsIntegration.Utils.Extensions
{
    public static class EnumerableExtensions
    {
        [DebuggerStepThrough]
        public static bool IsEmpty<T>(this IEnumerable<T> values)
        {
            return !values.Any();
        }

        [DebuggerStepThrough]
        public static bool IsEmpty<T>(this IEnumerable<T> value, Func<T, bool> predicate)
        {
            return !value.Any(predicate);
        }
    }
}