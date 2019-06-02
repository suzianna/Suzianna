using System.Collections.Generic;
using System.Linq;

namespace Suzianna.Core.Tests.Unit.Tests.Utilities
{
    internal static class EnumerableExtensions
    {
        internal static T Second<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.ElementAt(1);
        }
        internal static T Third<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.ElementAt(2);
        }
    }
}