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
                Console.WriteLine("Attempting to add a student without a name...");

                // Creating a Student object without setting the Name property
                Student student = new Student
                {
                    Address = "123 Main St",
                    RollNumber = 101
                };

                using var context = new EFCoreDbContext();

                // Adding the student to the context
                context.Add(student);

                // Attempting to save changes to the database
                context.SaveChanges();

                Console.WriteLine("Student added successfully.");
            }
            catch (DbUpdateException dbEx)
            {
                Console.WriteLine($"Database Update Error: {dbEx.InnerException?.Message ?? dbEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}