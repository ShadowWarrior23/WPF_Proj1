using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using WPF_Proj1.View.UserControls;

namespace WPF_Proj1.View
{
    /// <summary>
    /// Interaction logic for MainPageAdmin.xaml
    /// </summary>
    public partial class MainPageAdmin : Window
    {
        private readonly UserControl _overviewAdmin0 = new OverviewAdmin();

        private Process? _viteProcess;
        private readonly string _frontendPath;
        private const string DevUrl = "http://127.0.0.1:5173";

        public MainPageAdmin()
        {
            InitializeComponent();
            PagesAdmin.SelectedIndex = 0;
            PageAdmin.Content = _overviewAdmin0;

            // TEMP direct path
            _frontendPath = @"C:\Users\User\Documents\SzB 12.B\GUI CS\WPF_Proj1\WPF_Proj1\WPF_Proj1\Web";
        }

        private void PageChangerAdmin(object sender, RoutedEventArgs e)
        {
            int currentPage = PagesAdmin.SelectedIndex;

            switch (currentPage)
            {
                case 0:
                    PageAdmin.Content = new OverviewAdmin();
                    break;

                case 1:
                    PageAdmin.Content = new MonthlyMenuAdmin();
                    break;

                case 2:
                    PageAdmin.Content = new FinancesAdmin();
                    break;

                case 3:
                    PageAdmin.Content = new ProfilesAdmin();
                    break;

                case 4:
                    MessageBox.Show("Export");
                    break;
            }
        }

        private void OpenConsole(object sender, RoutedEventArgs e)
        {
        }

        private void Status(string text)
        {
            StatusText.Text = text;
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

        protected override void OnClosed(EventArgs e)
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

            base.OnClosed(e);
        }

        private async void OpenWeb(object sender, RoutedEventArgs e)
        {
            try
            {
                Status("Checking frontend folder...");
                EnsureFrontendFolderExists();

                Status("Checking Node/npm...");
                await EnsureNodeAvailable();

                Status("Checking dependencies...");
                if (!NodeModulesExist())
                {
                    Status("Installing dependencies...");
                    await RunCommandAndWait("npm.cmd", "ci", _frontendPath);
                }

                Status("Checking server...");
                if (!await IsServerRunning(DevUrl))
                {
                    Status("Starting Vite server...");
                    StartViteServer();
                    await WaitForServer(DevUrl, 20000);
                }

                Status("Opening browser...");
                OpenBrowser(DevUrl);

                Status("Done.");
            }
            catch (Exception ex)
            {
                Status("Failed.");
                MessageBox.Show(
                    ex.ToString(),
                    "OpenWeb error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
    }
}