using System.Windows;
using System.Windows.Controls;

namespace WPF_Proj1.View.UserControls
{
    /// <summary>
    /// Interaction logic for OverviewAdmin.xaml
    /// </summary>
    public partial class OverviewAdmin : UserControl
    {
        public OverviewAdmin()
        {
            InitializeComponent();
            using var db = new AppDbContext();
            userNum.Text = Convert.ToString(db.Users.Count()-1);
            orderCount.Text = Convert.ToString(db.Orders.Where(o => o.WantsSoup).Count() + db.Orders.Where(o => o.DishChoice != null).Count());
            monthly.Text = "0"; //Convert.ToString(db.Orders.Where(o => o.WantsSoup).Count() + db.Orders.Where(o => o.DishChoice != null).Count());
            expectable.Text = Convert.ToString(db.Orders.Where(o => o.WantsSoup).Count() * 300 + db.Orders.Where(o => o.DishChoice == "A").Count() * 500 + db.Orders.Where(o => o.DishChoice == "B").Count() * 700);
        }
    }
}
