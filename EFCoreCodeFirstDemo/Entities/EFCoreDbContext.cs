using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EFCoreCodeFirstDemo.Entities
{
    // EFCoreDbContext is your custom DbContext class that extends the base DbContext class provided by EF Core.
    public class EFCoreDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Display the generated SQL queries in the Console window
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);

            // Configure the connection string
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-RUC57UF;Database=EFCoreDB1;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        // DbSet<Student> corresponds to the Students table in the database.
        // It allows EF Core to track and manage Student entities.
        public DbSet<Student> Students { get; set; }

        // DbSet<Branch> corresponds to the Branches table in the database.
        // It allows EF Core to track and manage Branch entities.
        public DbSet<Branch> Branches { get; set; }
    }
}