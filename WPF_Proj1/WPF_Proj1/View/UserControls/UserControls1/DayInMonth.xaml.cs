using System.Windows.Controls;


namespace WPF_Proj1.View.UserControls.UserControls1
{
    public partial class DayInMonth : UserControl
    {
        //public string today => $"Today's date:\n{DateTime.Now.ToString("dd. (dddd)", new CultureInfo("en-US"))}";
        public int userId;
        public string today;
        public DateOnly todayDO;
        public DayInMonth(int uId, DateOnly day)
        {
            InitializeComponent();
            userId = uId;
            today = day.ToString();
            tDay.Text = today;
            todayDO = day;
            /*MessageBox.Show(tDay.Text);
            MessageBox.Show(today);*/
        }

        private void diffDish(object sender, SelectionChangedEventArgs e)
        {
            using var db = new AppDbContext();
            var order = db.Orders.FirstOrDefault(o => o.UserId == userId && o.Day == todayDO);

            if (order != null)
            {
                order.DishChoice = cb.SelectedItem == dishA ? "A" : cb.SelectedItem == dishB ? "B" : null;
                db.SaveChanges();
            }

        }

        private void changeCb(object sender, System.Windows.RoutedEventArgs e)
        {
            using var db = new AppDbContext();
            var order = db.Orders.FirstOrDefault(o => o.UserId == userId && o.Day == todayDO);
            Boolean ch = soupCb.IsChecked == true ? true : false;
            order.WantsSoup = ch;
            db.SaveChanges();
        }
    }
}
