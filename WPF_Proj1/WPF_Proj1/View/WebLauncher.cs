using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Proj1.View
{
    internal class WebLauncher
    {
        private Process? _viteProcess;
        private readonly string _frontendPath;
        private readonly string _devUrl;

        public WebLauncher(string frontendPath, string devUrl = "http://127.0.0.1:5173")
        {
            _frontendPath = frontendPath;
            _devUrl = devUrl;
        }

        public async Task OpenAsync(Action<string>? statusCallback = null)
        {
            void Status(string msg) => statusCallback?.Invoke(msg);

            Status("Painting your page");

            // 1. Check folder
            Status("A little birdwatching...");
            EnsureFrontendFolderExists();

            // 2. Check Node/npm
            Status("Tasting the cheese");
            await EnsureNodeAvailable();

            // 3. Dependencies
            Status("Ingredient-check");
            if (!NodeModulesExist())
            {
                Status("Writing the menus...");
                await RunCommandAndWait("npm.cmd", "install", _frontendPath);
                Status("That's done!");
            }
            else
            {
                Status("They're on the table...");
            }

            // 4. Server check
            Status("Checking in on the admin...");
            if (await IsServerRunning(_devUrl))
            {
                Status("Server already running.");
            }
            else
            {
                Status("Starting playground...");
                StartViteServer();

                Status("Soon to be open... ...");
                await WaitForServer(_devUrl, 20000);

                Status("Look outside!");
            }
            //
            // 5. Open browser
            Status("The doors are opening!");
            OpenBrowser(_devUrl);

            // 6. Done
            Status("Fired it up!");
        }

        public void Stop()
        {
            try
            {
                if (_viteProcess != null && !_viteProcess.HasExited)
                {
                    _viteProcess.Kill(true);
                }
            }
            catch
            {
            }
        }

        private void EnsureFrontendFolderExists()
        {
            if (!Directory.Exists(_frontendPath))
            {
                throw new DirectoryNotFoundException(
                    $"Frontend folder not found:\n{_frontendPath}");
            }
        }

        private bool NodeModulesExist()
        {
            return Directory.Exists(Path.Combine(_frontendPath, "node_modules"));
        }

        private async Task EnsureNodeAvailable()
        {
            await RunCommandAndWait("cmd.exe", "/c node -v", _frontendPath);
            await RunCommandAndWait("cmd.exe", "/c npm -v", _frontendPath);
        }

        private async Task RunCommandAndWait(string fileName, string arguments, string workingDirectory)
        {
            var psi = new ProcessStartInfo
            {
                FileName = fileName,
                Arguments = arguments,
                WorkingDirectory = workingDirectory,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var process = new Process { StartInfo = psi };
            process.Start();

            string stdOut = await process.StandardOutput.ReadToEndAsync();
            string stdErr = await process.StandardError.ReadToEndAsync();

            await process.WaitForExitAsync();

            if (process.ExitCode != 0)
            {
                throw new Exception(
                    $"Command failed:\n{fileName} {arguments}\n\n" +
                    $"Output:\n{stdOut}\n\nErrors:\n{stdErr}");
            }
        }

        private void StartViteServer()
        {
            var psi = new ProcessStartInfo
            {
                FileName = "npm.cmd",
                Arguments = "run dev -- --host 127.0.0.1 --port 5173",
                WorkingDirectory = _frontendPath,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            _viteProcess = Process.Start(psi);
        }

        private async Task<bool> IsServerRunning(string url)
        {
            try
            {
                using var client = new HttpClient
                {
                    Timeout = TimeSpan.FromSeconds(1)
                };

                using var response = await client.GetAsync(url);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        private async Task WaitForServer(string url, int timeoutMs)
        {
            var started = DateTime.UtcNow;

            while ((DateTime.UtcNow - started).TotalMilliseconds < timeoutMs)
            {
                if (await IsServerRunning(url))
                    return;

                await Task.Delay(500);
            }

            throw new TimeoutException("Vite dev server did not start in time.");
        }

        private void OpenBrowser(string url)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }
    }
}
