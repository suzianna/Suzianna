using System.Collections.Generic;

namespace Suzianna.Reporting.Model
{
    public class Scenario
    {
        public string Title { get; set; }
        public string Status { get; set; }
        public string FailureReason { get; set; }
        public DateRange Duration { get; internal set; }
        public List<Step> Steps { get; set; }
        public Scenario()
        {
            Duration = new DateRange();
            Steps = new List<Step>();
        }
        public void AddStep(Step step)
        {
            this.Steps.Add(step);
        }
    }
}