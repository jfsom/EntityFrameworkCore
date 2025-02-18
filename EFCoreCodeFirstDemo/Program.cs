using EFCoreCodeFirstDemo.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCoreCodeFirstDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Create an instance of the DbContext class
                using var context = new EFCoreDbContext();

                GetAllStudents(context);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}"); ;
            }
        }

        private static void GetAllStudents(EFCoreDbContext context)
        {
            // Retrieve all students from the context
            var students = context.Students.Include(s => s.Branch).ToList();

            // Display the students in the console
            Console.WriteLine("All Students:");
            foreach (var student in students)
            {
                Console.WriteLine($"\t{student.StudentId}: {student.FirstName} {student.LastName}, Branch: {student.Branch?.BranchName}");
            }
        }
    }
}