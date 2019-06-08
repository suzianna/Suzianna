using System.Linq;
using NFluent;
using Suzianna.Reporting.Tests.Unit.TestUtils;
using Xunit;
using static Suzianna.Reporting.Tests.Unit.TestUtils.TestConstants;

namespace Suzianna.Reporting.Tests.Unit.ReportParsingTests
{
    public class ReportParserFeatureTests : ReportTests
    {
        private Feature _feature;
        public ReportParserFeatureTests()
        {
            _feature = SampleFeatures.ReturnsGoToStock;
        }
        
        [Fact]
        public void should_parse_features()
        {
            Reporter.FeatureStarted(_feature.Title, _feature.Description);

            var report = ReportParser.Parse(Reporter.ExportXml());

            Check.That(report.Features).CountIs(1);
            Check.That(report.Features.First().Title).IsEqualTo(_feature.Title);
            Check.That(report.Features.First().Description).IsEqualTo(_feature.Description);
        }
    }
}