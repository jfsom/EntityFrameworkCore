using EFCoreCodeFirstDemo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
namespace EFCoreCodeFirstDemo.Entities
{
    public class EFCoreDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // To Display the Generated the Database Script
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);

            optionsBuilder.UseSqlServer(@"Server=DESKTOP-RUC57UF;Database=StudentDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        //Overriding the OnModelCreating method to add seed data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seeding Student data
            modelBuilder.Entity<Student>().HasData(
                    new Student { StudentId = 1, Name = "Pranaya", Branch = "CSE", RegdNumber = 1001 },
                    new Student { StudentId = 2, Name = "Hina", Branch = "CSE", RegdNumber = 1002 },
                    new Student { StudentId = 3, Name = "Rakesh", Branch = "CSE", RegdNumber = 1003 }
                );
        }
        public DbSet<Student> Students { get; set; }
    }
}