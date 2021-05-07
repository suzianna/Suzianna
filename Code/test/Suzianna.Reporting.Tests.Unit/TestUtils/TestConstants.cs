using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Suzianna.Reporting.Tests.Unit.Resources;

namespace Suzianna.Reporting.Tests.Unit.TestUtils
{
    internal static class TestConstants
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
            public static string RefundedItems = TestScenarioResources.RefundStockScenario_Title;
            public static string ReplacedItems = TestScenarioResources.ReplacedItemsScenario_Title;
        }
        
        public static class SampleSteps
        {
            public static class RefundedItems
            {
                public const string GivenText = "Given That a customer previously bought a black sweater from me";
                public const string AndText = "And I have three black sweaters in stock.";
                public const string WhenText = "When They return the black sweater for a refund";
                public const string ThenText = "Then I should have four black sweaters in stock.";
            }
        }

        public static class SampleEvents
        {
            public const string AdminAttemptsToDefineUsers = "Admin attemps to define users via posting data to url localhost:5050/users";
        }
    }
}
