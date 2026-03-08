using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF_Proj1.View.UserControls.UserControls1.UserControls11;

namespace WPF_Proj1.View.UserControls.UserControls1
{
    public partial class DayInMonthAdmin : UserControl
    {
        public DayInMonthAdmin()
        {
            InitializeComponent();
        }

        private void Popup(object sender, RoutedEventArgs e)
        {
            EditMenu _editMenu = new EditMenu();
            _editMenu.Show();
        }
    }
}
