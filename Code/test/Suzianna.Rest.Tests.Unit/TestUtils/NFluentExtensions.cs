using NFluent;
using NFluent.Extensibility;

namespace Suzianna.Rest.Tests.Unit.TestUtils
{
    public static class NFluentExtensions
    {
        public static ICheckLink<ICheck<T>> HasPropertiesWithSameValues<T, TU>(this ICheck<T> check, TU expected)
        {
            check.Considering().All.Properties.IgnoreExtra.IsEqualTo(expected);
            return ExtensibilityHelper.BuildCheckLink(check);
        }
    }
}
