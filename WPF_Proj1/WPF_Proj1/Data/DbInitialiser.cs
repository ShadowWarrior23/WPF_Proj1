using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Proj1.Data.Models;

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
                    new User { Username = "S137B", FullName = "Szabó Bence", PasswordHash = "L_LV.843", IsAdmin = false, Balance = 50000 },
                    new User { Username = "6A2b1S", FullName = "SzB Admin", PasswordHash = "PF0_m305", IsAdmin = true, Balance = 0 }
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
