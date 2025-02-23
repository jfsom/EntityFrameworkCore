using Microsoft.EntityFrameworkCore;
using EFCoreCodeFirstDemo.Entities;

namespace EFCoreCodeFirstDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Initialize the database context
                using (var context = new EFCoreDbContext())
                {
                    // Create managers and their subordinates
                    InsertEmployees(context);

                    // Fetch and display the tree structure
                    DisplayEmployeesTree(context);
                }
            }
            catch (DbUpdateException ex)
            {
                // Exception Database Exception
                Console.WriteLine($"Database Error: {ex.InnerException?.Message ?? ex.Message}");
            }
            catch (Exception ex)
            {
                // Exception handling to catch any errors
                Console.WriteLine($"Error occurred: {ex.Message}");
            }
        }

        // Method to insert a manager and their subordinates
        static void InsertEmployees(EFCoreDbContext context)
        {
            // Check if the database already has employees
            if (context.Employees.Any())
            {
                Console.WriteLine("Employees already exist in the database.\n");
                return;
            }

            // Create two manager employees
            var manager1 = new Employee { Name = "Alice Manager" };
            var manager2 = new Employee { Name = "Bob Manager" };

            // Create subordinates under manager1
            var subordinate1 = new Employee { Name = "Charlie Employee", Manager = manager1 };
            var subordinate2 = new Employee { Name = "David Employee", Manager = manager1 };

            // Create subordinates under manager2
            var subordinate3 = new Employee { Name = "Eve Employee", Manager = manager2 };
            var subordinate4 = new Employee { Name = "Frank Employee", Manager = manager2 };
            var subordinate5 = new Employee { Name = "Grace Employee", Manager = manager2 };

            // Add managers and subordinates to the context
            context.Employees.AddRange(manager1, manager2, subordinate1, subordinate2, subordinate3, subordinate4, subordinate5);

            // Save changes to the database
            context.SaveChanges();

            Console.WriteLine("Managers and their subordinates have been successfully added to the database.\n");
        }

        // Method to display employees in a tree structure
        static void DisplayEmployeesTree(EFCoreDbContext context)
        {
            // Retrieve managers along with their subordinates from the database
            var managers = context.Employees
                .Include(e => e.Subordinates) // Include subordinates in the query
                .Where(e => e.ManagerId == null) // Only select employees who are managers (no ManagerId)
                .ToList();

            // Display the managers and their subordinates
            foreach (var manager in managers)
            {
                Console.WriteLine($"Manager Id: {manager.EmployeeId}, Name: {manager.Name}");
                foreach (var subordinate in manager.Subordinates)
                {
                    Console.WriteLine($"\tSubordinate: Id:{subordinate.EmployeeId}, Name:{subordinate.Name}");
                }

                Console.WriteLine(); //Line Spacing for better viewability
            }
        }
    }
}