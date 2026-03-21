using System.Windows;
using System.Windows.Controls;
using WPF_Proj1.View.UserControls;

namespace WPF_Proj1.View
{
    /// <summary>
    /// Interaction logic for MainPageAdmin.xaml
    /// </summary>
    public partial class MainPageAdmin : Window
    {
        private readonly UserControl _overviewAdmin0 = new OverviewAdmin();
        public MainPageAdmin()
        {
            InitializeComponent();
            PagesAdmin.SelectedIndex = 0;
            PageAdmin.Content = _overviewAdmin0;
        }

        private void PageChangerAdmin(object sender, RoutedEventArgs e)
        {
            int currentPage = PagesAdmin.SelectedIndex;
            switch (currentPage)
            {
                case 0:
                    UserControl _overviewAdmin = new OverviewAdmin();
                    PageAdmin.Content = _overviewAdmin;
                    break;
                case 1:
                    UserControl _monthlyMenuAdmin = new MonthlyMenuAdmin();
                    PageAdmin.Content = _monthlyMenuAdmin;
                    break;

                case 2:
                    UserControl _finances = new FinancesAdmin();
                    PageAdmin.Content = _finances;
                    break;

                case 3:
                    UserControl _profiles = new ProfilesAdmin();
                    PageAdmin.Content = _profiles;
                    break;
                case 4:
                    MessageBox.Show("Export");
                    break;
            }
        }
    }
}
