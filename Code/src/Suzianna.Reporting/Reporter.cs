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
        }

        public void TestSuiteEnded()
        {
            _report.TotalDuration.SetEndDate(_clock.Now());
        }

        public XmlDocument GetReport()
        {
            return _report.ToXml();
        }
    }
}
