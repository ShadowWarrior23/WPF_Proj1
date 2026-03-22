using System.Windows.Controls;
using System.Globalization;


namespace WPF_Proj1.View.UserControls.UserControls1
{
    public partial class DayInMonth : UserControl
    {
        public string today => $"Today's date:\n{DateTime.Now.ToString("dd. (dddd)", new CultureInfo("en-US"))}";
        public DayInMonth()
        {
            InitializeComponent();
            tDay.Text = today;
        }
    }
}
