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
using System.Windows.Shapes;
using WPF_Proj1.View.UserControls;

namespace WPF_Proj1.View
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Window
    {
        private readonly UserControl _overview = new Overview();
        private readonly UserControl _todayMenu = new TodayMenu();
        private readonly UserControl _monthlyMenu = new MonthlyMenu();
        public MainPage()
        {
            InitializeComponent();
            Pages.SelectedIndex = 0;
            Page.Content = _overview;
        }

        private void PageChanger(object sender, RoutedEventArgs e)
        {
            int currentPage = Pages.SelectedIndex;
            switch (currentPage)
            {
                case 0:
                    Page.Content = _overview;
                    break;
                case 1:
                    Page.Content = _todayMenu;
                    break;

                case 2:
                    Page.Content = _monthlyMenu;
                    break;

                case 3:
                    MessageBox.Show("Export");
                    break;
            }
        }
    }
}