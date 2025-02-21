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
            // Seeding Department data
            modelBuilder.Entity<Department>().HasData(
                new Department { DepartmentId = 1, Name = "Human Resources" },
                new Department { DepartmentId = 2, Name = "IT" }
            );

            // Seeding Employee data
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    EmployeeId = 1,
                    FirstName = "Hina",
                    LastName = "Sharma",
                    DateOfJoining = new DateTime(2015, 6, 1),
                    DateOfBirth = new DateTime(1990, 5, 15),
                    DepartmentId = 1 // Human Resources
                },
                new Employee
                {
                    EmployeeId = 2,
                    FirstName = "Pranaya",
                    LastName = "Rout",
                    DateOfJoining = new DateTime(2018, 4, 10),
                    DateOfBirth = new DateTime(1992, 8, 20),
                    DepartmentId = 2 // IT
                },
                new Employee
                {
                    EmployeeId = 3,
                    FirstName = "Rakesh",
                    LastName = "Singh",
                    DateOfJoining = new DateTime(2020, 3, 15),
                    DateOfBirth = new DateTime(1985, 12, 5),
                    DepartmentId = 2 // IT
                },
                new Employee
                {
                    EmployeeId = 4,
                    FirstName = "Priyanka",
                    LastName = "Tiwary",
                    DateOfJoining = new DateTime(2017, 11, 20),
                    DateOfBirth = new DateTime(1995, 3, 10),
                    DepartmentId = 1 // Human Resources
                }
            );
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}