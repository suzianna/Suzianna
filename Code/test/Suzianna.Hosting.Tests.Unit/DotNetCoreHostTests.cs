using NFluent;
using Suzianna.Hosting.NetCoreHosting;
using Xunit;

namespace Suzianna.Hosting.Tests.Unit
{
    public class DotNetCoreHostTests
    {
        [Fact]
        public void should_set_baseUrl_based_on_given_port()
        {
            const int port = 5000;
            const string expectedBaseUrl = "http://localhost:5000";
            var options = new DotNetCoreHostOptions {Port = port};

            var host = new DotNetCoreHost(options);

            Check.That(host.BaseUrl).IsEqualTo(expectedBaseUrl);
        }
    }
}