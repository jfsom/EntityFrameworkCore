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
                // Create a student object with an existing StudentId (disconnected entity)
                Student student = new Student()
                {
                    StudentId = 1 // Assume this ID exists in the database
                };

                using var context = new EFCoreDbContext();

                // Set the entity state to Deleted
                context.Entry(student).State = EntityState.Deleted;

                // Display the entity state before saving
                Console.WriteLine($"Before SaveChanges - Entity State: {context.Entry(student).State}\n");

                // Persist changes to the database
                context.SaveChanges();

                // Display the entity state after saving
                Console.WriteLine($"\nAfter SaveChanges - Entity State: {context.Entry(student).State}");

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}