using System.Windows;
using System.Windows.Controls;

namespace WPF_Proj1.View.UserControls.UserControls1
{
    public partial class Profile : UserControl
    {
        public event EventHandler? RemoveRequested;

        public string UserNameValue { get; set; } = "";

        public Profile()
        {
            InitializeComponent();
        }

        private void RemUser(object sender, System.Windows.RoutedEventArgs e)
        {
            RemoveRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}
