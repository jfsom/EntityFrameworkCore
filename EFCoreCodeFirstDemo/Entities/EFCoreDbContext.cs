using Microsoft.EntityFrameworkCore;
namespace EFCoreCodeFirstDemo.Entities
{
    public class EFCoreDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-RUC57UF;Database=StudentDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        public DbSet<Employee> Employees { get; set; }

    }
}