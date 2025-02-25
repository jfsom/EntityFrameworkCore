using Microsoft.EntityFrameworkCore;
using EFCoreCodeFirstDemo.Entities;

namespace EFCoreCodeFirstDemo
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                Console.WriteLine("Starting BulkSaveChanges operation...");

                // Perform Bulk SaveChanges operation
                await BulkSaveChangesAsync();

                Console.WriteLine("BulkSaveChanges operation completed.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        private static async Task BulkSaveChangesAsync()
        {
            using var context = new EFCoreDbContext();

            // Fetch existing students from the database
            var existingStudents = await context.Students.Where(s => s.Branch == "CSE").ToListAsync();

            // Updating existing students (append 'Updated' to their names)
            foreach (var student in existingStudents)
            {
                student.FirstName += " Updated";
                student.LastName += " Updated";
            }

            // Adding a new student to the context
            context.Students.Add(new Student { FirstName = "New", LastName = "Student", Branch = "ETC" });

            // Deleting a student from the context (this will be batched in the BulkSaveChanges)
            var studentToDelete = await context.Students.Where(s => s.FirstName == "Sethy").FirstOrDefaultAsync();
            if (studentToDelete != null)
            {
                context.Students.Remove(studentToDelete);
            }

            // Perform BulkSaveChanges to save all updates, inserts, and deletes in one go
            await context.BulkSaveChangesAsync();

            // Display current students in the database after the save
            await DisplayStudentsAsync();
        }

        private static async Task DisplayStudentsAsync()
        {
            using var context = new EFCoreDbContext();
            var students = await context.Students.ToListAsync();
            Console.WriteLine("Current Students in the database:");
            foreach (var student in students)
            {
                Console.WriteLine($"ID: {student.StudentId}, Name: {student.FirstName} {student.LastName}, Branch: {student.Branch}");
            }
            Console.WriteLine();  // Blank line for better readability
        }
    }
}