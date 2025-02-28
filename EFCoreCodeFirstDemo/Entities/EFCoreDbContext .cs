using Microsoft.EntityFrameworkCore;
namespace EFCoreCodeFirstDemo.Entities
{
    public class EFCoreDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Configuring the database connection
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-RUC57UF;Database=StudentDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlogPost>().Property<DateTime>("CreatedAt");
            modelBuilder.Entity<BlogPost>().Property<DateTime>("LastUpdatedAt");
        }

        public override int SaveChanges()
        {
            var timestamp = DateTime.UtcNow;
            foreach (var entry in ChangeTracker.Entries<BlogPost>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("CreatedAt").CurrentValue = timestamp;
                    entry.Property("LastUpdatedAt").CurrentValue = timestamp;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Property("LastUpdatedAt").CurrentValue = timestamp;
                }
            }
            return base.SaveChanges();
        }
        public DbSet<BlogPost> BlogPosts { get; set; }
    }
}