using System;

namespace Suzianna.Reporting.Model
{
    public class DateRange
    {
        public DateTime? StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public string Duration => CalculateDuration();
        public void SetStartDate(DateTime dateTime)
        {
            this.StartDate = dateTime;
        }
        public void SetEndDate(DateTime dateTime)
        {
            this.EndDate = dateTime;
        }
        private string CalculateDuration()
        {
            if (this.EndDate == null || this.StartDate == null) return ReportConstants.Unknown;
            return (this.EndDate.Value - this.StartDate.Value).ToString("hh':'mm':'ss");
        }
    }
}