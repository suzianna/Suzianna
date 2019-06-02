using System.Collections.Generic;

namespace Suzianna.Reporting.Model
{
    public class Feature
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateRange Duration { get; set; }
        public List<Scenario> Scenarios { get; set; }
    }
}