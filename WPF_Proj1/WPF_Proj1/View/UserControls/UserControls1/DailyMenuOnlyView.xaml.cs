using System.Globalization;
using System.Windows.Controls;

namespace WPF_Proj1.View.UserControls.UserControls1
{
    public partial class DailyMenuOnlyView : UserControl
    {
        /*public string today => $"Next day with order:\n{DateTime.Now.ToString("yyyy.MM.dd. (dddd)", new CultureInfo("en-US"))}";
        public string todayDate = DateTime.Now.ToString("yyyy.MM.dd.");
        public DateTime todayDate2 = DateTime.Now;*/
        public DateOnly nextEligibleDay;

        public DailyMenuOnlyView(int uId)
        {
            InitializeComponent();
            using var db = new AppDbContext();
            List<int> weekdays = db.DailyMenus.Select(d => d.Day.Day).ToList();
            int[] yearAndMonth = new int[] { db.DailyMenus.ToList().Last().Day.Year, db.DailyMenus.ToList().Last().Day.Month };
            int elDay = 1;//weekdays.Contains(new DateOnly().Day) ? new DateOnly().Day : weekdays.Contains(new DateOnly().Day + 1) ? new DateOnly().Day + 1 : new DateOnly().Day + 2;
            nextEligibleDay = new DateOnly(yearAndMonth[0], yearAndMonth[1], elDay);

            tDate.Text = $"Next eligible day: {nextEligibleDay}";
            SoupCb.Content = db.DailyMenus.Where(m => m.Day == nextEligibleDay).Select(m => m.Soup);
            SoupCb.IsChecked = db.Orders.Where(o => o.UserId == uId && o.Day == nextEligibleDay).Select(o => o.WantsSoup).FirstOrDefault();
            string chDish = db.Orders.Where(o => o.UserId == uId && o.Day == nextEligibleDay).Select(o => o.DishChoice).FirstOrDefault() == "N" ? "None" : db.Orders.Where(o => o.UserId == uId && o.Day == nextEligibleDay).Select(o => o.DishChoice).FirstOrDefault() == "A" ? db.DailyMenus.Where(m => m.Day == nextEligibleDay).FirstOrDefault().DishA : db.DailyMenus.Where(m => m.Day == nextEligibleDay).FirstOrDefault().DishB;
            chosenDish.Text = $"Chosen dish: {chDish}";
        }
    }
}
