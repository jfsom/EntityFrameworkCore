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
                // Create a new disconnected Student entity
                Student newStudent = new Student()
                {
                    FirstName = "Pranaya",
                    LastName = "Rout"
                };

                using var context = new EFCoreDbContext();

                // Determine the state based on StudentId
                if (newStudent.StudentId > 0)
                {
                    // Existing entity: set state to Modified
                    context.Entry(newStudent).State = EntityState.Modified;
                }
                else if (newStudent.StudentId == 0)
                {
                    // New entity: set state to Added
                    context.Entry(newStudent).State = EntityState.Added;
                }
                else
                {
                    throw new Exception("Invalid Student ID");
                }

                // Display the entity state before saving
                Console.WriteLine($"Before SaveChanges - Entity State: {context.Entry(newStudent).State}\n");

                // Persist changes to the database
                context.SaveChanges();

                // Display the entity state after saving
                Console.WriteLine($"\nAfter SaveChanges - Entity State: {context.Entry(newStudent).State}");

                // Display the Student Id
                Console.WriteLine($"Student ID: {newStudent.StudentId}");

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}