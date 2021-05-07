using System.Collections.Generic;

namespace Suzianna.Reporting.XmlNodes
{
    public class StepNode
    {
        public string Text { get; set; }
        public List<string> Events { get; set; }
        public StepNode()
        {
            this.Events = new List<string>();
        }
    }
}