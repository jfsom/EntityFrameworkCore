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
                using (var context = new EFCoreDbContext())
                {
                    // Grouping students by their Branch using Query Syntax
                    var groupedStudentsQuerySyntax = (from student in context.Students
                                                     .Include(s => s.Branch) // Eager loading of the Branch property
                                                      group student by student.Branch.BranchName into studentGroup //Group Students by BranchName into studentGroup
                                                      select new
                                                      {
                                                          // studentGroup.Key is the BranchName in this case
                                                          BranchName = studentGroup.Key,

                                                          // Count the number of students in each group
                                                          StudentCount = studentGroup.Count()
                                                      }).ToList();

                    // Grouping students by their Branch using Method Syntax
                    //var groupedStudentsMethodSyntax = context.Students
                    //                                         .Include(s => s.Branch) // Eager loading of the Branch property
                    //                                         .GroupBy(s => s.Branch.BranchName) // Group students by BranchName
                    //                                         .Select(g => new
                    //                                         {
                    //                                             // g.Key is the BranchName in this case
                    //                                             BranchName = g.Key,

                    //                                             // Count the number of students in each group
                    //                                             StudentCount = g.Count()
                    //                                         })
                    //                                         .ToList();

                    // Check if any groups are found
                    if (groupedStudentsQuerySyntax.Any())
                    {
                        // Iterate through the grouped students and display their details
                        foreach (var group in groupedStudentsQuerySyntax)
                        {
                            // Output the Branch name and the number of students in that branch
                            Console.WriteLine($"\nBranch: {group.BranchName}, Number of Students: {group.StudentCount}");
                        }
                    }
                    else
                    {
                        // Output if no students are found
                        Console.WriteLine("No students found.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Exception handling: log the exception message
                // This catches any errors that occur during database access or LINQ operations
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}