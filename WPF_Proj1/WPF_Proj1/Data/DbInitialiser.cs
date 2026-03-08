namespace WPF_Proj1.Data.Models
{
    public static class DbInitialiser
    {
        public static void Initialize()
        {
            using var db = new AppDbContext();

            db.Database.EnsureCreated();

            if (!db.Users.Any())
            {
                db.Users.AddRange(
                    new User { Username = "6A2b1S", FullName = "SzB Admin", Email="pixaf0rk@gmailcom", PasswordHash = "PF0_m305", IsAdmin = true, Balance = 0 },
                    new User { Username = "S137B", FullName = "Szabó Bence", Email="bencepor@gmail.com", PasswordHash = "L_LV.843", IsAdmin = false, Balance = 50000 },
                    new User { Username = "T012Á", FullName = "Takács Ákos", Email = "takacsakos@gmail.com", PasswordHash = "C$FB,000", IsAdmin = false, Balance = 500 }
                );
            }

            if (!db.DailyMenus.Any())
            {
                db.DailyMenus.AddRange(
                    new DailyMenu
                    {
                        Day = new DateOnly(2026, 3, 9),
                        Soup = "Tomato Soup",
                        DishA = "Chicken Paprikash",
                        DishB = "Fried Cheese"
                    },
                    new DailyMenu
                    {
                        Day = new DateOnly(2026, 3, 10),
                        Soup = "Bean Soup",
                        DishA = "Pork Stew",
                        DishB = "Vegetable Rice"
                    }
                );
            }

            db.SaveChanges();
        }
    }
}
