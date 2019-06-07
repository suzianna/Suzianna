using System;
using System.Collections.Generic;
using System.Text;
using Org.XmlUnit;
using Org.XmlUnit.Builder;

namespace Suzianna.Reporting.Tests.Unit.TestUtils
{
    public static class StringExtensions
    {
        public static ISource ToXmlSource(this string target)
        {
            return Input.FromString(target).Build();
        }

        public static int ToNumber(this string input)
        {
            return int.Parse(input);
        }

        public static bool ToBoolean(this string input)
        {
            return bool.Parse(input);
        }
    }
}
