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
    /// Interaction logic for MainPageAdmin.xaml
    /// </summary>
    public partial class MainPageAdmin : Window
    {
        private readonly UserControl _overviewAdmin = new OverviewAdmin();
        private readonly UserControl _monthlyMenuAdmin = new MonthlyMenuAdmin();
        private readonly UserControl _finances = new FinancesAdmin();
        public MainPageAdmin()
        {
            InitializeComponent();
            PagesAdmin.SelectedIndex = 0;
            PageAdmin.Content = _overviewAdmin;
        }

        private void PageChangerAdmin(object sender, RoutedEventArgs e)
        {
            int currentPage = PagesAdmin.SelectedIndex;
            switch (currentPage)
            {
                case 0:
                    PageAdmin.Content = _overviewAdmin;
                    break;
                case 1:
                    PageAdmin.Content = _monthlyMenuAdmin;
                    break;

                case 2:
                    PageAdmin.Content = _finances;
                    break;

                case 3:
                    MessageBox.Show("Export");
                    break;
            }
        }
    }
}
