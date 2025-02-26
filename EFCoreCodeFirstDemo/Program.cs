using EFCoreCodeFirstDemo.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCoreCodeFirstDemo
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Create a disconnected Student entity with an existing StudentId
                Student existingStudent = new Student()
                {
                    StudentId = 1, // Ensure this ID exists in the database
                    FirstName = "Pranaya",
                    LastName = "Rout Updated"
                };

                using var context = new EFCoreDbContext();

                // Determine the state based on StudentId
                if (existingStudent.StudentId > 0)
                {
                    // Existing entity: set state to Modified
                    context.Entry(existingStudent).State = EntityState.Modified;
                }
                else if (existingStudent.StudentId == 0)
                {
                    // New entity: set state to Added
                    context.Entry(existingStudent).State = EntityState.Added;
                }
                else
                {
                    throw new Exception("Invalid Student ID");
                }

                // Display the entity state before saving
                Console.WriteLine($"Before SaveChanges - Entity State: {context.Entry(existingStudent).State}\n");

                // Persist changes to the database
                context.SaveChanges();

                // Display the entity state after saving
                Console.WriteLine($"\nAfter SaveChanges - Entity State: {context.Entry(existingStudent).State}");

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}