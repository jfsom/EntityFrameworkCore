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
                    // Create a new Branch
                    var branch = new Branch
                    {
                        BranchName = "Computer Science",
                        Description = "Computer Science Department",
                        PhoneNumber = "123-456-7890",
                        Email = "cs@example.com"
                    };

                    // Create a new Student
                    var student = new Student
                    {
                        FirstName = "John",
                        LastName = "Doe",
                        DateOfBirth = new DateTime(2000, 1, 1),
                        Gender = "Male",
                        Email = "john.doe@example.com",
                        PhoneNumber = "555-555-5555",
                        EnrollmentDate = DateTime.Now,
                        Branch = branch
                    };

                    // Display the Student Entity state before adding to the context
                    Console.WriteLine($"Student Entity State before adding to the context: {context.Entry(student).State}");

                    // Add the student to the context
                    // Using DbSet Add Methid
                    context.Students.Add(student);

                    // Using DbContext Add Methid
                    // context.Add(student);

                    // Display the Student Entity state after adding to the context
                    Console.WriteLine($"Student Entity State after adding to the context: {context.Entry(student).State} \n");

                    // Save changes to the database
                    // This will save both Branch and Student entity to the database
                    context.SaveChanges();

                    // Display the Student Entity state after saving changes
                    Console.WriteLine($"\nStudent Entity State after saving changes: {context.Entry(student).State}");
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