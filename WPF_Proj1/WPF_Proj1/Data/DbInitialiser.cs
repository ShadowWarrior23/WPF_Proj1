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
                    new User { Username = "6A2b1S", FullName = "SzB Admin", Email = "pixaf0rk@gmailcom", PasswordHash = "PF0_m305", IsAdmin = true, Balance = 0 },
                    new User { Username = "S137B", FullName = "Szabó Bence", Email = "bencepor@gmail.com", PasswordHash = "L_LV.843", IsAdmin = false, Balance = 500000 },
                    new User { Username = "T012Á", FullName = "Takács Ákos", Email = "takacsakos@gmail.com", PasswordHash = "C$FB,000", IsAdmin = false, Balance = 5000 }
                );
            }

            if (!db.DailyMenus.Any())
            {
                db.DailyMenus.AddRange(
                    new DailyMenu
                    {
                        Day = new DateOnly(2026, 4, 1),
                        Soup = "Vegetable Soup",
                        DishA = "Cheese Pasta",
                        DishB = "Grilled Chicken Breast with Bulgur"
                    },
                    new DailyMenu
                    {
                        Day = new DateOnly(2026, 4, 13),
                        Soup = "Green Pea Soup",
                        DishA = "Vegetable Couscous",
                        DishB = "Roast Pork with Potatoes"
                    },
                    new DailyMenu
                    {
                        Day = new DateOnly(2026, 4, 14),
                        Soup = "Garlic Cream Soup",
                        DishA = "Mushroom Risotto",
                        DishB = "Turkey Meatballs with Tomato Sauce"
                    },
                    new DailyMenu
                    {
                        Day = new DateOnly(2026, 4, 15),
                        Soup = "Lentil Soup",
                        DishA = "Vegetable Stir Fry",
                        DishB = "Beef Stew with Barley"
                    },
                    new DailyMenu
                    {
                        Day = new DateOnly(2026, 4, 16),
                        Soup = "Pumpkin Cream Soup",
                        DishA = "Spinach Pasta",
                        DishB = "Grilled Fish with Lemon Potatoes"
                    },
                    new DailyMenu
                    {
                        Day = new DateOnly(2026, 4, 17),
                        Soup = "Chicken Ragout Soup",
                        DishA = "Sweet Cottage Cheese Dumplings",
                        DishB = "Stuffed Cabbage"
                    },

                    new DailyMenu
                    {
                        Day = new DateOnly(2026, 4, 20),
                        Soup = "Tomato Soup with Basil",
                        DishA = "Vegetable Lasagna",
                        DishB = "Chicken Paprikash with Noodles"
                    },
                    new DailyMenu
                    {
                        Day = new DateOnly(2026, 4, 21),
                        Soup = "Bean Soup",
                        DishA = "Egg Fried Rice",
                        DishB = "Roast Duck with Red Cabbage"
                    },
                    new DailyMenu
                    {
                        Day = new DateOnly(2026, 4, 22),
                        Soup = "Cream of Broccoli Soup",
                        DishA = "Cheese Polenta",
                        DishB = "Pork Stew with Noodles"
                    },
                    new DailyMenu
                    {
                        Day = new DateOnly(2026, 4, 23),
                        Soup = "Vegetable Ragout Soup",
                        DishA = "Mushroom Pasta",
                        DishB = "Fried Fish with Potato Salad"
                    },
                    new DailyMenu
                    {
                        Day = new DateOnly(2026, 4, 24),
                        Soup = "Fruit Soup",
                        DishA = "Semolina Pudding with Cocoa",
                        DishB = "Grilled Turkey Breast with Rice"
                    },

                    new DailyMenu
                    {
                        Day = new DateOnly(2026, 4, 27),
                        Soup = "Potato Soup",
                        DishA = "Vegetable Stew with Bread",
                        DishB = "Beef Stroganoff with Pasta"
                    },
                    new DailyMenu
                    {
                        Day = new DateOnly(2026, 4, 28),
                        Soup = "Zucchini Cream Soup",
                        DishA = "Vegetable Fried Noodles",
                        DishB = "Roast Chicken Thigh with Rice"
                    },
                    new DailyMenu
                    {
                        Day = new DateOnly(2026, 4, 29),
                        Soup = "Green Bean Soup",
                        DishA = "Potato Dumplings with Sauce",
                        DishB = "Braised Pork with Vegetables"
                    },
                    new DailyMenu
                    {
                        Day = new DateOnly(2026, 4, 30),
                        Soup = "Cauliflower Soup",
                        DishA = "Cheese Rice",
                        DishB = "Grilled Fish with Couscous"
                    }
                );
            }

            /*if (!db.Orders.Any())
            {
                db.Orders.AddRange(
                    new Order
                    {
                        UserId = 1,
                        Day = new DateOnly(2026, 3, 20),
                        WantsSoup = false,
                        DishChoice = "n"
                    }
                );
            }*/

            db.SaveChanges();
        }
    }
}
