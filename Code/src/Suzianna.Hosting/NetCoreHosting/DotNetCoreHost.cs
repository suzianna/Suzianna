using System;
using System.Diagnostics;
using System.Threading;
using Suzianna.Hosting.Core;

namespace Suzianna.Hosting.NetCoreHosting
{
    public class DotNetCoreHost : IStartableHost
    {
        private readonly DotNetCoreHostOptions _options;
        private AutoResetEvent _resetEvent = new AutoResetEvent(false);
        public DotNetCoreHost(DotNetCoreHostOptions options)
        {
            _options = options;
        }

        public string BaseUrl => $"http://localhost:{_options.Port}";

        public void Start()
        {
            ProcessManager.KillByPort(this._options.Port);
            var startInfo = new ProcessStartInfo("dotnet")
            {
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                Arguments = $"run --project \"{_options.CsProjectPath}\"",
            };
            var process = Process.Start(startInfo);

            process.ErrorDataReceived += ProcessOnErrorDataReceived;
            process.OutputDataReceived += ProcessOnOutputDataReceived;

            process.BeginErrorReadLine();
            process.BeginOutputReadLine();

            _resetEvent.WaitOne();
        }

        private void ProcessOnOutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null && e.Data.Contains("Now listening on", StringComparison.OrdinalIgnoreCase))
                _resetEvent.Set();
        }
        private void ProcessOnErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null && !string.IsNullOrEmpty(e.Data))
                throw  new Exception(e.Data);
        }

        public void Stop()
        {
            ProcessManager.KillByPort(this._options.Port);
        }
    }
}