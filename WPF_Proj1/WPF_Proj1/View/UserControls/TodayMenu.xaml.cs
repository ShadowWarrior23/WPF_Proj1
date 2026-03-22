using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace WPF_Proj1.View.UserControls
{
    public partial class TodayMenu : UserControl
    {
        public string today => $"Today's date:\n{DateTime.Now.ToString("yyyy.MM.dd. (dddd)", new CultureInfo("en-US"))}";
        public string todayDate = DateTime.Now.ToString("yyyy.MM.dd.");
        public DateTime todayDate2 = DateTime.Now;

        public TodayMenu(int uId)
        {
            InitializeComponent();
            tDate.Text = today;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool? soup = SoupCb.IsChecked;
            string dish = None.IsChecked == true ? "None" : A.IsChecked == true ? "A" : "B";
            MessageBox.Show($"{todayDate}\nSoup: {Convert.ToString(soup == true ? "Yes" : "No")}; Dish: {dish}");
            using var db = new AppDbContext();
            // read orders
            // update orders
            db.SaveChanges();

        }
    }
}
