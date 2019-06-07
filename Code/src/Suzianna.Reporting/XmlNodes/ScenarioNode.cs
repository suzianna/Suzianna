using System;
using System.Collections.Generic;

namespace Suzianna.Reporting.XmlNodes
{
    public class ScenarioNode
    {
        public string Title { get; set; }
        public ScenarioStatus Status { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public TimeSpan? Duration { get; set; }
        public string FailureReason { get; set; }
        public List<StepNode> Steps { get; set; }
        public ScenarioNode()
        {
            this.Steps = new List<StepNode>();
        }

        internal void MarkAsPassed(DateTime date)
        {
            this.Status = ScenarioStatus.Passed;
            SetEnd(date);
        }
        internal void MarkAsFailed(string reason, DateTime date)
        {
            this.Status = ScenarioStatus.Failed;
            this.FailureReason = reason;
            SetEnd(date);
        }
        private void SetEnd(DateTime date)
        {
            this.End = date;
            if (this.Start != null)
                this.Duration = this.End - this.Start;
        }

    }
}