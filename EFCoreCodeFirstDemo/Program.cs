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
                Console.WriteLine("Attempting to add a student with an empty name...");

                // Creating a Student object with an empty Name
                Student student = new Student
                {
                    Name = string.Empty, // Empty string is allowed
                    Address = "456 Main St",
                    RollNumber = 102
                };

                using var context = new EFCoreDbContext();

                // Adding the student to the context
                context.Add(student);

                // Attempting to save changes to the database
                context.SaveChanges();

                Console.WriteLine("Student added successfully with an empty name.");
            }
            catch (DbUpdateException dbEx)
            {
                Console.WriteLine($"Database Error: {dbEx.InnerException?.Message ?? dbEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}