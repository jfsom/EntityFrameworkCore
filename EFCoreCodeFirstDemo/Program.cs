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
                // Create a list of new students to insert
                List<Student> newStudents = new List<Student>()
                {
                    new Student() { FirstName = "Pranaya", LastName = "Rout", Branch = "CSE" },
                    new Student() { FirstName = "Hina", LastName = "Sharma", Branch = "CSE" },
                    new Student() { FirstName = "Anurag", LastName = "Mohanty", Branch = "CSE" },
                    new Student() { FirstName = "Prity", LastName = "Tiwary", Branch = "ETC" }
                };

                using var context = new EFCoreDbContext();

                // Perform Bulk Insert using EF Extensions
                // Inserts all Student entities in the newStudents list into the database in a single, optimized operation.
                context.BulkInsert(newStudents);

                //No Need for SaveChanges():
                //The BulkInsert method handles database interactions internally, eliminating the need to call SaveChanges().
                Console.WriteLine("BulkInsert: Successfully inserted new students.");

                // Display all students belonging to the CSE branch
                DisplayStudentsByBranch("CSE");

                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"BulkInsert Error: {ex.Message}");
            }
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

            Console.WriteLine($"\nStudents in {branch} Branch:");
            foreach (var student in studentsList)
            {
                Console.WriteLine($"\tStudent ID: {student.StudentId}, Name: {student.FirstName} {student.LastName}, Branch: {student.Branch}");
            }
        }
    }
}