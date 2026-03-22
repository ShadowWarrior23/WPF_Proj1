using System.Windows;
using System.Windows.Controls;
using WPF_Proj1.View.UserControls;

namespace WPF_Proj1.View
{
    public partial class MainPage : Window
    {
        public static int uId;
        private readonly UserControl _overview = new Overview(uId);
        public MainPage(string username)
        {
            InitializeComponent();
            Pages.SelectedIndex = 0;
            Page.Content = _overview;
            using var db = new AppDbContext();
            uId = db.Users.Where(u => u.Username == username).Select(u => u.Id).FirstOrDefault();
        }

        private void PageChanger(object sender, RoutedEventArgs e)
        {
            int currentPage = Pages.SelectedIndex;
            switch (currentPage)
            {
                case 0:
                    UserControl _overview = new Overview(uId);
                    Page.Content = _overview;
                    break;
                case 1:
                    UserControl _todayMenu = new TodayMenu();
                    Page.Content = _todayMenu;
                    break;

                case 2:
                    UserControl _monthlyMenu = new MonthlyMenu();
                    Page.Content = _monthlyMenu;
                    break;

                case 3:
                    MessageBox.Show("Export");

                    break;
            }
        }
    }
}