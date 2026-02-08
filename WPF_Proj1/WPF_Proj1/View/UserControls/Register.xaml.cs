using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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

namespace WPF_Proj1.View.UserControls
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : UserControl
    {
        public Register()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string fName = FName.Text;
            string lName = LName.Text;
            string email = Email.Text;

            if (String.IsNullOrEmpty(fName) || String.IsNullOrEmpty(lName) || String.IsNullOrEmpty(email) || !email.Contains("@") || !email.Contains(".com")) MessageBox.Show("Please, fill in all informations correctly!", "Unfilled Registration", MessageBoxButton.OK, MessageBoxImage.Warning);
            else
            {
                MessageBox.Show($"{fName} {lName} - {email}");
                /*var smtp = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential("gpl.76.x@gmail.com", "app-password"),
                    EnableSsl = true
                };

                var mail = new MailMessage();
                mail.From = new MailAddress("jjobudos@gmail.com");
                mail.To.Add("gamernagylolhih@example.com");
                mail.Subject = "Test";
                mail.Body = "Hello from WPF";

                smtp.Send(mail);*/
            }

            using var db = new AppDbContext();
            // query/insert user
            db.SaveChanges();


            FName.Text = "";
            LName.Text = "";
            Email.Text = "";
            FName.Focus();
        }

    }
}
