using Microsoft.EntityFrameworkCore;
using PixaF0rk_Console;
using System;
using System.Collections.Generic;

namespace PixaF0rk_Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var db = new AppDbContext();
            Console.WriteLine("Welcome to PixaF0rk Console-Line Interface! Let me list your options:\n\n1: The next eligible day's menu\n2: This month's menu\n3: All users\n4: Food Information\n0: Exit");
            Console.WriteLine();
            Console.Write("What would you like to choose? ");
            string opt = Console.ReadLine();

            while (opt != "0")
            {
                if (opt == "1")
                {
                    List<DateOnly> weekdays = db.DailyMenus.Select(d => d.Day).OrderBy(d => d).ToList();
                    DateOnly today = DateOnly.FromDateTime(DateTime.Today);
                    DateOnly elDate = weekdays.Any(d => d >= today) ? weekdays.First(d => d >= today) : weekdays.First();

                    var nOrder = db.DailyMenus.Where(d => d.Day == elDate).FirstOrDefault();
                    Console.WriteLine($"Next Eligible Day: {nOrder.Day}; Soup: {nOrder.Soup}, Dish A: {nOrder.DishA}, Dish B: {nOrder.DishB}");

                    Console.Write("Would you like to export it? (Y/N) ");
                    string exp = Console.ReadLine().ToUpper();
                    if (exp == "Y")
                    {
                        using (StreamWriter sw = new StreamWriter(@"..\..\..\..\..\exports\daily_menu.txt"))
                        {
                            sw.WriteLine($"Next Eligible Day: {nOrder.Day}; Soup: {nOrder.Soup}, Dish A: {nOrder.DishA}, Dish B: {nOrder.DishB}");
                            Console.WriteLine("Successful export!");
                        }
                    }
                }
                else if (opt == "2")
                {
                    var menu0 = db.DailyMenus.ToList();
                    foreach (var m in menu0)
                    {
                        Console.WriteLine($"Day: {m.Day}; Soup: {m.Soup}, Dish A: {m.DishA}, Dish B: {m.DishB}");
                    }

                    Console.Write("Would you like to export it? (Y/N) ");
                    string exp = Console.ReadLine().ToUpper();
                    if (exp == "Y")
                    {
                        using (StreamWriter sw = new StreamWriter(@"..\..\..\..\..\exports\monthly_menu.txt"))
                        {
                            foreach (var m in menu0)
                            {
                                sw.WriteLine($"Day: {m.Day}; Soup: {m.Soup}, Dish A: {m.DishA}, Dish B: {m.DishB}");
                            }
                            Console.WriteLine("Successful export!");
                        }
                    }
                }

                else if (opt == "3")
                {
                    var users0 = db.Users.Where(u => u.IsAdmin != true).ToList();
                    foreach (var u in users0)
                    {
                        Console.WriteLine($"Name: {u.FullName}, Username: {u.Username}, Email: {u.Email}, Password: {u.PasswordHash}, Balance: {u.Balance}");
                    }

                    Console.Write("Would you like to export it? (Y/N) ");
                    string exp = Console.ReadLine().ToUpper();
                    if (exp == "Y")
                    {
                        using (StreamWriter sw = new StreamWriter(@"..\..\..\..\..\exports\users.txt"))
                        {
                            foreach (var u in users0)
                            {
                                sw.WriteLine($"Name: {u.FullName}, Username: {u.Username}, Email: {u.Email}, Password: {u.PasswordHash}, Balance: {u.Balance}");
                            }
                            Console.WriteLine("Successful export!");
                        }
                    }
                }

                else if (opt == "4")
                {
                    var foodItems0 = db.FoodItems.Where(f => f.Ingreds.Length != 0).ToList(); //???
                    foreach (var f in foodItems0)
                    {
                        Console.WriteLine($"Dish Name: {f.Name}; Category: {f.Categ}; Ingredients: {String.Join(", ", f.Ingreds)}; Allergens: {(f.Allergens.Length != 0 ? String.Join(", ", f.Allergens) : "None")}; Tags: {String.Join(", ", f.Tags)}");
                    }

                    Console.Write("Would you like to export it? (Y/N) ");
                    string exp = Console.ReadLine().ToUpper();
                    if (exp == "Y")
                    {
                        using (StreamWriter sw = new StreamWriter(@"..\..\..\..\..\exports\food_items.txt"))
                        {
                            foreach (var f in foodItems0)
                            {
                                sw.WriteLine($"Dish Name: {f.Name}; Category: {f.Categ}; Ingredients: {f.Ingreds.ToString()}; ");
                            }
                        }
                        Console.WriteLine("Successful export!");
                    }
                }

                else
                {
                    Console.WriteLine("Please enter a valid option!");
                }
                Console.WriteLine();
                Console.Write("What would you like to choose? ");
                opt = Console.ReadLine();
            }

            Console.ReadLine();

        }
    }
}