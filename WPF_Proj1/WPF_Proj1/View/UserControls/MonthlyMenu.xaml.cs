using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using WPF_Proj1.View.UserControls.UserControls1;

namespace WPF_Proj1.View.UserControls
{
    public partial class MonthlyMenu : UserControl
    {
        public string today2 = DateTime.Now.ToString("yyyy.MMMM", new CultureInfo("en-US"));
        int userId;
        public MonthlyMenu(int uId)
        {
            InitializeComponent();
            using var db = new AppDbContext();
            tDateWoDay.Text = today2;
            userId = uId;

            var userOrder = db.Orders.Where(o => o.UserId == uId);

            foreach (var order in userOrder)
            {
                var d = db.DailyMenus.Where(m => m.Day == order.Day);
                DayInMonth dim = new DayInMonth(uId, order.Day);
                dim.tDay.Text = order.Day.ToString();
                dim.soup.Text = d.Select(m => m.Soup).FirstOrDefault();
                dim.soupCb.IsChecked = order.WantsSoup;
                dim.dishA.Content = d.Select(m => m.DishA).FirstOrDefault();
                dim.dishB.Content = d.Select(m => m.DishB).FirstOrDefault();
                dim.cb.SelectedItem = order.DishChoice == "A" ? dim.dishA : order.DishChoice == "B" ? dim.dishB : dim.noneC;
                dim.Width = 900;
                dim.Height = 50;
                menus.Items.Add(dim);
            }
        }
    }
}