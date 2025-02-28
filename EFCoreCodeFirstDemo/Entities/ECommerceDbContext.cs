using Microsoft.EntityFrameworkCore;
namespace EFCoreCodeFirstDemo.Entities
{
    public class ECommerceDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Configure the connection string to your ECommerceDB SQL Server Database
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-RUC57UF;Database=ECommerceDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        // Configures the model and mappings between entities and database
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure enums to be stored as strings
            // For PaymentStatus enum
            modelBuilder.Entity<Payment>()
                .Property(c => c.Status)
                .HasConversion<string>()
                .IsRequired();    // Optional: Specify if the property is required
        }

        // DbSets for primary e-commerce entities
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Payment> Payments { get; set; }
    }
}