using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using WPF_Proj1.View.UserControls;

namespace WPF_Proj1.View
{
    enum WebLaunchState
    {
        Initializing,
        CheckingFrontend,
        CheckingEnvironment,
        CheckingDependencies,
        InstallingDependencies,
        CheckingServer,
        StartingServer,
        WaitingForServer,
        OpeningBrowser,
        Completed,
        Error
    }
    public partial class MainPageAdmin : Window
    {
        private readonly UserControl _overviewAdmin0 = new OverviewAdmin();

        private Process? _viteProcess;
        private readonly string _frontendPath;
        private const string DevUrl = "http://127.0.0.1:5173";
        private readonly WebLauncher _webLauncher;

        public MainPageAdmin()
        {
            InitializeComponent();
            PagesAdmin.SelectedIndex = 0;
            PageAdmin.Content = _overviewAdmin0;

            // School_30 direct path
            //_frontendPath = @"C:\Users\User\Documents\SzB 12.B\GUI CS\WPF_Proj1\WPF_Proj1\WPF_Proj1\Web";
            _frontendPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Web"));
            _webLauncher = new WebLauncher(_frontendPath);
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

        /*private void Status(string text)
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
            await RunCommandAndWait("cmd.exe", "/c pnpm -v", _frontendPath);
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
                FileName = "pnpm.cmd",
                Arguments = "dev --host 127.0.0.1 --port 5173",
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

        private string GetMessage(WebLaunchState state)
        {
            return state switch
            {
                WebLaunchState.Initializing => "Painting...",
                WebLaunchState.CheckingFrontend => "Checking for staff...",
                WebLaunchState.CheckingEnvironment => "A quick check on the admin...",
                WebLaunchState.CheckingDependencies => "Looking at menus...",
                WebLaunchState.InstallingDependencies => "Carrying menus...",
                WebLaunchState.CheckingServer => "Looking outside...",
                WebLaunchState.StartingServer => "Opening the door...",
                WebLaunchState.WaitingForServer => "Waiting...",
                WebLaunchState.OpeningBrowser => "Almost there!",
                WebLaunchState.Completed => "Fired up!",
                WebLaunchState.Error => "Something went wrong.",
                _ => "Working..."
            };
        }

        private void SetState(WebLaunchState state)
        {
            Status(GetMessage(state));
        }

        private async void OpenWeb(object sender, RoutedEventArgs e)
        {
            try
            {
                SetState(WebLaunchState.Initializing);

                SetState(WebLaunchState.CheckingFrontend);
                EnsureFrontendFolderExists();

                SetState(WebLaunchState.CheckingEnvironment);
                await EnsureNodeAvailable();

                SetState(WebLaunchState.CheckingDependencies);
                if (!NodeModulesExist())
                {
                    SetState(WebLaunchState.InstallingDependencies);
                    await RunCommandAndWait("npm.cmd", "install", _frontendPath);
                }

                SetState(WebLaunchState.CheckingServer);
                if (!await IsServerRunning(DevUrl))
                {
                    SetState(WebLaunchState.StartingServer);
                    StartViteServer();

                    SetState(WebLaunchState.WaitingForServer);
                    await WaitForServer(DevUrl, 20000);
                }

                SetState(WebLaunchState.OpeningBrowser);
                OpenBrowser(DevUrl);

                SetState(WebLaunchState.Completed);
            }
            catch (Exception ex)
            {
                SetState(WebLaunchState.Error);

                MessageBox.Show(
                    ex.ToString(),
                    "OpenWeb error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }*/

        private async void OpenWeb(object sender, RoutedEventArgs e)
        {
            try
            {
                await _webLauncher.OpenAsync(Status);
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

        private void Status(string text)
        {
            Dispatcher.Invoke(() =>
            {
                StatusText.Text = text;
            });
        }

        protected override void OnClosed(EventArgs e)
        {
            _webLauncher.Stop();
            base.OnClosed(e);
        }
    }
}