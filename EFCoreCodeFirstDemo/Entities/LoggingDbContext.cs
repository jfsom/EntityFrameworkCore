using Microsoft.EntityFrameworkCore;
namespace EFCoreCodeFirstDemo.Entities
{
    public class LoggingDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Configure the connection string to your Logging SQL Server Database
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-RUC57UF;Database=LoggingBD;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        // DbSet for logging entities
        public DbSet<OrderLog> OrderLogs { get; set; }
    }
}
