using EFCorePropertyConfigurations.Entities;
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
            // Configuring Customer entity
            modelBuilder.Entity<Customer>(entity =>
            {
                // Configuring column name
                entity.Property(c => c.FirstName)
                      .HasColumnName("First_Name");

                // Configuring maximum length
                entity.Property(c => c.FirstName)
                      .HasMaxLength(100);

                // Configuring required property
                entity.Property(c => c.Email)
                      .IsRequired();

                // Configuring default value
                entity.Property(c => c.CreatedDate)
                      .HasDefaultValueSql("GETDATE()");

                //Configuring Identity
                entity.Property(c => c.Id)
                      .ValueGeneratedOnAdd();

                //Configuring Nullable Property
                entity.Property(p => p.PhoneNumber)
                    .IsRequired(false);
            });

            // Configuring Product entity
            modelBuilder.Entity<Product>(entity =>
            {
                // Configuring column name
                entity.Property(p => p.Name).HasColumnName("ProductName");

                //Configuring Column data type
                entity.Property(p => p.Price)
                    .HasColumnType("decimal(18,2)");

                //Ignoring the Description Property
                entity.Ignore(p => p.Description);

                // Configuring concurrency token
                entity.Property(p => p.RowVersion)
                    .IsRowVersion();

                // Configuring Shadow Property
                entity.Property<DateTime>("CreatedDate")
                    .HasDefaultValueSql("GETDATE()");
            });

            // Configuring Order entity
            modelBuilder.Entity<Order>(entity =>
            {
                // Configuring enum mapping to string, i.e., value conversion
                entity.Property(o => o.Status)
                    .HasConversion<string>();

                // Configuring default value for CreatedDate
                entity.Property(o => o.CreatedDate)
                    .HasDefaultValueSql("GETDATE()");

                // Configuring default value for Status
                entity.Property(o => o.Status)
                    .HasDefaultValue(OrderStatus.Pending);

                // Configuring concurrency token
                entity.Property(o => o.RowVersion)
                    .IsRowVersion();
            });

            // Configuring OrderItem entity
            modelBuilder.Entity<OrderItem>(entity =>
            {
                // Configuring precision and scale
                entity.Property(oi => oi.UnitPrice)
                    .HasPrecision(18, 2);

                // Configuring precision and scale
                entity.Property(oi => oi.TotalPrice)
                    .HasPrecision(18, 2);

                // Configuring computed column
                entity.Property(oi => oi.TotalPrice)
                    .HasComputedColumnSql("[UnitPrice] * [Quantity]");
            });
        }

        // Define DbSets
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}