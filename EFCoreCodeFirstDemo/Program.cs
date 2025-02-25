using EFCoreCodeFirstDemo.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCoreCodeFirstDemo
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                Console.WriteLine("Starting BulkMerge operation...");

                // Perform Bulk Merge operation
                await BulkMergeAsync();

                Console.WriteLine("BulkMerge operation completed.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        private static async Task BulkMergeAsync()
        {
            using var context = new EFCoreDbContext();

            // Existing list of students to merge (sync) with the database
            List<Student> studentsToMerge = new List<Student>
            {
                // Assume that StudentId 1 exists and will be updated
                new Student { StudentId = 1, FirstName = "John", LastName = "Doe Updated", Branch = "CSE" },
                
                // This is a new student that will be inserted
                new Student { FirstName = "Ramesh", LastName = "Sethy", Branch = "CSE" }
            };

            // Perform BulkMerge operation:
            // - Updates student with StudentId = 1
            // - Inserts new student without an ID
            await context.BulkMergeAsync(studentsToMerge);

            // Display current students in the database after the merge
            await DisplayStudentsAsync();
        }

        private static async Task DisplayStudentsAsync()
        {
            using var context = new EFCoreDbContext();
            var students = await context.Students.ToListAsync();
            Console.WriteLine("Current Students in the database:");
            foreach (var student in students)
            {
                Console.WriteLine($"\tID: {student.StudentId}, Name: {student.FirstName} {student.LastName}, Branch: {student.Branch}");
            }
            Console.WriteLine();  // Blank line for better readability
        }
    }
}