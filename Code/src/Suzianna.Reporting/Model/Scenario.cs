namespace Suzianna.Reporting.Model
{
    public class Scenario
    {
        public string Title { get; set; }
        public string Status { get; set; }
        public string FailureReason { get; set; }
        public DateRange Duration { get; internal set; }
        public Scenario()
        {
            this.Duration = new DateRange();
        }
    }
}