using System;
using System.Collections.Generic;
using System.Text;
using Suzianna.Rest.Tests.Integration.Constants;
using Xunit;

namespace Suzianna.Rest.Tests.Integration.Collections
{
    [CollectionDefinition(TestCollections.NeedsHost)]
    public class HostCollection : ICollectionFixture<HostFixture>
    {
    }
}
