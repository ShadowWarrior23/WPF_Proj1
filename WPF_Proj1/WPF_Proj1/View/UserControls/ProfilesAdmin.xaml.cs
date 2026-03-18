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

            int i = 0;
            foreach (var u in users)
            {
                MessageBox.Show(u.Username);
                var prof = new Profile();
                prof.Username.Text = u.Username;
                prof.FullName.Text = u.FullName;
                prof.Password.Text = u.PasswordHash;
                //MessageBox.Show($"{u.Username}, {u.FullName}, {u.PasswordHash}");
                prof.Height = 150;
                //i == 0 ? c0.Children.Add(prof) : i == 1 ? c1.Children.Add(prof) : c2.Children.Add(prof);
                if (i == 0) c0.Children.Add(prof);
                else if (i == 1) c1.Children.Add(prof);
                else c2.Children.Add(prof);
                if (i < 3) prof.Margin = "0 0 150 0";
                i++;
            }
        }
    }
}
