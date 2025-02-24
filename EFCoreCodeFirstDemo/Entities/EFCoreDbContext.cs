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
            // Configure ApplicationLog entity to have no primary key
            modelBuilder.Entity<ApplicationLog>()
                        .HasNoKey(); // Specifies that the ApplicationLog entity does not have a primary key

            // Optionally map the entity to a specific table name
            modelBuilder.Entity<ApplicationLog>().ToTable("Logs");
        }

        // DbSet for keyless table
        public DbSet<ApplicationLog> ApplicationLogs { get; set; }
    }
}