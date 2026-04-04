using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WebApplication1.Data.Models;
using WPF_Proj1;

namespace WebApplication1.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<DailyMenu> DailyMenus => Set<DailyMenu>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<FoodItem> FoodItems => Set<FoodItem>();

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
                .HasMaxLength(20);

            modelBuilder.Entity<FoodItem>()
                .Property(f => f.Ingreds)
                .HasConversion(
                    v => string.Join(";", v),
                    v => string.IsNullOrWhiteSpace(v)
                        ? Array.Empty<string>()
                        : v.Split(';', StringSplitOptions.RemoveEmptyEntries))
                .Metadata.SetValueComparer(
                    new ValueComparer<string[]>(
                        (a, b) => a!.SequenceEqual(b!),
                        a => a.Aggregate(0, (x, y) => HashCode.Combine(x, y.GetHashCode())),
                        a => a.ToArray()));

            modelBuilder.Entity<FoodItem>()
                .Property(f => f.Allergens)
                .HasConversion(
                    v => string.Join(";", v),
                    v => string.IsNullOrWhiteSpace(v)
                        ? Array.Empty<string>()
                        : v.Split(';', StringSplitOptions.RemoveEmptyEntries))
                .Metadata.SetValueComparer(
                    new ValueComparer<string[]>(
                        (a, b) => a!.SequenceEqual(b!),
                        a => a.Aggregate(0, (x, y) => HashCode.Combine(x, y.GetHashCode())),
                        a => a.ToArray()));

            modelBuilder.Entity<FoodItem>()
                .Property(f => f.Tags)
                .HasConversion(
                    v => string.Join(";", v),
                    v => string.IsNullOrWhiteSpace(v)
                        ? Array.Empty<string>()
                        : v.Split(';', StringSplitOptions.RemoveEmptyEntries))
                .Metadata.SetValueComparer(
                    new ValueComparer<string[]>(
                        (a, b) => a!.SequenceEqual(b!),
                        a => a.Aggregate(0, (x, y) => HashCode.Combine(x, y.GetHashCode())),
                        a => a.ToArray()));
        }
    }
}