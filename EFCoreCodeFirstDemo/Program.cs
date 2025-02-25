using EFCoreCodeFirstDemo.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCoreCodeFirstDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Step 1: Perform bulk delete for students in the "CSE" branch
                Console.WriteLine("Deleting all students with Branch = 'CSE'...");
                BulkDelete("CSE");

                // Step 2: Attempt to fetch and display students in the "CSE" branch post-deletion
                Console.WriteLine("\nFetching students with Branch = 'CSE' after deletion attempt:");
                GetStudents("CSE");
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Database Error: {ex.InnerException?.Message ?? ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
            }
        }

        // Method to perform bulk delete of students based on their branch
        public static void BulkDelete(string branch)
        {
            using var context = new EFCoreDbContext();

            // Fetch students with the specified branch
            var studentsList = context.Students
                .Where(std => EF.Functions.Like(std.Branch, branch))
                .ToList();

            // Check if any students were found before deleting
            if (studentsList.Any())
            {
                // Remove the fetched students from the context
                context.Students.RemoveRange(studentsList);

                // Save changes to the database (generates individual DELETE statements)
                context.SaveChanges();

                // Confirm successful deletion
                Console.WriteLine($"{studentsList.Count} students with Branch = '{branch}' have been deleted.");
            }
            else
            {
                Console.WriteLine($"No students found in the '{branch}' branch to delete.");
            }
        }

        // Method to fetch and display students based on their branch
        public static void GetStudents(string branch)
        {
            using var context = new EFCoreDbContext();

            // Fetch students with the specified branch (case-insensitive)
            var studentsList = context.Students
                .Where(std => EF.Functions.Like(std.Branch, branch))
                .ToList();

            // Display fetched students or notify if none are found
            if (studentsList.Any())
            {
                foreach (var student in studentsList)
                {
                    Console.WriteLine($"Student ID: {student.StudentId}, Name: {student.FirstName} {student.LastName}, Branch: {student.Branch}");
                }
            }
            else
            {
                Console.WriteLine($"No students found in the '{branch}' branch.");
            }
        }
    }
}