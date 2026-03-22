using System.Windows;
using System.Windows.Controls;
using WPF_Proj1.View.UserControls.UserControls1;

namespace WPF_Proj1.View.UserControls
{
    public partial class ProfilesAdmin : UserControl
    {
        public ProfilesAdmin()
        {
            InitializeComponent();
            LoadProfiles();
        }

        private void LoadProfiles()
        {
            c0.Children.Clear();
            c1.Children.Clear();
            c2.Children.Clear();

            using var db = new AppDbContext();

            var users = db.Users
                .Where(u => !u.IsAdmin)
                .Select(u => new
                {
                    u.FullName,
                    u.Username,
                    u.PasswordHash
                })
                .ToList();

            int i = 0;
            foreach (var u in users)
            {
                var prof = new Profile();

                prof.Username.Text = u.Username;
                prof.FullName.Text = u.FullName;
                prof.Password.Text = u.PasswordHash;

                prof.UserNameValue = u.Username;
                prof.Height = 150;

                prof.RemoveRequested += Profile_RemoveRequested;

                if (i % 3 == 0) c0.Children.Add(prof);
                else if (i % 3 == 1) c1.Children.Add(prof);
                else c2.Children.Add(prof);

                if (i < 3) prof.Margin = new Thickness(0, 0, 0, 350);
                if (i > 5) prof.Margin = new Thickness(0, 350, 0, 0);

                i++;
            }
        }

        private void Profile_RemoveRequested(object? sender, System.EventArgs e)
        {
            if (sender is not Profile prof)
                return;

            using var db = new AppDbContext();

            var user = db.Users.FirstOrDefault(u => u.Username == prof.UserNameValue);
            if (user != null)
            {
                db.Users.Remove(user);
                db.SaveChanges();
            }

            LoadProfiles();
        }
    }
}
