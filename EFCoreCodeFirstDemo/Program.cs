using EFCoreCodeFirstDemo.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCoreCodeFirstDemo
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("=== EF Core Asynchronous CRUD Operations with EF Extensions ===\n");

            try
            {
                // Initialize the DbContext
                using var context = new EFCoreDbContext();

                // 1. Create (Bulk Insert) Operation
                await BulkInsertStudentsAsync(context);

                // 2. Read Operation
                await DisplayStudentsByBranchAsync(context, "CSE");

                // 3. Update (Bulk Update) Operation
                await BulkUpdateStudentsAsync(context, "CSE");

                // 4. Read Operation to verify updates
                await DisplayStudentsByBranchAsync(context, "CSE");

                // 5. Delete (Bulk Delete) Operation
                await BulkDeleteStudentsAsync(context, "ETC");

                // 6. Read Operations to verify deletions
                await DisplayStudentsByBranchAsync(context, "CSE");
                await DisplayStudentsByBranchAsync(context, "ETC");
            }
            catch (Exception ex)
            {
                // Handle any unexpected exceptions
                Console.WriteLine($"\nAn unexpected error occurred: {ex.Message}");
            }
        }

        // Performs bulk insert of new students asynchronously.
        private static async Task BulkInsertStudentsAsync(EFCoreDbContext context)
        {
            Console.WriteLine("1. Starting Bulk Insert Operation...");

            // Define a list of new students to insert
            List<Student> newStudents = new List<Student>()
            {
                new Student() { FirstName = "Alice", LastName = "Johnson", Branch = "CSE" },
                new Student() { FirstName = "Bob", LastName = "Smith", Branch = "CSE" },
                new Student() { FirstName = "Charlie", LastName = "Brown", Branch = "ETC" },
                new Student() { FirstName = "Diana", LastName = "Prince", Branch = "ETC" }
            };

            try
            {
                // Perform Bulk Insert asynchronously
                await context.BulkInsertAsync(newStudents);

                Console.WriteLine("Bulk Insert: Successfully inserted new students.\n");
            }
            catch (Exception ex)
            {
                // Handle exceptions related to bulk insert
                Console.WriteLine($"Bulk Insert Error: {ex.Message}\n");
            }
        }

        // Displays students belonging to a specific branch asynchronously.
        private static async Task DisplayStudentsByBranchAsync(EFCoreDbContext context, string branch)
        {
            Console.WriteLine($"2. Retrieving Students in '{branch}' Branch...\n");

            try
            {
                // Fetch students asynchronously with no tracking for performance
                var studentsList = await context.Students
                                               .AsNoTracking()
                                               .Where(std => std.Branch == branch)
                                               .ToListAsync();

                if (studentsList.Any())
                {
                    Console.WriteLine($"Students in '{branch}' Branch:");
                    Console.WriteLine("-------------------------------------------------");
                    foreach (var student in studentsList)
                    {
                        Console.WriteLine($"\tID: {student.StudentId}, Name: {student.FirstName} {student.LastName}, Branch: {student.Branch}");
                    }
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine($"No students found in '{branch}' Branch.\n");
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions related to data retrieval
                Console.WriteLine($"Read Error: {ex.Message}\n");
            }
        }

        // Performs bulk update of students' names asynchronously.
        private static async Task BulkUpdateStudentsAsync(EFCoreDbContext context, string branch)
        {
            Console.WriteLine("3. Starting Bulk Update Operation...");

            try
            {
                // Fetch students asynchronously
                var studentsToUpdate = await context.Students
                                                   .Where(std => std.Branch == branch)
                                                   .ToListAsync();

                if (studentsToUpdate.Any())
                {
                    // Modify the desired properties for each student
                    foreach (var student in studentsToUpdate)
                    {
                        student.FirstName += " Updated";
                        student.LastName += " Updated";
                    }

                    // Perform Bulk Update asynchronously
                    await context.BulkUpdateAsync(studentsToUpdate);

                    Console.WriteLine("Bulk Update: Successfully updated student records.\n");
                }
                else
                {
                    Console.WriteLine($"No students found in '{branch}' Branch to update.\n");
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions related to bulk update
                Console.WriteLine($"Bulk Update Error: {ex.Message}\n");
            }
        }

        // Performs bulk delete of students belonging to a specific branch asynchronously.
        private static async Task BulkDeleteStudentsAsync(EFCoreDbContext context, string branch)
        {
            Console.WriteLine("4. Starting Bulk Delete Operation...");

            try
            {
                // Fetch students asynchronously
                var studentsToDelete = await context.Students
                                                   .Where(std => std.Branch == branch)
                                                   .ToListAsync();

                if (studentsToDelete.Any())
                {
                    // Perform Bulk Delete asynchronously
                    await context.BulkDeleteAsync(studentsToDelete);

                    Console.WriteLine($"Bulk Delete: Successfully deleted students from '{branch}' Branch.\n");
                }
                else
                {
                    Console.WriteLine($"No students found in '{branch}' Branch to delete.\n");
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions related to bulk delete
                Console.WriteLine($"Bulk Delete Error: {ex.Message}\n");
            }
        }
    }
}