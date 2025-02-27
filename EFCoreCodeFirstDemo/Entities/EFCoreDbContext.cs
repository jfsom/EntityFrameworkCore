using Microsoft.EntityFrameworkCore;
namespace EFCoreCodeFirstDemo.Entities
{
    public class EFCoreDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //// Configuring the connection string to the SQL Server database
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-RUC57UF;Database=StudentDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        // Configures the model and mappings between entities and database
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuring Table-Per-Hierarchy (TPH) inheritance for Payment entities
            modelBuilder.Entity<Payment>()
                .HasDiscriminator<string>("PaymentType") // Adds a discriminator column named 'PaymentType'
                .HasValue<CardPayment>("Card")          // Sets discriminator value 'Card' for CardPayment entities
                .HasValue<UPIPayment>("UPI")            // Sets discriminator value 'UPI' for UPIPayment entities
                .HasValue<CashOnDeliveryPayment>("COD"); // Sets discriminator value 'COD' for CashOnDeliveryPayment entities
        }

        // DbSet representing the Payments table in the database
        public DbSet<Payment> Payments { get; set; }
    }
}