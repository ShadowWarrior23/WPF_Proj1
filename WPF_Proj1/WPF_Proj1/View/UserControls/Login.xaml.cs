using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace WPF_Proj1.View.UserControls
{
    public partial class Login : UserControl
    {
        Dictionary<string, string> users = new Dictionary<string, string>();
        public Login()
        {
            InitializeComponent();
            users["S137B"] = "L_LV.843";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string userName = UserName.Text;
            string password = Password.Text;

            if (String.IsNullOrEmpty(userName) || String.IsNullOrEmpty(password)) MessageBox.Show("Please, fill in all informations correctly!");

            else
            {
                bool found = false;
                foreach (string key in users.Keys)
                {
                    if (userName == "6A2b1S" && password == "PF0_m305")
                    {
                        MessageBox.Show($"Welcome {userName}!", "Successful login", MessageBoxButton.OK, MessageBoxImage.Information);
                        found = true;
                        UserName.Text = "";
                        MainPageAdmin mainPageAdmin = new MainPageAdmin();
                        mainPageAdmin.Show();
                    }
                    else if (userName == key && password == users[key])
                    {
                        MessageBox.Show($"Welcome {userName}!", "Successful login", MessageBoxButton.OK, MessageBoxImage.Information);
                        found = true;
                        UserName.Text = "";
                        MainPage mainPage = new MainPage();
                        mainPage.Show();
                    }
                }
                if (found == false)
                {
                    MessageBox.Show("Please make sure to check your username-password combination!");
                    Password.Focus();
                }
            }

            Password.Text = "";
        }
    }
}
