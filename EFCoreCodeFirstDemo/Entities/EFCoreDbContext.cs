using Microsoft.EntityFrameworkCore;
namespace EFCoreCodeFirstDemo.Entities
{
    public class EFCoreDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Configuring the connection string to the SQL Server database
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-RUC57UF;Database=StudentDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        // Configures the model and mappings between entities and database
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Map each class in the hierarchy to its own table
            modelBuilder.Entity<Content>().ToTable("Contents");  // Base table for common properties
            modelBuilder.Entity<Article>().ToTable("Articles");  // Table for Articles
            modelBuilder.Entity<Video>().ToTable("Videos");  // Table for Videos
            modelBuilder.Entity<Image>().ToTable("Images");  // Table for Images

            // Configure enums to be stored as strings

            // For ContentType enum
            modelBuilder.Entity<Content>()
                .Property(c => c.ContentType)
                .HasConversion<string>()
                .IsRequired();    // Optional: Specify if the property is required

            // For ContentStatus enum
            modelBuilder.Entity<Content>()
                .Property(c => c.Status)
                .HasConversion<string>()
                .IsRequired();    // Optional: Specify if the property is required
        }

        // DbSets representing each table in the database
        public DbSet<Content> Contents { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}