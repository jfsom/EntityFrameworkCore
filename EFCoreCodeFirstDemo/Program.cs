using EFCoreCodeFirstDemo.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCoreCodeFirstDemo
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                // Step 1: Perform a bulk insert of students asynchronously
                Console.WriteLine("Inserting students into the database asynchronously...");
                await BulkInsertAsync();

                // Step 2: Perform a bulk update of students asynchronously
                Console.WriteLine("\nUpdating students in the 'CSE' branch asynchronously...");
                await BulkUpdateAsync("CSE");

                // Step 3: Perform a bulk delete of students asynchronously
                Console.WriteLine("\nDeleting students in the 'CSE' branch asynchronously...");
                await BulkDeleteAsync("CSE");
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

        // Method to perform bulk insert asynchronously
        public static async Task BulkInsertAsync()
        {
            using var context = new EFCoreDbContext();

            // Create a list of new students to insert
            var newStudents = new List<Student>
            {
                new Student() { FirstName = "John", LastName = "Doe", Branch = "CSE" },
                new Student() { FirstName = "Jane", LastName = "Smith", Branch = "CSE" },
                new Student() { FirstName = "Mark", LastName = "Johnson", Branch = "CSE" },
                new Student() { FirstName = "Sara", LastName = "Connor", Branch = "IT" }
            };

            // Asynchronously add the list of students to the context
            await context.Students.AddRangeAsync(newStudents);

            // Asynchronously save changes to the database
            await context.SaveChangesAsync();

            // Confirm successful insertion
            Console.WriteLine($"{newStudents.Count} students have been inserted successfully.");
        }

        // Method to perform bulk update asynchronously
        public static async Task BulkUpdateAsync(string branch)
        {
            using var context = new EFCoreDbContext();

            // Asynchronously fetch students from the specified branch
            var studentsList = await context.Students
                .Where(s => EF.Functions.Like(s.Branch, branch))
                .ToListAsync();

            if (studentsList.Any())
            {
                // Update properties for each student
                foreach (var student in studentsList)
                {
                    student.FirstName += "Updated";
                    student.LastName += "Updated";
                }

                // Asynchronously save updated student details to the database
                await context.SaveChangesAsync();

                // Confirm successful update
                Console.WriteLine($"{studentsList.Count} students in the '{branch}' branch have been updated successfully.");
            }
            else
            {
                Console.WriteLine($"No students found in the '{branch}' branch to update.");
            }
        }

        // Method to perform bulk delete asynchronously
        public static async Task BulkDeleteAsync(string branch)
        {
            using var context = new EFCoreDbContext();

            // Asynchronously fetch students from the specified branch
            var studentsList = await context.Students
                .Where(s => EF.Functions.Like(s.Branch, branch))
                .ToListAsync();

            if (studentsList.Any())
            {
                // Mark the fetched students for deletion
                context.Students.RemoveRange(studentsList);

                // Asynchronously save changes to the database
                await context.SaveChangesAsync();

                // Confirm successful deletion
                Console.WriteLine($"{studentsList.Count} students in the '{branch}' branch have been deleted.");
            }
            else
            {
                Console.WriteLine($"No students found in the '{branch}' branch to delete.");
            }
        }
    }
}