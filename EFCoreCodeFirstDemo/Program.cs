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
                using (var context = new EFCoreDbContext())
                {
                    // Retrieve the Student with StudentId 1 from the database
                    var student = context.Students.Find(1);

                    if (student == null)
                    {
                        Console.WriteLine("Student with ID 1 not found.");
                        return;
                    }

                    // Display the state of the student after retrieval
                    Console.WriteLine($"Entity State after retrieval: {context.Entry(student).State}");

                    // Simulate calling SaveChanges without modifying the entity
                    context.SaveChanges();

                    Console.WriteLine("SaveChanges called. Since the entity was in the Unchanged state, no operations were performed on the database.");

                    Console.WriteLine($"Entity State after SaveChanges: {context.Entry(student).State}");
                }
            }
            catch (DbUpdateException dbEx)
            {
                Console.WriteLine($"Database update error: {dbEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}