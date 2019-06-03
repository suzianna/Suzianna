using Org.XmlUnit;
using Org.XmlUnit.Xpath;

namespace Suzianna.Reporting.Tests.Unit.TestUtils
{
    public static class SourceExtensions
    {
        public static string EvaluateXPath(this ISource source, string xpath)
        {
            return new XPathEngine().Evaluate(xpath, source);
        }
    }
}