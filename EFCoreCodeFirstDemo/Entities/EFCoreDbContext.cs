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
            modelBuilder.Entity<Student>()
                .HasMany(s => s.Courses) // Student can enroll in many Courses
                .WithMany(c => c.Students) // Course can have many Students
                .UsingEntity(j => j.ToTable("StudentCourses"));  //Explicitly set the join table name
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }

    }
}