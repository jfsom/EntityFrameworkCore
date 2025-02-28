using Microsoft.EntityFrameworkCore;
namespace EFCoreCodeFirstDemo.Entities
{
    public class EFCoreDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Configuring the connection string to the SQL Server database
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-RUC57UF;Database=StudentDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        // Configures the model and mappings between entities and database
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the base class as abstract to prevent EF Core from creating a separate table
            modelBuilder.Entity<Invoice>().UseTpcMappingStrategy();
            // Configure TPC inheritance by mapping each concrete class to its own table
            modelBuilder.Entity<UtilityBill>();
            modelBuilder.Entity<ProductPurchase>();
            modelBuilder.Entity<SubscriptionService>();
        }
        // DbSets representing each table in the database
        public DbSet<Invoice> Invoices { get; set; } //No Table for this Property
        public DbSet<UtilityBill> UtilityBills { get; set; }
        public DbSet<ProductPurchase> ProductPurchases { get; set; }
        public DbSet<SubscriptionService> SubscriptionServices { get; set; }

    }
}