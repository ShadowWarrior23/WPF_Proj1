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
using System.Windows.Shapes;

namespace WPF_Proj1.View.UserControls.UserControls1.UserControls11
{
    public partial class EditMenu : Window
    {
        public EditMenu(string day, string soup, string dishA, string dishB)
        {
            InitializeComponent();
            tDate.Text = day;
            tSoup.Text = soup;
            tA.Text = dishA;
            tB.Text = dishB;
        }

        private void SaveChanges(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Saved");
            using var db = new AppDbContext();
            var newDayMenu = new DailyMenu
            {
                Day = DateOnly.Parse(tDate.Text),
                Soup = tSoup.Text,
                DishA = tA.Text,
                DishB = tB.Text
            };
            db.DailyMenus.Update(newDayMenu);
            db.SaveChanges();
            this.Close();
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
