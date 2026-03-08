using System.Configuration;
using System.Data;
using System.Windows;
using WPF_Proj1.Data.Models;

namespace WPF_Proj1
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            DbInitialiser.Initialize();
        }
    }

}
