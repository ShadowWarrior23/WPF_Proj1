using Microsoft.EntityFrameworkCore;

namespace WPF_Proj1
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<DailyMenu> DailyMenus => Set<DailyMenu>();
        public DbSet<Order> Orders => Set<Order>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<DailyMenu>().ToTable("daily_menu");
            modelBuilder.Entity<Order>().ToTable("orders");

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
        }
    }
}