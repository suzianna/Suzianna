using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using NFluent;
using NFluent.ApiChecks;
using Suzianna.Hosting.Core;
using Suzianna.Hosting.NetCoreHosting;
using Suzianna.Hosting.Tests.Integration.Constants;
using Xunit;

namespace Suzianna.Hosting.Tests.Integration
{
    [Collection(TestCollections.NoParallel)]
    public class DotNetCoreHostTests
    {
        public DotNetCoreHostTests()
        {
            var options = new DotNetCoreHostOptions
            {
                CsProjectPath = TestSutUrls.ApiProjectPath,
                Port = Port
            };
            _host = new DotNetCoreHost(options);
            _client = new HttpClient();
        }

        private readonly DotNetCoreHost _host;
        private readonly HttpClient _client;
        private const int Port = 5000;
        private const string HealthCheckEndpoint = "http://localhost:5000/api/healthcheck";

        [Fact]
        public async Task should_start_a_host()
        {
            _host.Start();

            var data = await _client.GetAsync(HealthCheckEndpoint);

            Check.That(data.StatusCode).IsEqualTo(HttpStatusCode.OK);

            _host.Stop();
        }

        [Fact]
        public void should_stop_a_host()
        {
            _host.Start();

            _host.Stop();

            Func<Task<HttpResponseMessage>> getData = () => _client.GetAsync(HealthCheckEndpoint);

            Check.ThatAsyncCode(getData).Throws<HttpRequestException>();
        }

        [Fact]
        public void start_should_kill_previous_opened_processes_and_start_again()
        {
            _host.Start();
            var firstProcessId = ProcessManager.FindProcessIdByPort(Port);

            _host.Start();
            var secondProcessId = ProcessManager.FindProcessIdByPort(Port);

            Check.That(firstProcessId).IsNotEqualTo(secondProcessId);
        }
    }
}