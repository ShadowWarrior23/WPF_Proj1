using Microsoft.EntityFrameworkCore;
using System;
using System.IO;

namespace WPF_Proj1
{
    class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<DailyMenu> DailyMenus { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // DB file will sit next to your .exe (bin/Debug/netX/)
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "pixaf0rk.db");
            options.UseSqlite($"Data Source={dbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<DailyMenu>().ToTable("daily_menu");
            modelBuilder.Entity<Order>().ToTable("orders");
        }
    }
}
