using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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

namespace WPF_Proj1.View.UserControls.UserControls1
{
    /// <summary>
    /// Interaction logic for DailyMenuOnlyView.xaml
    /// </summary>
    public partial class DailyMenuOnlyView : UserControl
    {
        public string today => $"Today's date:\n{DateTime.Now.ToString("yyyy.MM.dd. (dddd)", new CultureInfo("en-US"))}";
        public string todayDate = DateTime.Now.ToString("yyyy.MM.dd.");
        public DateTime todayDate2 = DateTime.Now;

        public DailyMenuOnlyView()
        {
            InitializeComponent();
            tDate.Text = today;


        }
    }
}
