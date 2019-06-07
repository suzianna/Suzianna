using NFluent;
using Suzianna.Reporting.Tests.Unit.TestUtils;
using Xunit;

namespace Suzianna.Reporting.Tests.Unit.ReporterGenerationTests
{
    public class ReportFeatureTests : ReportTests
    {
        [Fact]
        public void should_add_feature_with_correct_title_to_report()
        {
            Reporter.FeatureStarted(TestConstants.SampleFeatures.ReturnsGoToStock);

            var report = Reporter.GetReport().ToXmlSource();

            Check.That(report.EvaluateXPath("//Report/Features/Feature/@Title")).IsEqualTo(TestConstants.SampleFeatures.ReturnsGoToStock.Title);
        }

        [Fact]
        public void should_add_feature_with_correct_description_to_report()
        {
            Reporter.FeatureStarted(TestConstants.SampleFeatures.ReturnsGoToStock);

            var report = Reporter.GetReport().ToXmlSource();

            Check.That(report.EvaluateXPath("//Report/Features/Feature/@Description")).IsEqualTo(TestConstants.SampleFeatures.ReturnsGoToStock.Description);
        }

        [Fact]
        public void should_add_multiple_features()
        {
            Reporter.FeatureStarted(TestConstants.SampleFeatures.ReturnsGoToStock);
            Reporter.FeatureStarted(TestConstants.SampleFeatures.OnlineMembershipRenewal);

            var report = Reporter.GetReport().ToXmlSource();

            Check.That(report.EvaluateXPath("count(//Report/Features/Feature)").ToNumber()).IsEqualTo(2);
            Check.That(report.EvaluateXPath("//Report/Features/Feature[1]/@Title")).IsEqualTo(TestConstants.SampleFeatures.ReturnsGoToStock.Title);
            Check.That(report.EvaluateXPath("//Report/Features/Feature[2]/@Title")).IsEqualTo(TestConstants.SampleFeatures.OnlineMembershipRenewal.Title);
            Check.That(report.EvaluateXPath("//Report/Features/Feature[1]/@Description")).IsEqualTo(TestConstants.SampleFeatures.ReturnsGoToStock.Description);
            Check.That(report.EvaluateXPath("//Report/Features/Feature[2]/@Description")).IsEqualTo(TestConstants.SampleFeatures.OnlineMembershipRenewal.Description);
        }
    }
}
