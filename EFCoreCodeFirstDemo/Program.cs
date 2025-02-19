using EFCoreCodeFirstDemo.Entities;
using Microsoft.EntityFrameworkCore;
namespace EFCoreCodeFirstDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Initialize the DbContext
                using (var context = new EFCoreDbContext())
                {
                    // Define the filtering criteria
                    string branchName = "Computer Science Engineering"; // Branch name filter
                    string gender = "Female"; // Gender filter

                    // LINQ Query Syntax to filter students by branch name and gender with eager loading
                    var filteredStudentsQS = (from student in context.Students
                                             .Include(s => s.Branch) // Eager loading of the Branch property
                                              where student.Branch.BranchName == branchName && student.Gender == gender
                                              select student).ToList();

                    // LINQ Method Syntax to filter students by branch name and gender with eager loading
                    var filteredStudents = context.Students
                                                  .Include(s => s.Branch) // Eager loading of the Branch property
                                                  .Where(s => s.Branch.BranchName == branchName && s.Gender == gender)
                                                  .ToList();

                    // Check if any students match the filtering criteria
                    if (filteredStudentsQS.Any())
                    {
                        // Iterate through the filtered students and display their details
                        foreach (var student in filteredStudentsQS)
                        {
                            Console.WriteLine($"Student Found: {student.FirstName} {student.LastName}, Branch: {student.Branch.BranchName}, Gender: {student.Gender}");
                        }
                    }
                    else
                    {
                        // Output if no students match the filtering criteria
                        Console.WriteLine("No students found matching the given criteria.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Exception handling: log the exception message
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}