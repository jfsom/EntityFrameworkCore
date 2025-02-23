using Microsoft.EntityFrameworkCore;
namespace EFCoreCodeFirstDemo.Entities
{
    public class EFCoreDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-RUC57UF;Database=StudentDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Passport> Passports { get; set; }

    }
}