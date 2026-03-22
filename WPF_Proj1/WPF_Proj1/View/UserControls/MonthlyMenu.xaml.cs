using System.Globalization;
using System.Windows.Controls;

namespace WPF_Proj1.View.UserControls
{
    /// <summary>
    /// Interaction logic for MonthlyMenu.xaml
    /// </summary>
    public partial class MonthlyMenu : UserControl
    {
        public string today2 = DateTime.Now.ToString("yyyy.MMMM", new CultureInfo("en-US"));
        public MonthlyMenu()
        {
            InitializeComponent();
            tDateWoDay.Text = today2;
            using var db = new AppDbContext();
            var userOrder = db.Orders;
        }
    }
}
