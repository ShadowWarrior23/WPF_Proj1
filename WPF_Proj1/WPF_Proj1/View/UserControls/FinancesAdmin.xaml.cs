using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WPF_Proj1.View.UserControls
{
    public partial class FinancesAdmin : UserControl
    {
        private void DrawSlice(double cx, double cy, double r,
    double startAngle, double sweepAngle, Brush color)
        {
            double startRad = startAngle * Math.PI / 180;
            double endRad = (startAngle + sweepAngle) * Math.PI / 180;

            Point center = new Point(cx, cy);

            Point p1 = new Point(
                cx + r * Math.Cos(startRad),
                cy + r * Math.Sin(startRad));

            Point p2 = new Point(
                cx + r * Math.Cos(endRad),
                cy + r * Math.Sin(endRad));

            bool largeArc = sweepAngle > 180;

            var fig = new PathFigure { StartPoint = center, IsClosed = true };

            fig.Segments.Add(new LineSegment(p1, true));
            fig.Segments.Add(new ArcSegment
            {
                Point = p2,
                Size = new Size(r, r),
                SweepDirection = SweepDirection.Clockwise,
                IsLargeArc = largeArc
            });
            fig.Segments.Add(new LineSegment(center, true));

            var geo = new PathGeometry();
            geo.Figures.Add(fig);

            PieChart.Children.Add(new Path
            {
                Data = geo,
                Fill = color,
                Stroke = (Brush)new BrushConverter().ConvertFromString("#1A111A"),
                StrokeThickness = 2
            });
        }

        private void DrawIncomeChart(int soup, int dishA, int dishB)
        {
            PieChart.Children.Clear();

            double total = soup + dishA + dishB;
            if (total == 0) return;

            double start = -90;

            double soupAngle = soup / total * 360;
            double aAngle = dishA / total * 360;
            double bAngle = dishB / total * 360;

            Brush soupCol = (Brush)new BrushConverter().ConvertFromString("#00ADB5");
            Brush aCol = (Brush)new BrushConverter().ConvertFromString("#D48166");
            Brush bCol = (Brush)new BrushConverter().ConvertFromString("#8DAA91");

            DrawSlice(150, 150, 100, start, soupAngle, soupCol);
            start += soupAngle;

            DrawSlice(150, 150, 100, start, aAngle, aCol);
            start += aAngle;

            DrawSlice(150, 150, 100, start, bAngle, bCol);
        }

        private int GiveValidatedBalance(string curVal)
        {
            return !string.IsNullOrEmpty(curVal = curVal?.Trim()) && curVal.All(char.IsDigit) ? int.Parse(curVal) : 0;
        }

        public FinancesAdmin()
        {
            InitializeComponent();
            using var db = new AppDbContext();
            int soupInc = db.Orders.Where(o => o.WantsSoup == true).Count() * 300;
            int aInc = db.Orders.Where(o => o.DishChoice == "A").Count() * 500;
            int bInc = db.Orders.Where(o => o.DishChoice == "B").Count() * 700;
            if (soupInc + aInc + bInc == 0) Info.Text = "Sorry, there's no displayable income!";
            else
            {
                DrawIncomeChart(soupInc, aInc, bInc);
                Info.Text = $"Blue: Soups - {soupInc}\nOrange: Dish A - {aInc}\nGreen: Dish B - {bInc}";
            }

            List<User> users = db.Users.Where(u => u.IsAdmin == false).ToList();
            List<string> usersByName = users.Select(u => u.FullName).ToList();
            UserList.ItemsSource = usersByName;
            UserList.SelectedItem = usersByName[0];
            CBalance.Text = users.Where(u => u.FullName == UserList.SelectedItem).FirstOrDefault().Balance.ToString();
        }

        private void UserChanged(object sender, SelectionChangedEventArgs e)
        {
            using var db = new AppDbContext();
            string selName = UserList.SelectedItem.ToString();
            List<User> users = db.Users.Where(u => u.IsAdmin == false).ToList();

            CBalance.Text = users.Where(u => u.FullName == selName).FirstOrDefault().Balance.ToString();
        }

        private void ChangeBalance(object sender, RoutedEventArgs e)
        {
            using var db = new AppDbContext();
            string selName = UserList.SelectedItem.ToString();
            int newBalance = GiveValidatedBalance(CBalance.Text);
            CBalance.Text = newBalance.ToString();
            var user = db.Users.FirstOrDefault(u => !u.IsAdmin && u.FullName == selName);
            if (user != null) user.Balance = newBalance;

            db.SaveChanges();
        }
    }
}
