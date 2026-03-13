using System.Windows.Controls;

namespace WPF_Proj1.View.UserControls
{
    public partial class ProfilesAdmin : UserControl
    {
        public ProfilesAdmin()
        {
            InitializeComponent();
            using var db = new AppDbContext();



            foreach (var item in db.Users)
            {

            }
        }
    }
}
