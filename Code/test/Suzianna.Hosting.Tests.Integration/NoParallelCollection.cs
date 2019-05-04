using System;
using System.Collections.Generic;
using System.Text;
using Suzianna.Hosting.Tests.Integration.Constants;
using Xunit;

namespace Suzianna.Hosting.Tests.Integration
{
    [CollectionDefinition(TestCollections.NoParallel, DisableParallelization = true)]
    public class NoParallelCollection
    {
    }
}
