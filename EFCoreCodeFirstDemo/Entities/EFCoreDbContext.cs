using Microsoft.EntityFrameworkCore;
namespace EFCoreCodeFirstDemo.Entities
{
    public class EFCoreDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-RUC57UF;Database=StudentDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>() //Refers to the Employee Entity
                .HasOne(e => e.Manager) //Each employee has one Manager
                .WithMany(m => m.Subordinates) //Each Manager can have multiple Subordinates
                .HasForeignKey(e => e.ManagerId) //ManagerId is the Foreign Key
                .OnDelete(DeleteBehavior.Restrict);
        }
        public DbSet<Employee> Employees { get; set; }

    }
}