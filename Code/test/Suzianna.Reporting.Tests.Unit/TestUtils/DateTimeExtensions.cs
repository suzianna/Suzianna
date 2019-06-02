using System;

namespace Suzianna.Reporting.Tests.Unit.TestUtils
{
    public static class DateTimeExtensions
    {
        public static DateTime At(this DateTime sourceDate,  string time)
        {
            return At(sourceDate, TimeSpan.Parse(time));
        }
        public static DateTime At(this DateTime sourceDate, TimeSpan time)
        {
            return sourceDate.Add(time);
        }
    }
}