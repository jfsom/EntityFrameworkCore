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
                // Step 1: Create a list of new students to insert
                List<Student> newStudents = new List<Student>()
                {
                    new Student() { FirstName = "Pranaya", LastName = "Rout", Branch = "CSE" },
                    new Student() { FirstName = "Hina", LastName = "Sharma", Branch = "CSE" },
                    new Student() { FirstName = "Anurag", LastName = "Mohanty", Branch = "CSE" },
                    new Student() { FirstName = "Prity", LastName = "Tiwary", Branch = "ETC" }
                };

                // Step 2: Perform a bulk insert of the students
                Console.WriteLine("Inserting new students into the database...");
                BulkInsert(newStudents);

                // Step 3: Display students with the branch "CSE"
                Console.WriteLine("\nFetching and displaying students with Branch = 'CSE':");
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

        // Method to perform bulk insert operation
        public static void BulkInsert(IList<Student> newStudents)
        {
            using (var context = new EFCoreDbContext())
            {
                // Add the list of students to the context
                context.Students.AddRange(newStudents);

                // Save changes to the database (generates MERGE statement)
                context.SaveChanges();

                // Confirm successful insertion
                Console.WriteLine($"{newStudents.Count} students have been inserted successfully.");
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