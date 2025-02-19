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
                // Simulating a list of student phone number updates received from an external source
                var studentUpdates = new List<Student>
                {
                    //Currently we have only one entity in the database with Id 1
                    new Student { StudentId = 1, PhoneNumber = "111-111-1111", Email="john.doe@dotnettutorials.com" }
                };

                using (var context = new EFCoreDbContext())
                {
                    foreach (var updatedStudent in studentUpdates)
                    {
                        // Initial state before attaching (Detached)
                        Console.WriteLine($"Before Attach: StudentId {updatedStudent.StudentId}, State: {context.Entry(updatedStudent).State}");

                        // Attach the student to the context (state should be Unchanged)
                        context.Students.Attach(updatedStudent);
                        Console.WriteLine($"After Attach: StudentId {updatedStudent.StudentId}, State: {context.Entry(updatedStudent).State}");

                        // Mark the PhoneNumber and Email properties as modified (state should be Modified)
                        context.Entry(updatedStudent).Property(s => s.PhoneNumber).IsModified = true;
                        context.Entry(updatedStudent).Property(s => s.Email).IsModified = true;

                        Console.WriteLine($"After Marking PhoneNumber as Modified: StudentId {updatedStudent.StudentId}, State: {context.Entry(updatedStudent).State}");
                    }

                    // Save all changes to the database in one batch
                    context.SaveChanges();
                    Console.WriteLine("Student Phone numbers and Emails Updated successfully.");

                    // Final state after saving (should be Unchanged)
                    foreach (var updatedStudent in studentUpdates)
                    {
                        Console.WriteLine($"After SaveChanges: StudentId {updatedStudent.StudentId}, State: {context.Entry(updatedStudent).State}");
                    }
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