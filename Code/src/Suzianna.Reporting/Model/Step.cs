using System.Collections.Generic;

namespace Suzianna.Reporting.Model
{
    public class Step
    {
        public string Title { get;private set; }
        public List<string> Events { get; set; }
        public Step(string title)
        {
            Title = title;
            this.Events = new List<string>();
        }
    }
}