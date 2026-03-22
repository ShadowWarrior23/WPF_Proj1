using System.Windows;
using System.Windows.Controls;
using WPF_Proj1.View.UserControls;

namespace WPF_Proj1.View
{
    public partial class MainPage : Window
    {
        public int uId;
        public DateOnly nextEligibleDay;
        public MainPage(string username)
        {
            InitializeComponent();
            using var db = new AppDbContext();
            uId = db.Users.Where(u => u.Username == username).Select(u => u.Id).FirstOrDefault();
            Pages.SelectedIndex = 0;
            UserControl _overview = new Overview(uId);
            Page.Content = _overview;
        }

        private void PageChanger(object sender, RoutedEventArgs e)
        {
            using var db = new AppDbContext();
            int currentPage = Pages.SelectedIndex;
            switch (currentPage)
            {
                case 0:
                    UserControl _overview = new Overview(uId);
                    Page.Content = _overview;
                    break;
                case 1:
                    List<DateOnly> weekdays = db.DailyMenus.Select(d => d.Day).OrderBy(d => d).ToList();

                    DateOnly today = DateOnly.FromDateTime(DateTime.Today);

                    DateOnly elDate = weekdays.Any(d => d >= today) ? weekdays.First(d => d >= today) : weekdays.First();
                    nextEligibleDay = elDate;
                    UserControl _todayMenu = new TodayMenu(uId, nextEligibleDay);
                    Page.Content = _todayMenu;
                    break;

                case 2:
                    UserControl _monthlyMenu = new MonthlyMenu(uId);
                    Page.Content = _monthlyMenu;
                    break;

                case 3:
                    MessageBox.Show("Export");

                    break;
            }
        }
    }
}