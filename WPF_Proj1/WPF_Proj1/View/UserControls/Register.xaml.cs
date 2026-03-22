using System.Windows;
using System.Windows.Controls;

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

        private static string CrUsername(string fName, string lName)
        {
            Random rnd = new Random();

            return $"{fName[0]}{rnd.Next(0, 10)}{rnd.Next(0, 10)}{rnd.Next(0, 10)}{lName[0]}";
        }

        private static string CrPw()
        {
            Random rnd = new Random();
            string letters = "qwertzuiopasdfghjklyxcvbnm";
            string specChars = ",.?:;*-_#&!";

            return $"{letters[rnd.Next(letters.Length)]}{specChars[rnd.Next(specChars.Length)]}{letters[rnd.Next(letters.Length)]}{letters[rnd.Next(letters.Length)]}{specChars[rnd.Next(specChars.Length)]}{rnd.Next(0, 10)}{rnd.Next(0, 10)}{rnd.Next(0, 10)}";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string fName = FName.Text;
            string lName = LName.Text;
            string email = Email.Text;

            if (String.IsNullOrEmpty(fName) || String.IsNullOrEmpty(lName) || String.IsNullOrEmpty(email) || !email.Contains("@") || !email.Contains(".com")) MessageBox.Show("Please, fill in all informations correctly!", "Unfilled Registration", MessageBoxButton.OK, MessageBoxImage.Warning);

            using var db = new AppDbContext();
            User newUser = new User
            {
                Username = "",
                FullName = "",
                PasswordHash = "",
                IsAdmin = false,
                Balance = 0
            };

            if (!db.Users.Select(u => u.Email).Contains(email))
            {
                newUser = new User
                {
                    Username = CrUsername(fName, lName),
                    FullName = $"{fName} {lName}",
                    PasswordHash = CrPw(),
                    IsAdmin = false,
                    Balance = 0
                };

                db.Users.Add(newUser);
                db.SaveChanges();
                List<int> weekdays = db.DailyMenus.Select(d => d.Day.Day).ToList();
                //MessageBox.Show($"^{new DateOnly(2026, 4, 1).ToString().Split(".")[2].Trim()}^");
                int[] yearAndMonth = new int[] { db.DailyMenus.ToList().Last().Day.Year, db.DailyMenus.ToList().Last().Day.Month };
                foreach (int day in weekdays)
                {
                    db.Orders.Add(new Order
                    {
                        UserId = newUser.Id,
                        Day = new DateOnly(yearAndMonth[0], yearAndMonth[1], day),
                        WantsSoup = false,
                        DishChoice = "n"
                    });
                }
                db.SaveChanges();
            }

            MessageBox.Show($"You've successfully registered!\nYour login information:\nUsername: {newUser.Username}; Password: {newUser.PasswordHash}");

            FName.Text = "";
            LName.Text = "";
            Email.Text = "";
            FName.Focus();
        }

    }
}
