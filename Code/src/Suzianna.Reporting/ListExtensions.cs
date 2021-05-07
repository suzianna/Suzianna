using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Suzianna.Reporting
{
    internal static class ListExtensions
    {
        public static bool IsEmpty<T>(this List<T> items)
        {
            return !items.Any();
        }
    }
}
