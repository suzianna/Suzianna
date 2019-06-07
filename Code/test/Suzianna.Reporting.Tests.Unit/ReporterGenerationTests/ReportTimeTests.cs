using NFluent;
using Suzianna.Reporting.Tests.Unit.TestUtils;
using Xunit;

namespace Suzianna.Reporting.Tests.Unit.ReporterGenerationTests
{
    public class ReportTimeTests : ReportTests
    {
        [Fact]
        public void should_set_test_suite_start_time()
        {
            var start = DateFactory.SomeDate();
            GotoTimeAndStartTestSuite(start);
            var expected = start.ToReportFormat();

            var report = Reporter.GetReport().ToXmlSource();

            Check.That(report.EvaluateXPath("//Report/Start")).IsEqualTo(expected);
        }

        [Fact]
        public void should_set_test_suite_end_time()
        {
            var date = DateFactory.SomeDate();
            GotoTimeAndEndTestSuite(date);

            var report = Reporter.GetReport().ToXmlSource();

            Check.That(report.EvaluateXPath("//Report/End")).IsEqualTo(date.ToReportFormat());
        }

        [Fact]
        public void should_calculate_duration_of_test_suite_correctly()
        {
            var start = DateFactory.SomeDate().At("17:30:20");
            var end = DateFactory.SomeDate().At("17:32:00");
            var expected = "PT1M40S";
            GotoTimeAndStartTestSuite(start);
            GotoTimeAndEndTestSuite(end);

            var report = Reporter.GetReport().ToXmlSource();

            Check.That(report.EvaluateXPath("//Report/Duration")).IsEqualTo(expected);
        }

        [Fact]
        public void should_set_report_duration_to_unknown_when_report_start_not_called()
        {
            var report = Reporter.GetReport().ToXmlSource();

            Check.That(report.EvaluateXPath("//Report/Duration")).IsEmpty();
            Check.That(report.EvaluateXPath("//Report/Duration/@*[local-name()='nil']").ToBoolean()).IsTrue();
        }

        [Fact]
        public void should_set_report_duration_to_unknown_when_report_end_not_called()
        {
            Reporter.TestSuiteStarted();

            var report = Reporter.GetReport().ToXmlSource();

            Check.That(report.EvaluateXPath("//Report/Duration")).IsEmpty();
            Check.That(report.EvaluateXPath("//Report/Duration/@*[local-name()='nil']").ToBoolean()).IsTrue();
        }

        [Fact]
        public void should_set_start_time_to_unknown_when_report_start_not_called()
        {
            var report = Reporter.GetReport().ToXmlSource();

            Check.That(report.EvaluateXPath("//Report/Start")).IsEmpty();
            Check.That(report.EvaluateXPath("//Report/Start/@*[local-name()='nil']").ToBoolean()).IsTrue();
        }
    }
}
