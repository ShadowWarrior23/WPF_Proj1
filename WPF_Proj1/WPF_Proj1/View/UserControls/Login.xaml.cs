using System.Windows;
using System.Windows.Controls;

namespace WPF_Proj1.View.UserControls
{
    public partial class Login : UserControl
    {
        public Login()
        {
            InitializeComponent();
        }

        private static bool AdminLogin(string userName, UserControls1.ClearableTextboxDark2 UserName)
        {
            MessageBox.Show($"Welcome {userName}! Admin detected", "Successful admin login", MessageBoxButton.OK, MessageBoxImage.Information);
            UserName.Text = "";
            MainPageAdmin mainPageAdmin = new MainPageAdmin();
            mainPageAdmin.Show();
            return true;
        }

        private static bool CustomerLogin(string userName, UserControls1.ClearableTextboxDark2 UserName)
        {
            MessageBox.Show($"Welcome {userName}!", "Successful login", MessageBoxButton.OK, MessageBoxImage.Information);
            UserName.Text = "";
            MainPage mainPage = new MainPage(userName);
            mainPage.Show();
            return true;
        }

        private static bool WrongLoginInfo(UserControls1.ClearableTextboxDark2 Password)
        {
            MessageBox.Show("Please make sure to check your username-password combination!", "Incorrect Login Infos", MessageBoxButton.OK, MessageBoxImage.Warning);
            Password.Focus();
            return false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using var db = new AppDbContext();

            string userName = UserName.Text;
            string password = Password.Text;

            if (String.IsNullOrEmpty(userName) || String.IsNullOrEmpty(password)) MessageBox.Show("Please, fill in all informations correctly!", "Unfilled Login", MessageBoxButton.OK, MessageBoxImage.Warning);

            else
            {
                var users = db.Users.Select(p => new { un = p.Username, pw = p.PasswordHash, adm = p.IsAdmin }).ToList();
                bool logged = false;
                int i = 0;
                while (logged == false && i < users.Count)
                {
                    logged = users[i].un == userName && users[i].pw == password ? users[i].adm == true ? AdminLogin(users[i].un, UserName) : CustomerLogin(users[i].un, UserName) : false;
                    i++;
                }

                if (i >= users.Count)
                {
                    WrongLoginInfo(Password);
                }
            }
            Password.Text = "";
        }
    }
}
