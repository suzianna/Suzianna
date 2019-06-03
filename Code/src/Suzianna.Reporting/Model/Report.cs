using System;
using System.Xml;
using Suzianna.Reporting.Template;

namespace Suzianna.Reporting.Model
{
    public class Report
    {
        private XmlDocument _doc;
        public Report()
        {
            _doc = new XmlDocument();
            TotalDuration = new DateRange();
        }
        public DateRange TotalDuration { get; private set; }
        public XmlDocument ToXml()
        {
            return TemplateAgent.Render(this);
        }
    }
}