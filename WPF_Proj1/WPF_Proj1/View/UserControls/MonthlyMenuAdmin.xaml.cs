using System.Windows.Controls;
using WPF_Proj1.View.UserControls.UserControls1;

namespace WPF_Proj1.View.UserControls
{
    public partial class MonthlyMenuAdmin : UserControl
    {
        public MonthlyMenuAdmin()
        {
            InitializeComponent();
            using var db = new AppDbContext();
            var dailyMenu = db.DailyMenus;
            foreach (var d in dailyMenu)
            {
                DayInMonthAdmin _dayInMonthAdmin = new DayInMonthAdmin(d.Day.ToString(), d.Soup, d.DishA, d.DishB);
                _dayInMonthAdmin.tDay.Text = d.Day.ToString();
                _dayInMonthAdmin.Soup.Text = d.Soup;
                _dayInMonthAdmin.MenuA.Text = d.DishA;
                _dayInMonthAdmin.MenuB.Text = d.DishB;
                _dayInMonthAdmin.Width = 900;
                _dayInMonthAdmin.Height = 50;
                days.Items.Add(_dayInMonthAdmin);
            }
        }
    }
}
