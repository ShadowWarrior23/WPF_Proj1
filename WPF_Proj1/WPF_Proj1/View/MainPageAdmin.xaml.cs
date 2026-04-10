using System.Diagnostics;
using System.IO;
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
            string projectPath = Path.GetFullPath(
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                @"..\..\..\..\PixaF0rk_Console\PixaF0rk_Console.csproj"));

            string exePath = Path.GetFullPath(
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                @"..\..\..\..\PixaF0rk_Console\bin\Debug\net8.0\PixaF0rk_Console.exe"));

            var buildInfo = new ProcessStartInfo
            {
                FileName = "dotnet",
                Arguments = $"build \"{projectPath}\" --nologo -v q",
                UseShellExecute = false,
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            using var build = Process.Start(buildInfo);
            if (build == null)
            {
                MessageBox.Show("Could not start build process.");
                return;
            }

            string stdOut = build.StandardOutput.ReadToEnd();
            string stdErr = build.StandardError.ReadToEnd();
            build.WaitForExit();

            if (build.ExitCode != 0)
            {
                MessageBox.Show("Console project failed to build.\n\n" + stdErr + stdOut);
                return;
            }

            if (!File.Exists(exePath))
            {
                MessageBox.Show("Console EXE not found after build.");
                return;
            }

            Process.Start(new ProcessStartInfo
            {
                FileName = exePath,
                UseShellExecute = true
            });
        }


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