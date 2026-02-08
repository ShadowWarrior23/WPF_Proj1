using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Proj1
{
    internal class AppDbContext : DbContext
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<DailyMenu> DailyMenus => Set<DailyMenu>();
        public DbSet<Order> Orders => Set<Order>();

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // One stable DB path, always the same.
            var folder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "WPF_Proj1");

            Directory.CreateDirectory(folder);

            var dbPath = Path.Combine(folder, "pixaf0rk.db");
            options.UseSqlite($"Data Source={dbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Match your existing table names
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<DailyMenu>().ToTable("daily_menu");
            modelBuilder.Entity<Order>().ToTable("orders");

            // IMPORTANT if DailyMenu’s PK is Day (common for menus)
            // If your DailyMenu entity already has [Key], this is optional.
            modelBuilder.Entity<DailyMenu>().HasKey(x => x.Day);
        }
    }
}
