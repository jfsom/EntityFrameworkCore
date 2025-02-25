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
                // Step 1: Perform a bulk update for students in the "CSE" branch
                Console.WriteLine("Updating all students with Branch = 'CSE'...");
                BulkUpdate("CSE");

                // Step 2: Display the updated students in the "CSE" branch
                Console.WriteLine("\nFetching and displaying updated students with Branch = 'CSE':");
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

        // Method to perform bulk update of students based on their branch
        public static void BulkUpdate(string branch)
        {
            using (var context = new EFCoreDbContext())
            {
                // Fetch students with the specified branch
                var studentsList = context.Students
                    .Where(std => EF.Functions.Like(std.Branch, branch))
                    .ToList();

                // Check if any students were found before updating
                if (studentsList.Any())
                {
                    // Update properties for each student
                    foreach (var student in studentsList)
                    {
                        student.FirstName += "Changed";
                        student.LastName += "Changed";
                    }

                    // Save changes to the database (generates individual UPDATE statements)
                    context.SaveChanges();

                    // Confirm successful update
                    Console.WriteLine($"{studentsList.Count} students have been updated successfully.");
                }
                else
                {
                    Console.WriteLine($"No students found in the '{branch}' branch to update.");
                }
            }
        }

        // Method to fetch and display students based on their branch
        public static void GetStudents(string branch)
        {
            using (var context = new EFCoreDbContext())
            {
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
}