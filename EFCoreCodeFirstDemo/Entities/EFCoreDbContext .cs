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
            // Configure the Country entity
            modelBuilder.Entity<Country>(entity =>
            {
                // Set the primary key
                entity.HasKey(c => c.CountryId);
                // Configure the one-to-many relationship between Country and State
                entity.HasMany(c => c.States) // A Country has many States
                      .WithOne(s => s.Country) // Each State has one Country
                      .HasForeignKey(s => s.CountryId) // Foreign key in State table
                      .OnDelete(DeleteBehavior.Cascade); // Enable Cascade Delete
            });
            // Configure the State entity
            modelBuilder.Entity<State>(entity =>
            {
                // Set the primary key
                entity.HasKey(s => s.StateId);
                // Configure the one-to-many relationship between State and City
                entity.HasMany(s => s.Cities) // A State has many Cities
                      .WithOne(c => c.State) // Each City has one State
                      .HasForeignKey(c => c.StateId) // Foreign key in City table
                      .OnDelete(DeleteBehavior.Cascade); // Enable Cascade Delete
            });
        }
        // DbSets representing the tables
        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }
    }
}