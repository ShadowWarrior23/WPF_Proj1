using Microsoft.EntityFrameworkCore;
using WPF_Proj1;
using WPF_Proj1.Data.Models;

namespace PixaF0rk_Console
{
    internal class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<DailyMenu> DailyMenus { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<FoodItem> FoodItems { get; set; }

        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var basePath = Directory.GetCurrentDirectory();
                var dbPath = Path.GetFullPath(Path.Combine(basePath, @"..\..\..\..\WPF_Proj1\pixaFork.db"));
                optionsBuilder.UseSqlite($"Data Source={dbPath}");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<DailyMenu>().ToTable("daily_menu");
            modelBuilder.Entity<Order>().ToTable("orders");
            modelBuilder.Entity<FoodItem>().ToTable("food_items");

            modelBuilder.Entity<Order>()
                .HasIndex(o => new { o.UserId, o.Day })
                .IsUnique();

            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.DailyMenu)
                .WithMany(m => m.Orders)
                .HasForeignKey(o => o.Day)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FoodItem>()
                .HasIndex(f => f.Name)
                .IsUnique();

            modelBuilder.Entity<FoodItem>()
                .Property(f => f.Name)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<FoodItem>()
                .Property(f => f.Categ)
                .IsRequired()
                .HasMaxLength(4);

            modelBuilder.Entity<FoodItem>()
                .Property(f => f.Ingreds)
                .HasConversion(
                    v => string.Join(";", v),
                    v => string.IsNullOrWhiteSpace(v)
                        ? Array.Empty<string>()
                        : v.Split(';', StringSplitOptions.RemoveEmptyEntries));

            modelBuilder.Entity<FoodItem>()
                .Property(f => f.Allergens)
                .HasConversion(
                    v => string.Join(";", v),
                    v => string.IsNullOrWhiteSpace(v)
                        ? Array.Empty<string>()
                        : v.Split(';', StringSplitOptions.RemoveEmptyEntries));

            modelBuilder.Entity<FoodItem>()
                .Property(f => f.Tags)
                .HasConversion(
                    v => string.Join(";", v),
                    v => string.IsNullOrWhiteSpace(v)
                        ? Array.Empty<string>()
                        : v.Split(';', StringSplitOptions.RemoveEmptyEntries));

            modelBuilder.Entity<FoodItem>()
                .Property(f => f.Tags)
                .Metadata.SetValueComparer(
                    new Microsoft.EntityFrameworkCore.ChangeTracking.ValueComparer<string[]>(
                        (a, b) => a.SequenceEqual(b),
                        a => a.Aggregate(0, (x, y) => HashCode.Combine(x, y.GetHashCode())),
                        a => a.ToArray()));

            modelBuilder.Entity<FoodItem>()
                .Property(f => f.Ingreds)
                .Metadata.SetValueComparer(
                    new Microsoft.EntityFrameworkCore.ChangeTracking.ValueComparer<string[]>(
                        (a, b) => a.SequenceEqual(b),
                        a => a.Aggregate(0, (x, y) => HashCode.Combine(x, y.GetHashCode())),
                        a => a.ToArray()));

            modelBuilder.Entity<FoodItem>()
                .Property(f => f.Allergens)
                .Metadata.SetValueComparer(
                    new Microsoft.EntityFrameworkCore.ChangeTracking.ValueComparer<string[]>(
                        (a, b) => a.SequenceEqual(b),
                        a => a.Aggregate(0, (x, y) => HashCode.Combine(x, y.GetHashCode())),
                        a => a.ToArray()));
        }
    }
}