using Microsoft.EntityFrameworkCore;
namespace EFCoreCodeFirstDemo.Entities
{
    public class EFCoreDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-RUC57UF;Database=StudentDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }
        // Override the OnModelCreating method to customize the model building process
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Using Fluent API to define entity relationships within the OnModelCreating method.
            // Start configuring the User entity
            modelBuilder.Entity<User>() //Refers to the User entity
                                        // Specifies that the User entity has a one-to-one relationship with a Passport entity, meaning each User has one Passport.
                .HasOne(u => u.Passport)
                // Specifies that the Passport entity is also related to exactly one User entity, making the relationship bidirectional.
                .WithOne(p => p.User)
                // Sets the UserId property in the Passport entity as the foreign key that references the User entity's primary key.
                .HasForeignKey<Passport>(p => p.UserId);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Passport> Passports { get; set; }

    }
}