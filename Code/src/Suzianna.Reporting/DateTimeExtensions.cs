using System;
using System.Collections.Generic;
using System.Text;

namespace Suzianna.Reporting
{
    public static class DateTimeExtensions
    {
        public static string ToReportFormat(this DateTime date)
        {
            return date.ToString(ReportConstants.DateFormat);
        }

        public static string ToReportFormat(this DateTime? date)
        {
            if (date == null) return ReportConstants.Unknown;

            return date.Value.ToString(ReportConstants.DateFormat);
        }
    }
}
