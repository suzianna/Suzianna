using System;
using System.IO;
using System.Text;
using System.Xml;
using Microsoft.VisualStudio.TestPlatform.Common.ExtensionFramework;
using NFluent;
using Suzianna.Reporting.Tests.Unit.TestDoubles;
using Suzianna.Reporting.Tests.Unit.TestUtils;
using Xunit;
using static Suzianna.Reporting.Tests.Unit.TestUtils.DateFactory;

namespace Suzianna.Reporting.Tests.Unit
{
    public class ReportTimeTests
    {
        private Reporter _reporter;
        private StubClock _clock;
        public ReportTimeTests()
        {
            _clock = new StubClock();
            _reporter = new Reporter(_clock);
        }

        [Fact]
        public void should_set_test_suite_start_time()
        {
            var start = SomeDate();
            GotoTimeAndStartTestSuite(start);

            var report = _reporter.GetReport();

            Check.That(report.DocumentElement.Attributes[ReportConstants.Attributes.StartTime].Value).IsEqualTo(start.ToReportFormat());
        }

        [Fact]
        public void should_set_test_suite_end_time()
        {
            var date = SomeDate();
            GotoTimeAndEndTestSuite(date);

            var report = _reporter.GetReport();

            Check.That(report.DocumentElement.Attributes[ReportConstants.Attributes.EndTime].Value).IsEqualTo(date.ToReportFormat());
        }

        [Fact]
        public void should_calculate_duration_of_test_suite_correctly()
        {
            var start = SomeDate().At("17:30:20");
            var end = SomeDate().At("17:32:00");
            var expected = "00:01:40";
            GotoTimeAndStartTestSuite(start);
            GotoTimeAndEndTestSuite(end);

            var report = _reporter.GetReport();

            Check.That(report.DocumentElement.Attributes[ReportConstants.Attributes.Duration].Value).IsEqualTo(expected);
        }

        [Fact]
        public void should_set_report_duration_to_unknown_when_report_start_not_called()
        {
            var report = _reporter.GetReport();

            Check.That(report.DocumentElement.Attributes[ReportConstants.Attributes.Duration].Value).IsEqualTo(ReportConstants.Unknown);
        }

        [Fact]
        public void should_set_report_duration_to_unknown_when_report_end_not_called()
        {
            _reporter.TestSuiteStarted();

            var report = _reporter.GetReport();

            Check.That(report.DocumentElement.Attributes[ReportConstants.Attributes.Duration].Value).IsEqualTo(ReportConstants.Unknown);
        }

        [Fact]
        public void should_set_start_time_to_unknown_when_report_start_not_called()
        {
            var report = _reporter.GetReport();

            Check.That(report.DocumentElement.Attributes[ReportConstants.Attributes.StartTime].Value).IsEqualTo(ReportConstants.Unknown);
        }

        private void GotoTimeAndStartTestSuite(DateTime time)
        {
            _clock.TimeTravelTo(time);
            _reporter.TestSuiteStarted();
        }
        private void GotoTimeAndEndTestSuite(DateTime time)
        {
            _clock.TimeTravelTo(time);
            _reporter.TestSuiteEnded();
        }
    }
}
