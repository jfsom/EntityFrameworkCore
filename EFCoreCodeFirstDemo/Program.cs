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
                    // Fetching students from the database who have an email with "dotnettutorials.com" domain
                    var studentsToProcess = context.Students
                                                   .Where(s => s.Email.Contains("dotnettutorials.com"))
                                                   .ToList();

                    foreach (var student in studentsToProcess)
                    {
                        // Update the student's Email address by replacing the domain with "example.com"
                        student.Email = ReplaceDomain(student.Email, "example.com");

                        // Save changes to the database
                        context.SaveChanges();
                        Console.WriteLine($"StudentId {student.StudentId} email updated to '{student.Email}'");

                        // Detach the student to free up memory after processing
                        context.Entry(student).State = EntityState.Detached;
                        Console.WriteLine($"Detached StudentId {student.StudentId}, State: {context.Entry(student).State}");
                    }

                    Console.WriteLine("All students processed successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        // Helper method to replace the email domain
        static string ReplaceDomain(string email, string newDomain)
        {
            var atIndex = email.IndexOf('@');
            if (atIndex >= 0)
            {
                return email.Substring(0, atIndex + 1) + newDomain;
            }
            return email; // In case the email doesn't contain '@', return it as is
        }
    }
}