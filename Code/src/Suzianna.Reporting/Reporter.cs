using System;
using System.IO;
using System.Text;
using System.Xml;
using Suzianna.Reporting.Model;

namespace Suzianna.Reporting
{
    public class Reporter
    {
        private readonly IClock _clock;
        private readonly Report _report;
        public Reporter(IClock clock)
        {
            _clock = clock;
            _report = new Report();
        }

        public void TestSuiteStarted()
        {
            _report.TotalDuration.SetStartDate(_clock.Now());
            //_doc.AppendChild(reportElement);
        }

        public void TestSuiteEnded()
        {
            _report.TotalDuration.SetEndDate(_clock.Now());

            //if (_doc.DocumentElement == null) throw new TestSuiteHasNotBeenStartedException();
            //endDate = _clock.Now();
            //_doc.DocumentElement.SetAttribute(ReportConstants.Attributes.EndTime, endDate.ToReportFormat());

            //var diff = endDate - startDate;
            //_doc.DocumentElement.SetAttribute(ReportConstants.Attributes.Duration, diff.ToString());
        }

        public XmlDocument GetReport()
        {
            return _report.ToXml();
        }
    }
}
