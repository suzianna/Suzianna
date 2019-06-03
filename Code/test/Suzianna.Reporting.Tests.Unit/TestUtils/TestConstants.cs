using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Suzianna.Reporting.Model;
using Suzianna.Reporting.Tests.Unit.Resources;

namespace Suzianna.Reporting.Tests.Unit.TestUtils
{
    public static class TestConstants
    {
        public static class SampleFeatures
        {
            public static Feature ReturnsGoToStock => CreateReturnsGoToStock();
            public static Feature OnlineMembershipRenewal => CreateOnlineMembershipRenewal();
            private static Feature CreateReturnsGoToStock()
            {
                return new Feature()
                {
                    Title = TestFeaturesResources.ReturnsGoToStock_Title,
                    Description = TestFeaturesResources.ReturnsGoToStock_Description,
                };
            }
            private static Feature CreateOnlineMembershipRenewal()
            {
                return new Feature()
                {
                    Title = TestFeaturesResources.OnlineMembershipRenewal_Title,
                    Description = TestFeaturesResources.OnlineMembershipRenewal_Description,
                };
            }

        }

        public static class SampleScenarios
        {
            public static Scenario RefundedItems => CreateRefundedItems();
            public static Scenario ReplacedItems => CreateReplacedItems();
            private static Scenario CreateRefundedItems()
            {
                return new Scenario()
                {
                    Title = TestScenarioResources.RefundStockScenario_Title,
                };
            }
            private static Scenario CreateReplacedItems()
            {
                return new Scenario()
                {
                    Title = TestScenarioResources.ReplacedItemsScenario_Title
                };
            }
        }
    }
}
