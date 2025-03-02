using Microsoft.EntityFrameworkCore;
namespace EFCoreCodeFirstDemo.Entities
{
    public class EFCoreDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-RUC57UF;Database=StudentDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        //Overriding the OnModelCreating method to add seed data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seeding Product data
            modelBuilder.Entity<Product>().HasData(
                    new Product { ProductId = 1, Name = "Laptop", Price = 5000, StockQuantity = 10 },
                    new Product { ProductId = 2, Name = "Desktop", Price = 3000, StockQuantity = 15 },
                    new Product { ProductId = 3, Name = "Mobile", Price = 1500, StockQuantity = 20 }
                );
        }
        public DbSet<Product> Products { get; set; }
    }
}