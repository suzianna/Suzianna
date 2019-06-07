using System.Collections.Generic;
using System.Linq;
using Suzianna.Reporting.Exceptions;

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
        internal void AddStep(Step step)
        {
            this.Steps.Add(step);
        }
        internal void AddEvent(string eventText)
        {
            if (this.Steps.IsEmpty())
                throw new StepNotFoundException(this.Title);

            var latestStep = this.Steps.Last();
            latestStep.Events.Add(eventText);
        }
    }
}