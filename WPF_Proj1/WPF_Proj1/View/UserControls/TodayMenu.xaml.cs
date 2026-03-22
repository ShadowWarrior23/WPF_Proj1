using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace WPF_Proj1.View.UserControls
{
    public partial class TodayMenu : UserControl
    {
        public int userId;
        public DateOnly todayDO;

        public TodayMenu(int uId, DateOnly day)
        {
            InitializeComponent();
            userId = uId;
            using var db = new AppDbContext();
            List<DateOnly> weekdays = db.DailyMenus.Select(d => d.Day).OrderBy(d => d).ToList();

            DateOnly today = DateOnly.FromDateTime(DateTime.Today);
            DateOnly elDate = weekdays.Any(d => d >= today) ? weekdays.First(d => d >= today) : weekdays.First();
            todayDO = elDate;

            var tOrder = db.Orders.Where(o => o.UserId ==  uId && o.Day == todayDO);
            var tMenu = db.DailyMenus.Where(o => o.Day == todayDO);
            tDate.Text = $"Next day:\n{todayDO}";
            Soup.Text = tMenu.Select(m => m.Soup).FirstOrDefault();
            SoupCb.IsChecked = tOrder.Select(o => o.WantsSoup).FirstOrDefault() == true ? true : false;
            DishA.Text = tMenu.Select(m => m.DishA).FirstOrDefault();
            DishB.Text = tMenu.Select(m => m.DishB).FirstOrDefault();
            NoneC.IsChecked = tOrder.Select(o => o.DishChoice == null).FirstOrDefault() ? true : false;
            AC.IsChecked = tOrder.Select(o => o.DishChoice == "A").FirstOrDefault() ? true : false;
            BC.IsChecked = tOrder.Select(o => o.DishChoice == "B").FirstOrDefault() ? true : false;
        }

        private void SaveDailyMenu(object sender, RoutedEventArgs e)
        {
            bool soupOpt = SoupCb.IsChecked == true ? true : false;
            string dish = NoneC.IsChecked == true ? "None" : AC.IsChecked == true ? "A" : "B";
            MessageBox.Show($"{todayDO}\nSoup: {Convert.ToString(soupOpt == true ? "Yes" : "No")}; Dish: {dish}");
            using var db = new AppDbContext();

            db.Orders.Where(o => o.UserId == userId && o.Day == todayDO).FirstOrDefault().WantsSoup = soupOpt;
            db.Orders.Where(o => o.UserId == userId && o.Day == todayDO).FirstOrDefault().DishChoice = dish;

            db.SaveChanges();

        }
    }
}
