using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            orderCount.Text = Convert.ToString(db.Orders.Where(o => o.WantsSoup).Count() + db.Orders.Where(o => o.DishChoice != "n").Count());
            monthly.Text = "0"; //Convert.ToString(db.Orders.Where(o => o.WantsSoup).Count() + db.Orders.Where(o => o.DishChoice != "n").Count());
            expectable.Text = Convert.ToString(db.Orders.Where(o => o.WantsSoup).Count() * 300 + db.Orders.Where(o => o.DishChoice == "a").Count() * 500 + db.Orders.Where(o => o.DishChoice == "a").Count() * 700);
        }
    }
}
