using EFCoreCodeFirstDemo.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCoreCodeFirstDemo
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Starting BulkUpdate Operation...");

                // Specify the branch to update
                string branchToUpdate = "CSE";

                // Perform Bulk Update
                BulkUpdateStudents(branchToUpdate);

                Console.WriteLine("BulkUpdate: Successfully updated student records.");

                // Display updated students to verify changes
                DisplayStudentsByBranch(branchToUpdate);

                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"BulkUpdate Error: {ex.Message}");
            }
        }

        //Updates the first and last names of students in the specified branch.
        public static void BulkUpdateStudents(string branch)
        {
            using var context = new EFCoreDbContext();

            // Fetch students belonging to the specified branch
            var studentsToUpdate = context.Students
                                         .Where(std => std.Branch == branch)
                                         .ToList();

            // Modify the desired properties for each student
            foreach (var student in studentsToUpdate)
            {
                student.FirstName += " Updated";
                student.LastName += " Updated";
            }

            // Perform Bulk Update using EF Extensions
            context.BulkUpdate(studentsToUpdate);
        }

        // Retrieves and displays students from a specified branch.
        public static void DisplayStudentsByBranch(string branch)
        {
            using var context = new EFCoreDbContext();

            // Fetch all students where Branch matches the specified value
            var studentsList = context.Students
                                      .AsNoTracking() // Improves performance for read-only operations
                                      .Where(std => std.Branch == branch)
                                      .ToList();

            Console.WriteLine($"\nUpdated Students in {branch} Branch:");
            foreach (var student in studentsList)
            {
                Console.WriteLine($"\tID: {student.StudentId}, First Name: {student.FirstName}, Last Name: {student.LastName}, Branch: {student.Branch}");
            }
        }
    }
}