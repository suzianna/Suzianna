using System;
using System.Collections.Generic;
using System.Text;
using NFluent;
using Suzianna.Reporting.Model;
using Suzianna.Reporting.Tests.Unit.TestUtils;
using Xunit;
using static Suzianna.Reporting.Tests.Unit.TestUtils.TestConstants;

namespace Suzianna.Reporting.Tests.Unit
{
    public class ReportFeatureTests : ReportTests
    {
        [Fact]
        public void should_add_feature_with_correct_title_to_report()
        {
            Reporter.FeatureStarted(SampleFeatures.ReturnsGoToStock);

            var report = Reporter.GetReport().ToXmlSource();

            Check.That(report.EvaluateXPath("//Report/Features/Feature/@Title")).IsEqualTo(SampleFeatures.ReturnsGoToStock.Title);
        }

        [Fact]
        public void should_add_feature_with_correct_description_to_report()
        {
            Reporter.FeatureStarted(SampleFeatures.ReturnsGoToStock);

            var report = Reporter.GetReport().ToXmlSource();

            Check.That(report.EvaluateXPath("//Report/Features/Feature/@Description")).IsEqualTo(SampleFeatures.ReturnsGoToStock.Description);
        }

        [Fact]
        public void should_add_multiple_features()
        {
            Reporter.FeatureStarted(SampleFeatures.ReturnsGoToStock);
            Reporter.FeatureStarted(SampleFeatures.OnlineMembershipRenewal);

            var report = Reporter.GetReport().ToXmlSource();

            Check.That(report.EvaluateXPath("count(//Report/Features/Feature)").ToNumber()).IsEqualTo(2);
            Check.That(report.EvaluateXPath("//Report/Features/Feature[1]/@Title")).IsEqualTo(SampleFeatures.ReturnsGoToStock.Title);
            Check.That(report.EvaluateXPath("//Report/Features/Feature[2]/@Title")).IsEqualTo(SampleFeatures.OnlineMembershipRenewal.Title);
            Check.That(report.EvaluateXPath("//Report/Features/Feature[1]/@Description")).IsEqualTo(SampleFeatures.ReturnsGoToStock.Description);
            Check.That(report.EvaluateXPath("//Report/Features/Feature[2]/@Description")).IsEqualTo(SampleFeatures.OnlineMembershipRenewal.Description);
        }
    }
}
