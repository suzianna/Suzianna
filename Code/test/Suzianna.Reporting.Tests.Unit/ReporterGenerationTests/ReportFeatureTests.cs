using NFluent;
using Suzianna.Reporting.Tests.Unit.TestUtils;
using Xunit;
using static Suzianna.Reporting.Tests.Unit.TestUtils.TestConstants;

namespace Suzianna.Reporting.Tests.Unit.ReporterGenerationTests
{
    public class ReportFeatureTests : ReportTests
    {
        [Fact]
        public void should_add_feature_with_correct_title_to_report()
        {
            var feature = SampleFeatures.ReturnsGoToStock;
            Reporter.FeatureStarted(feature.Title, feature.Description);

            var report = Reporter.ExportXml();

            Check.That(report.EvaluateXPath("//Report/Features/FeatureNode/Title")).IsEqualTo(feature.Title);
        }

        [Fact]
        public void should_add_feature_with_correct_description_to_report()
        {
            var feature = SampleFeatures.ReturnsGoToStock;
            Reporter.FeatureStarted(feature.Title, feature.Description);

            var report = Reporter.ExportXml();

            Check.That(report.EvaluateXPath("//Report/Features/FeatureNode/Description")).IsEqualTo(feature.Description);
        }

        [Fact]
        public void should_add_multiple_features()
        {
            Reporter.FeatureStarted(SampleFeatures.ReturnsGoToStock.Title, SampleFeatures.ReturnsGoToStock.Description);
            Reporter.FeatureStarted(SampleFeatures.OnlineMembershipRenewal.Title, SampleFeatures.OnlineMembershipRenewal.Description);

            var report = Reporter.ExportXml();

            Check.That(report.EvaluateXPath("count(//Report/Features/FeatureNode)").ToNumber()).IsEqualTo(2);
            Check.That(report.EvaluateXPath("//Report/Features/FeatureNode[1]/Title")).IsEqualTo(SampleFeatures.ReturnsGoToStock.Title);
            Check.That(report.EvaluateXPath("//Report/Features/FeatureNode[2]/Title")).IsEqualTo(SampleFeatures.OnlineMembershipRenewal.Title);
            Check.That(report.EvaluateXPath("//Report/Features/FeatureNode[1]/Description")).IsEqualTo(SampleFeatures.ReturnsGoToStock.Description);
            Check.That(report.EvaluateXPath("//Report/Features/FeatureNode[2]/Description")).IsEqualTo(SampleFeatures.OnlineMembershipRenewal.Description);
        }
    }
}
