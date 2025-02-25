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
                List<Student> newStudents = new List<Student>();
                for (int i = 1; i <= 200; i++)
                {
                    newStudents.Add(new Student() { FirstName = $"Pranaya-{i}", LastName = $"Rout-{i}", Branch = "CSE" });
                }

                // Step 2: Perform a bulk insert of the students
                Console.WriteLine("Inserting new students into the database...");
                BulkInsert(newStudents);

                Console.WriteLine("Inserting new students into the database completed...");
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
                // Step 1: Add the list of students to the context
                context.Students.AddRange(newStudents);

                // Step 2: Save the changes to the database (MERGE statement generated)
                context.SaveChanges();

                // Output confirmation of successful insertion
                Console.WriteLine($"{newStudents.Count} students have been inserted successfully.");
            }
        }
    }
}