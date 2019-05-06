using System;
using System.Collections.Generic;
using System.Text;
using Suzianna.Hosting.Core;
using Suzianna.Hosting.NetCoreHosting;
using Suzianna.Rest.Tests.Integration.Constants;
using Xunit;

namespace Suzianna.Rest.Tests.Integration.Collections
{
    public class HostFixture : IDisposable
    {
        public IStartableHost Host { get; }
        public HostFixture()
        {
            this.Host = new DotNetCoreHost(new DotNetCoreHostOptions()
            {
                CsProjectPath = TestSutUrls.ApiProjectPath,
                Port = 5000
            });
            Host.Start();
        }

        public void Dispose()
        {
            Host.Stop();
        }
    }
}
