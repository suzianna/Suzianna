using Org.XmlUnit;
using Org.XmlUnit.Builder;
using Org.XmlUnit.Xpath;

namespace Suzianna.Reporting.Tests.Unit.TestUtils
{
    public static class SourceExtensions
    {
        public static string EvaluateXPath(this string targetString, string xpath)
        {
            var source =  Input.FromString(targetString).Build();
            return new XPathEngine().Evaluate(xpath, source);
        }
    }
}