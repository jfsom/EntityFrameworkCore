using Microsoft.EntityFrameworkCore;

namespace EFCoreCodeFirstDemo.Entities
{
    // EFCoreDbContext is your custom DbContext class that extends the base DbContext class provided by EF Core.
    public class EFCoreDbContext : DbContext
    {
        // The OnConfiguring method allows us to configure the DbContext options,
        // such as specifying the database provider and connection string.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Configuring the connection string to use a SQL Server database.
            // UseSqlServer is an extension method that configures the context to connect to a SQL Server database.
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