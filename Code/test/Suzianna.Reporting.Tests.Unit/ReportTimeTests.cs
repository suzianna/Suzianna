using System;
using System.IO;
using System.Text;
using System.Xml;
using Microsoft.VisualStudio.TestPlatform.Common.ExtensionFramework;
using NFluent;
using Org.XmlUnit;
using Org.XmlUnit.Builder;
using Org.XmlUnit.Xpath;
using Suzianna.Reporting.Tests.Unit.TestDoubles;
using Suzianna.Reporting.Tests.Unit.TestUtils;
using Xunit;
using static Suzianna.Reporting.Tests.Unit.TestUtils.DateFactory;

namespace Suzianna.Reporting.Tests.Unit
{
    public class ReportTimeTests : ReportTests
    {
        [Fact]
        public void should_set_test_suite_start_time()
        {
            var start = SomeDate();
            GotoTimeAndStartTestSuite(start);
            var expected = start.ToReportFormat();

            var report = Reporter.GetReport().ToXmlSource();

            Check.That(report.EvaluateXPath("//Report/@Start")).IsEqualTo(expected);
        }

        [Fact]
        public void should_set_test_suite_end_time()
        {
            var date = SomeDate();
            GotoTimeAndEndTestSuite(date);

            var report = Reporter.GetReport().ToXmlSource();

            Check.That(report.EvaluateXPath("//Report/@End")).IsEqualTo(date.ToReportFormat());
        }

        [Fact]
        public void should_calculate_duration_of_test_suite_correctly()
        {
            var start = SomeDate().At("17:30:20");
            var end = SomeDate().At("17:32:00");
            var expected = "00:01:40";
            GotoTimeAndStartTestSuite(start);
            GotoTimeAndEndTestSuite(end);

            var report = Reporter.GetReport().ToXmlSource();

            Check.That(report.EvaluateXPath("//Report/@Duration")).IsEqualTo(expected);
        }

        [Fact]
        public void should_set_report_duration_to_unknown_when_report_start_not_called()
        {
            var report = Reporter.GetReport().ToXmlSource();

            Check.That(report.EvaluateXPath("//Report/@Duration")).IsEqualTo(ReportConstants.Unknown);
        }

        [Fact]
        public void should_set_report_duration_to_unknown_when_report_end_not_called()
        {
            Reporter.TestSuiteStarted();

            var report = Reporter.GetReport().ToXmlSource();

            Check.That(report.EvaluateXPath("//Report/@Duration")).IsEqualTo(ReportConstants.Unknown);
        }

        [Fact]
        public void should_set_start_time_to_unknown_when_report_start_not_called()
        {
            var report = Reporter.GetReport().ToXmlSource();

            Check.That(report.EvaluateXPath("//Report/@Start")).IsEqualTo(ReportConstants.Unknown);
        }

       
    }
}
