using System;
using System.Collections.Generic;
using System.Text;

namespace Suzianna.Core
{
    public static class StringExtentions
    {
        public static string SurroundBy(this string value, string surrondValue)
        {
            return $"{surrondValue}{value}{surrondValue}";
        }

        public static string SurroundByDoubleQuotes(this string value)
        {
            return SurroundBy(value, Characters.DoubleQoute);
        }
    }
}
