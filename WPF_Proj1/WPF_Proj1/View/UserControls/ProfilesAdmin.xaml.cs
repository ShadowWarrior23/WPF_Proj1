using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using WPF_Proj1.View.UserControls.UserControls1;

namespace WPF_Proj1.View.UserControls
{
    public partial class ProfilesAdmin : UserControl
    {
        public ProfilesAdmin()
        {
            InitializeComponent();
            using var db = new AppDbContext();

            var users = db.Users.Select(u => new {u.FullName, u.Username, u.PasswordHash});

            foreach (var u in users)
            {
                var prof = new Profile();
                prof.Username.Text = u.Username;
                prof.FullName.Text = u.FullName;
                prof.Password.Text = u.PasswordHash;
                MessageBox.Show($"{u.Username}, {u.FullName}, {u.PasswordHash}");
            }
        }
    }
}
