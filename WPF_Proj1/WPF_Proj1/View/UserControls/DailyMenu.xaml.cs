using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
    /// Interaction logic for DailyMenu.xaml
    /// </summary>
    public partial class DailyMenu : UserControl
    {
        public string today =>
            $"Today's date:\n{DateTime.Now.ToString("yyyy.MM.dd. (dddd)", new CultureInfo("en-US"))}";
        public string todayDate = DateTime.Now.ToString("yyyy.MM.dd.");
        public DateTime todayDate2 = DateTime.Now;

        public DailyMenu()
        {
            InitializeComponent();
            tDate.Text = today;

            using var db = new AppDbContext();

            Soup.Text = db.DailyMenus.Where(x => x.Day == todayDate2).Select(x => x.Soup);

            var today0 = db.DailyMenus
                          .FirstOrDefault(m => m.Day == new DateTime(2025, 2, 5));

            /*if (today0 != null)
            {
                Console.WriteLine($"Soup: {today0.Soup}");
                Console.WriteLine($"A: {today0.DishA}");
                Console.WriteLine($"B: {today0.DishB}");
            }*/

            Console.WriteLine("\nAll orders:");
            var orders = db.Orders.ToList();

            //Works, only for convenience comment
            /*foreach (var o in orders)
            {
                MessageBox.Show(
                    $"User {o.UserId} ordered {o.DishChoice} on {o.Day}");
            }*/
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool? soup = SoupCb.IsChecked;
            string dish = None.IsChecked == true ? "None" : A.IsChecked == true ? "A" : "B";
            MessageBox.Show($"{todayDate}\nSoup: {Convert.ToString(soup == true ? "Yes": "No")}; Dish: {dish}");
        }
    }
}