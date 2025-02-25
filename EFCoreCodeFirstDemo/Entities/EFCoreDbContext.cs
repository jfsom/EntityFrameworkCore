using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EFCoreCodeFirstDemo.Entities
{
    public class EFCoreDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Log the SQL queries to the console
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);

            string _connectionString = @"Server=DESKTOP-RUC57UF;Database=StudentDB;Trusted_Connection=True;TrustServerCertificate=True;";

            optionsBuilder.UseSqlServer(_connectionString, sqlOptions =>
            {
                sqlOptions.MaxBatchSize(100); // Set the batch size to 100
            });
        }

        public DbSet<Student> Students { get; set; }
    }
}