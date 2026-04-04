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
                    new User { Username = "6A2b1S", FullName = "SzB Admin", Email = "pixaf0rk@gmail.com", PasswordHash = "PF0_m305", IsAdmin = true, Balance = 0 },
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
                        DishChoice = null
                    }
                );
            }*/

            db.SaveChanges();

            SeedFoodItemsFromDailyMenus(db);

            db.SaveChanges();

            SeedFoodItemDetails(db);
            db.SaveChanges();
        }

        private static void SeedFoodItemsFromDailyMenus(AppDbContext db)
        {
            var existingNames = db.FoodItems
                .Select(f => f.Name)
                .ToHashSet(StringComparer.OrdinalIgnoreCase);

            var dailyMenus = db.DailyMenus.ToList();

            foreach (var menu in dailyMenus)
            {
                TryAddFood(menu.Soup, "soup");
                TryAddFood(menu.DishA, "dish");
                TryAddFood(menu.DishB, "dish");
            }

            void TryAddFood(string? name, string categ)
            {
                if (string.IsNullOrWhiteSpace(name))
                    return;

                name = name.Trim();

                if (existingNames.Contains(name))
                    return;

                db.FoodItems.Add(new FoodItem
                {
                    Name = name,
                    Categ = categ,
                    Ingreds = Array.Empty<string>(),
                    Allergens = Array.Empty<string>(),
                    Tags = Array.Empty<string>()
                });

                existingNames.Add(name);
            }
        }

        private static void SeedFoodItemDetails(AppDbContext db)
        {
            UpdateFood("Vegetable Soup",
                new[] { "carrot", "parsley root", "celery", "onion" },
                Array.Empty<string>(),
                new[] { "vegetarian", "light" });

            UpdateFood("Cheese Pasta",
                new[] { "pasta", "cheese", "sour cream" },
                new[] { "gluten", "milk" },
                new[] { "vegetarian" });

            UpdateFood("Grilled Chicken Breast with Bulgur",
                new[] { "chicken breast", "bulgur", "oil" },
                new[] { "gluten" },
                new[] { "high-protein" });

            UpdateFood("Green Pea Soup",
                new[] { "green peas", "carrot", "onion" },
                Array.Empty<string>(),
                new[] { "vegetarian" });

            UpdateFood("Vegetable Couscous",
                new[] { "couscous", "vegetables", "oil" },
                new[] { "gluten" },
                new[] { "vegetarian" });

            UpdateFood("Roast Pork with Potatoes",
                new[] { "pork", "potatoes", "oil" },
                Array.Empty<string>(),
                new[] { "traditional" });

            UpdateFood("Garlic Cream Soup",
                new[] { "garlic", "cream", "flour" },
                new[] { "milk", "gluten" },
                new[] { "vegetarian" });

            UpdateFood("Mushroom Risotto",
                new[] { "rice", "mushroom", "cheese" },
                new[] { "milk" },
                new[] { "vegetarian" });

            UpdateFood("Turkey Meatballs with Tomato Sauce",
                new[] { "turkey", "egg", "breadcrumbs", "tomato" },
                new[] { "gluten", "egg" },
                new[] { "high-protein" });

            UpdateFood("Lentil Soup",
                new[] { "lentils", "carrot", "onion" },
                Array.Empty<string>(),
                new[] { "vegetarian", "high-protein" });

            UpdateFood("Vegetable Stir Fry",
                new[] { "mixed vegetables", "soy sauce", "oil" },
                new[] { "soy" },
                new[] { "vegetarian" });

            UpdateFood("Beef Stew with Barley",
                new[] { "beef", "barley", "onion" },
                new[] { "gluten" },
                new[] { "traditional" });

            UpdateFood("Pumpkin Cream Soup",
                new[] { "pumpkin", "cream" },
                new[] { "milk" },
                new[] { "vegetarian" });

            UpdateFood("Spinach Pasta",
                new[] { "pasta", "spinach", "cream" },
                new[] { "gluten", "milk" },
                new[] { "vegetarian" });

            UpdateFood("Grilled Fish with Lemon Potatoes",
                new[] { "fish", "potatoes", "lemon" },
                new[] { "fish" },
                new[] { "light" });

            UpdateFood("Chicken Ragout Soup",
                new[] { "chicken", "vegetables", "flour" },
                new[] { "gluten" },
                new[] { "traditional" });

            UpdateFood("Sweet Cottage Cheese Dumplings",
                new[] { "cottage cheese", "semolina", "egg" },
                new[] { "milk", "gluten", "egg" },
                new[] { "sweet", "vegetarian" });

            UpdateFood("Stuffed Cabbage",
                new[] { "cabbage", "pork", "rice" },
                Array.Empty<string>(),
                new[] { "traditional" });

            UpdateFood("Tomato Soup with Basil",
                new[] { "tomato", "basil" },
                Array.Empty<string>(),
                new[] { "vegetarian", "light" });

            UpdateFood("Vegetable Lasagna",
                new[] { "pasta", "vegetables", "cheese" },
                new[] { "gluten", "milk" },
                new[] { "vegetarian" });

            UpdateFood("Chicken Paprikash with Noodles",
                new[] { "chicken", "paprika", "noodles", "sour cream" },
                new[] { "gluten", "milk" },
                new[] { "traditional" });

            UpdateFood("Bean Soup",
                new[] { "beans", "carrot", "onion" },
                Array.Empty<string>(),
                new[] { "vegetarian", "high-protein" });

            UpdateFood("Egg Fried Rice",
                new[] { "rice", "egg", "soy sauce" },
                new[] { "egg", "soy" },
                new[] { "vegetarian" });

            UpdateFood("Roast Duck with Red Cabbage",
                new[] { "duck", "red cabbage" },
                Array.Empty<string>(),
                new[] { "traditional" });

            UpdateFood("Cream of Broccoli Soup",
                new[] { "broccoli", "cream" },
                new[] { "milk" },
                new[] { "vegetarian" });

            UpdateFood("Cheese Polenta",
                new[] { "cornmeal", "cheese" },
                new[] { "milk" },
                new[] { "vegetarian", "gluten-free" });

            UpdateFood("Pork Stew with Noodles",
                new[] { "pork", "noodles", "onion" },
                new[] { "gluten" },
                new[] { "traditional" });

            UpdateFood("Vegetable Ragout Soup",
                new[] { "vegetables", "flour" },
                new[] { "gluten" },
                new[] { "vegetarian" });

            UpdateFood("Mushroom Pasta",
                new[] { "pasta", "mushroom" },
                new[] { "gluten" },
                new[] { "vegetarian" });

            UpdateFood("Fried Fish with Potato Salad",
                new[] { "fish", "potatoes", "oil" },
                new[] { "fish" },
                new[] { "traditional" });

            UpdateFood("Fruit Soup",
                new[] { "mixed fruits", "sugar" },
                Array.Empty<string>(),
                new[] { "sweet", "vegetarian" });

            UpdateFood("Semolina Pudding with Cocoa",
                new[] { "semolina", "milk", "cocoa" },
                new[] { "gluten", "milk" },
                new[] { "sweet", "vegetarian" });

            UpdateFood("Grilled Turkey Breast with Rice",
                new[] { "turkey", "rice" },
                Array.Empty<string>(),
                new[] { "high-protein" });

            UpdateFood("Potato Soup",
                new[] { "potatoes", "onion" },
                Array.Empty<string>(),
                new[] { "vegetarian" });

            UpdateFood("Vegetable Stew with Bread",
                new[] { "vegetables", "bread" },
                new[] { "gluten" },
                new[] { "vegetarian" });

            UpdateFood("Beef Stroganoff with Pasta",
                new[] { "beef", "cream", "pasta" },
                new[] { "milk", "gluten" },
                new[] { "traditional" });

            UpdateFood("Zucchini Cream Soup",
                new[] { "zucchini", "cream" },
                new[] { "milk" },
                new[] { "vegetarian" });

            UpdateFood("Vegetable Fried Noodles",
                new[] { "noodles", "vegetables", "soy sauce" },
                new[] { "gluten", "soy" },
                new[] { "vegetarian" });

            UpdateFood("Roast Chicken Thigh with Rice",
                new[] { "chicken", "rice" },
                Array.Empty<string>(),
                new[] { "traditional" });

            UpdateFood("Green Bean Soup",
                new[] { "green beans", "carrot" },
                Array.Empty<string>(),
                new[] { "vegetarian" });

            UpdateFood("Potato Dumplings with Sauce",
                new[] { "potato", "flour" },
                new[] { "gluten" },
                new[] { "vegetarian" });

            UpdateFood("Braised Pork with Vegetables",
                new[] { "pork", "vegetables" },
                Array.Empty<string>(),
                new[] { "traditional" });

            UpdateFood("Cauliflower Soup",
                new[] { "cauliflower", "carrot" },
                Array.Empty<string>(),
                new[] { "vegetarian" });

            UpdateFood("Cheese Rice",
                new[] { "rice", "cheese" },
                new[] { "milk" },
                new[] { "vegetarian" });

            UpdateFood("Grilled Fish with Couscous",
                new[] { "fish", "couscous" },
                new[] { "fish", "gluten" },
                new[] { "light" });


            void UpdateFood(string name, string[] ingreds, string[] allergens, string[] tags)
            {
                var food = db.FoodItems.FirstOrDefault(f => f.Name == name);
                if (food == null)
                    return;

                if (food.Ingreds == null || food.Ingreds.Length == 0)
                    food.Ingreds = ingreds;

                if (food.Allergens == null || food.Allergens.Length == 0)
                    food.Allergens = allergens;

                if (food.Tags == null || food.Tags.Length == 0)
                    food.Tags = tags;
            }
        }
    }
}
