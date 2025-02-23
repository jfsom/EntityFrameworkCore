using Microsoft.EntityFrameworkCore;
namespace EFCoreCodeFirstDemo.Entities
{
    public class EFCoreDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-RUC57UF;Database=StudentDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        //Overriding the OnModelCreating method to configure the Default and Computed Column values
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure dynamic default value for CreatedOn in Product entity
            modelBuilder.Entity<Product>()
                .Property(p => p.CreatedOn)
                .HasDefaultValueSql("GETDATE()");

            // Configure static or fixed default value for CreatedBy in Product entity
            modelBuilder.Entity<Product>()
                .Property(p => p.CreatedBy)
                .HasDefaultValueSql("'System'");

            // Configure dynamic default value for OrderDate in Order entity
            modelBuilder.Entity<Order>()
                .Property(o => o.OrderDate)
                .HasDefaultValueSql("GETDATE()");

            // Configure static or fixed default value for Status in Order entity to Pending
            modelBuilder.Entity<Order>()
               .Property(o => o.Status)
               .HasDefaultValue("Pending");

            // Configure computed column for CustomerId in Customer entity
            // CUST-SerialNumber
            modelBuilder.Entity<Product>()
                .Property(c => c.SKU)
                .HasComputedColumnSql("'PROD-' + CAST([Category] AS NVARCHAR) + '-' + CAST([SerialNumber] AS NVARCHAR)");

            // Configure computed column for TotalPrice in OrderItem entity
            modelBuilder.Entity<OrderItem>()
                .Property(oi => oi.TotalPrice)
                .HasComputedColumnSql("[Quantity] * [Price]");
        }

        // DbSet properties for each entity
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}