using Microsoft.EntityFrameworkCore;
namespace EFCoreCodeFirstDemo.Entities
{
    public class EFCoreDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-RUC57UF;Database=StudentDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Fluent API Configuration
            // Configure One to Many Relationships Between Order and OrderItem
            modelBuilder.Entity<Order>()
            .HasMany(o => o.OrderItems) // Order has many OrderItems, specifies the 'many' side of the relationship
            .WithOne(oi => oi.Order)    // OrderItem is associated with one Order, specifies the 'one' side of the relationship
            .HasForeignKey(oi => oi.OrderId) // OrderId is the Foreign key in OrderItem table, specifies the foreign key
            .OnDelete(DeleteBehavior.Cascade); // This will delete the child record(s) when parent record is deleted
        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

    }
}