using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using WPF_Proj1.View.UserControls.UserControls1.UserControls11;

namespace WPF_Proj1.View.UserControls.UserControls1
{
    public partial class DayInMonthAdmin : UserControl
    {
        public DayInMonthAdmin()
        {
            InitializeComponent();
            string d = $"{DateTime.Now.ToString("yyyy.MM.dd.")}";
            tDay.Text = d;
        }

        private void Popup(object sender, RoutedEventArgs e)
        {
            EditMenu _editMenu = new EditMenu();
            _editMenu.Show();
        }
    }
}
