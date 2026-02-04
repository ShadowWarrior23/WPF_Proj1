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

namespace WPF_Proj1.View.UserControls
{
    /// <summary>
    /// Interaction logic for DailyMenu.xaml
    /// </summary>
    public partial class DailyMenu : UserControl
    {
        public string TodayDate =>
    $"Today's date: \n{DateTime.Now:yyyy.MM.dd. (dddd)}";

        public DailyMenu()
        {
            InitializeComponent();
        }
    }
}
