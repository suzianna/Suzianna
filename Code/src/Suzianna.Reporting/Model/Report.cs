using System;
using System.Xml;

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
            var reportElement = _doc.CreateElement(ReportConstants.Elements.Report);
            reportElement.SetAttribute(ReportConstants.Attributes.StartTime, TotalDuration.StartDate.ToReportFormat());
            reportElement.SetAttribute(ReportConstants.Attributes.EndTime, TotalDuration.EndDate.ToReportFormat());
            reportElement.SetAttribute(ReportConstants.Attributes.Duration, TotalDuration.CalculateDuration().ToReportFormat());
            _doc.AppendChild(reportElement);
            return _doc;
        }
    }
}