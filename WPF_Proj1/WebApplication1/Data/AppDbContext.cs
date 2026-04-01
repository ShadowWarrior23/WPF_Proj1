using Microsoft.EntityFrameworkCore;

namespace WPF_Proj1
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<DailyMenu> DailyMenus => Set<DailyMenu>();
        public DbSet<Order> Orders => Set<Order>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=PixaFork.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Match your existing table names
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<DailyMenu>().ToTable("daily_menu");
            modelBuilder.Entity<Order>().ToTable("orders");

            // Unique: one order per user per day
            modelBuilder.Entity<Order>()
                .HasIndex(o => new { o.UserId, o.Day })
                .IsUnique();

            // Relation: Order -> User
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relation: Order -> DailyMenu
            modelBuilder.Entity<Order>()
                .HasOne(o => o.DailyMenu)
                .WithMany(m => m.Orders)
                .HasForeignKey(o => o.Day)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
