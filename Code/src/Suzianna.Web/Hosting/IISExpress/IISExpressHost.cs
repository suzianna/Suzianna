using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Suzianna.Core;

namespace Suzianna.Web.Hosting.IISExpress
{
    public class IISExpressHost : IStartableHost
    {
        private const string IIS_EXPRESS = @"C:\Program Files (x86)\IIS Express\iisexpress.exe";
        private const string READY_MSG = @"Enter 'Q' to stop IIS Express";

        private readonly ProcessStartInfo startInfo;

        private Process process;

        public IISExpressHost(string path, int port, string iisExePath = null)
        {
            if (!Directory.Exists(path))
            {
                throw new DirectoryNotFoundException();
            }

            if (ushort.MinValue > port || ushort.MaxValue < port)
            {
                throw new ArgumentOutOfRangeException(nameof(port));
            }

            path = path.TrimEnd('\\').SurroundByDoubleQoutes();
            Port = port;

            if (string.IsNullOrEmpty(iisExePath))
                iisExePath = IIS_EXPRESS;

            startInfo = new ProcessStartInfo
            {
                FileName = iisExePath,
                Arguments = $"/path:{path} /port:{port} /trace:error",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false
            };
        }

        public int Port { get; }
        public string Url => $"http://localhost:{Port}";

        public Task Start()
        {
            return Start(default(CancellationToken));
        }

        private Task Start(CancellationToken cancellationToken = default(CancellationToken))
        {
            var tcs = new TaskCompletionSource<object>();

            if (cancellationToken.IsCancellationRequested)
            {
                tcs.SetCanceled();
                //Logger.Write($"IIS express cancelled at first of method.");
                return tcs.Task;
            }

            try
            {
                var proc = new Process { EnableRaisingEvents = true, StartInfo = startInfo };

                DataReceivedEventHandler onOutput = null;
                onOutput =
                    (sender, e) =>
                    {
                        if (cancellationToken.IsCancellationRequested)
                        {
                            tcs.TrySetCanceled();
                            //Logger.Write($"IIS express cancelled at first of output method.");
                        }

                        try
                        {
                            //Logger.Write($"Output: \t{e.Data}");

                            if (string.Equals(READY_MSG, e.Data, StringComparison.OrdinalIgnoreCase))
                            {
                                //Logger.Write($"IIS express Ready message recieved !");
                                proc.OutputDataReceived -= onOutput;
                                process = proc;
                                tcs.TrySetResult(null);
                            }
                        }
                        catch (Exception ex)
                        {
                            //Logger.Write($"IIS express exception occured ! {ex.ExtractFullMessage()}");
                            tcs.TrySetException(ex);
                            proc.Dispose();
                        }
                    };

                proc.OutputDataReceived += onOutput;
                //proc.ErrorDataReceived += (sender, e) => Logger.Write($"Error Data Recieved : \t{e.Data}");
                //proc.Exited += (sender, e) => Logger.Write("IIS Express exited.");

                //Logger.Write($"Going to start iis express");
                proc.Start();
                //Logger.Write($"IIS express started !");
                proc.BeginOutputReadLine();
                proc.BeginErrorReadLine();
            }
            catch (Exception ex)
            {
                //Logger.Write($"IIS express exception occured (end of method) ! {ex.ExtractFullMessage()}");
                tcs.TrySetException(ex);
            }

            return tcs.Task;
        }
        public Task Stop()
        {
            var tcs = new TaskCompletionSource<object>(null);
            try
            {
                process.Exited += (sender, e) => tcs.TrySetResult(null);

                SendStopMessageToProcess(process.Id);
            }
            catch (Exception ex)
            {
                tcs.TrySetException(ex);
            }

            return tcs.Task;
        }
        public void Quit()
        {
            Process proc;
            if ((proc = Interlocked.Exchange(ref process, null)) != null)
            {
                proc.Kill();
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Quit();
            }
        }
        private static void SendStopMessageToProcess(int pid)
        {
            try
            {
                for (var ptr = NativeMethods.GetTopWindow(IntPtr.Zero); ptr != IntPtr.Zero; ptr = NativeMethods.GetWindow(ptr, 2))
                {
                    uint num;
                    NativeMethods.GetWindowThreadProcessId(ptr, out num);
                    if (pid == num)
                    {
                        var hWnd = new HandleRef(null, ptr);
                        NativeMethods.PostMessage(hWnd, 0x12, IntPtr.Zero, IntPtr.Zero);
                        return;
                    }
                }
            }
            catch (ArgumentException)
            {
            }
        }
    }
}