using System;
using NFluent;
using Xunit;

namespace Suzianna.Reporting.Tests.Unit.ReportParsingTests
{
    public class ReportParserTimeTests : ReportTests
    {
        [Fact]
        public void should_parse_empty_report()
        {
            var report = ReportParser.Parse(Reporter.ExportXml());

            Check.That(report.Features).IsEmpty();
            Check.That(report.Start).IsNull();
            Check.That(report.End).IsNull();
            Check.That(report.Duration).IsNull();
        }

        [Fact]
        public void should_parse_report_start_time()
        {
            var start = DateTime.Parse("2010/02/03 15:30:20");
            GotoTimeAndStartTestSuite(start);

            var report = ReportParser.Parse(Reporter.ExportXml());

            Check.That(report.Start.Value).IsEqualTo(start);
        }
        
        [Fact]
        public void should_parse_report_end_time()
        {
            var end = DateTime.Parse("2010/02/03 17:30:20");
            GotoTimeAndEndTestSuite(end);

            var report = ReportParser.Parse(Reporter.ExportXml());

            Check.That(report.End.Value).IsEqualTo(end);
        }
        
        [Fact]
        public void should_parse_report_duration_time()
        {
            var start = DateTime.Parse("2010/02/03 15:30:20");
            var end = DateTime.Parse("2010/02/03 17:10:40");
            var expectedDuration = TimeSpan.Parse("01:40:20");
            GotoTimeAndStartTestSuite(start);
            GotoTimeAndEndTestSuite(end);

            var report = ReportParser.Parse(Reporter.ExportXml());

            Check.That(report.Duration).IsEqualTo(expectedDuration);
        }
    }
}