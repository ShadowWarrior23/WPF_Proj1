using System.Windows.Controls;
using WPF_Proj1.View.UserControls.UserControls1;

namespace WPF_Proj1.View.UserControls
{
    public partial class Overview : UserControl
    {
        public Overview(int uId)
        {
            InitializeComponent();
            using var db = new AppDbContext();
            var u = db.Orders.Where(u => u.UserId == uId);

            DailyMenuOnlyView _dmow = new DailyMenuOnlyView(uId);
            daysNum.Text = Convert.ToString(u.Where(d => d.WantsSoup || d.DishChoice == "A" || d.DishChoice == "B").Count());
            soupAmount.Text = Convert.ToString(u.Where(d => d.WantsSoup).Count());
            ABNone.Text = $"${u.Where(d => d.DishChoice == "A").Count()}/${u.Where(d => d.DishChoice == "B").Count()}/${u.Where(d => d.DishChoice == "N").Count()}";
            expPrice.Text = Convert.ToString(u.Where(d => d.WantsSoup).Count() * 300 + u.Where(d => d.DishChoice == "A").Count() * 500 + u.Where(d => d.DishChoice == "B").Count() * 700);

        }
    }
}
