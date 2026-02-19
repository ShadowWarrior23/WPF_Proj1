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
using WPF_Proj1.View.UserControls.UserControls1;
using WPF_Proj1.View.UserControls.UserControls1.UserControls11;

namespace WPF_Proj1.View.UserControls
{
    public partial class MonthlyMenuAdmin : UserControl
    {
        public MonthlyMenuAdmin()
        {
            InitializeComponent();
            
        }

        /*private void DayInMonthAdmin_EditMenuRequested(object sender, RoutedEventArgs e)
        {
            if (e is not EditMenuRequestedEventArgs args)
                return;

            var edit = new EditMenu();

            // Pass day/menu info (pick one pattern)
            // edit.Day = args.Day;
            // or: edit.DataContext = args.Day;
            // or: edit.Load(args.Day);

            pu.Content = edit;
            pu.Visibility = Visibility.Visible;

            e.Handled = true;
        }*/
    }
}
