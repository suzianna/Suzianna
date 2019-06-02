using System;

namespace Suzianna.Reporting.Model
{
    public class DateRange
    {
        public DateTime? StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public void SetStartDate(DateTime dateTime)
        {
            this.StartDate = dateTime;
        }
        public void SetEndDate(DateTime dateTime)
        {
            this.EndDate = dateTime;
        }
        public TimeSpan? CalculateDuration()
        {
            if (this.EndDate == null || this.StartDate == null) return null;
            return this.EndDate.Value - this.StartDate.Value;
        }
    }
}