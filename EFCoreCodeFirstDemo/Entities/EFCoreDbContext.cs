using Microsoft.EntityFrameworkCore;
namespace EFCoreCodeFirstDemo.Entities
{
    public class EFCoreDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Configuring the Connection String
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-RUC57UF;Database=StudentDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuring the Customer entity
            modelBuilder.Entity<Customer>(entity =>
            {
                // Setting the table name and schema
                entity.ToTable("tblCustomer", schema: "Admin");

                // Setting the primary key
                entity.HasKey(c => c.CustomerId);

                // Setting alternate key (unique constraint) on Email
                entity.HasAlternateKey(c => c.Email);

                // Configuring indexes
                entity.HasIndex(c => c.Email).IsUnique();

                // Configuring the owned entity Address
                entity.OwnsOne(c => c.Address);

                // Configuring One to Many Relationships Between Customer and Order
                entity.HasMany(c => c.Orders)
                    .WithOne(o => o.Customer)
                    .HasForeignKey(o => o.CustomerId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configuring the Product entity
            modelBuilder.Entity<Product>(entity =>
            {
                // Setting the primary key
                entity.HasKey(p => p.ProductId);
            });

            // Configuring the Order entity
            modelBuilder.Entity<Order>(entity =>
            {
                // Setting the primary key
                entity.HasKey(o => o.OrderId);

                // Store the enum as string
                entity.Property(o => o.Status)
                    .HasConversion<string>();

                // Configuring One to Many Relationships between Order and Order Items
                entity.HasMany(o => o.OrderItems)
                    .WithOne(oi => oi.Order)
                    .HasForeignKey(oi => oi.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configuring the OrderItem entity
            modelBuilder.Entity<OrderItem>(entity =>
            {
                // Configuring composite primary key
                entity.HasKey(oi => new { oi.OrderId, oi.ProductId });

                // Configuring One to Many relationship Between Product and OrderItem 
                entity.HasOne(oi => oi.Product)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(oi => oi.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }

        // Define DbSets
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}