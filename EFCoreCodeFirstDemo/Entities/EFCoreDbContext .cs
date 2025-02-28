using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
namespace EFCoreCodeFirstDemo.Entities
{
    public class EFCoreDbContext : DbContext
    {
        // Simulated Customer ID (In real applications, retrieve this from the authenticated user's context)
        private readonly int _currentCustomerId;

        public EFCoreDbContext(int currentCustomerId)
        {
            _currentCustomerId = currentCustomerId;
        }

        public EFCoreDbContext()
        {
        }

        // Configuring the database connection and logging
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Display the generated SQL queries in the console
            // optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);

            // Configuring the database connection
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-RUC57UF;Database=StudentDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        // Defining the global query filter in OnModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Global Query Filter to enforce cutsomer-based data isolation
            modelBuilder.Entity<Order>().HasQueryFilter(c => c.CustomerId == _currentCustomerId);
        }

        // DbSet representing Customers and Orders tables
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}