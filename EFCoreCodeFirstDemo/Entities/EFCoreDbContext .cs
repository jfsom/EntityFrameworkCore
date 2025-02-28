using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
namespace EFCoreCodeFirstDemo.Entities
{
    public class EFCoreDbContext : DbContext
    {
        // Configuring the database connection and logging
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Display the generated SQL queries in the console
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);

            // Configuring the database connection
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-RUC57UF;Database=StudentDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        // Defining the global query filter in OnModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Global Query Filter to exclude soft-deleted orders
            modelBuilder.Entity<Order>().HasQueryFilter(o => !o.IsDeleted);
        }

        // DbSet representing Orders table
        public DbSet<Order> Orders { get; set; }
    }
}