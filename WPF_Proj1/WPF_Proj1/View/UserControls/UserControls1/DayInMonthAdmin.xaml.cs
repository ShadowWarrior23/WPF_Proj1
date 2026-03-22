using System.Windows;
using System.Windows.Controls;
using WPF_Proj1.View.UserControls.UserControls1.UserControls11;

namespace WPF_Proj1.View.UserControls.UserControls1
{
    public partial class DayInMonthAdmin : UserControl
    {
        public string localDay;
        public string localSoup;
        public string localA;
        public string localB;
        public DayInMonthAdmin(string day, string soup, string dishA, string dishB)
        {
            InitializeComponent();
            /*using var db = new AppDbContext();
            var dailyMenu = db.Orders;*/
            localDay = day;
            localSoup = soup;
            localA = dishA;
            localB = dishB;
        }

        private void Popup(object sender, RoutedEventArgs e)
        {
            EditMenu _editMenu = new EditMenu(localDay, localSoup, localA, localB);
            _editMenu.Show();
        }
    }
}
