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
            // Seed data for the Expense entity
            modelBuilder.Entity<Expense>().HasData(
                new Expense { ExpenseId = 1, Department = "HR", Purpose = "Office Supplies", Amount = 1500, Date = new DateTime(2024, 9, 15) },
                new Expense { ExpenseId = 2, Department = "IT", Purpose = "Software License", Amount = 3500, Date = new DateTime(2024, 9, 16) },
                new Expense { ExpenseId = 3, Department = "IT", Purpose = "Team Lunch", Amount = 800, Date = new DateTime(2024, 9, 17) },
                new Expense { ExpenseId = 4, Department = "HR", Purpose = "Training", Amount = 2500, Date = new DateTime(2024, 9, 18) },
                new Expense { ExpenseId = 5, Department = "IT", Purpose = "Hardware Upgrade", Amount = 5500, Date = new DateTime(2024, 9, 19) }
            );
        }

        public DbSet<Expense> Expenses { get; set; }
        public DbSet<DepartmentExpenseReport> DepartmentExpenseReports { get; set; }
    }
}