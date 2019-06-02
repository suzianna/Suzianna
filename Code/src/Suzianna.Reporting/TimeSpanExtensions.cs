using System;

namespace Suzianna.Reporting
{
    public static class TimeSpanExtensions
    {
        public static string ToReportFormat(this TimeSpan? time)
        {
            if (time == null) return ReportConstants.Unknown;
            return time.Value.ToString();
        }
    }
}