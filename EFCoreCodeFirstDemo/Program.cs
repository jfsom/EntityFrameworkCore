using EFCoreCodeFirstDemo.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace EFCoreCodeFirstDemo
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Starting BulkDelete Operation...");

                // Specify the branch to delete students from
                string branchToDelete = "ETC";

                // Perform Bulk Delete
                BulkDeleteStudents(branchToDelete);

                Console.WriteLine("BulkDelete: Successfully deleted student records.");

                // Display remaining students to verify deletion
                DisplayStudentsByBranch("CSE");
                DisplayStudentsByBranch("ETC");

                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"BulkDelete Error: {ex.Message}");
            }
        }

        // Deletes all students belonging to the specified branch.
        public static void BulkDeleteStudents(string branch)
        {
            using var context = new EFCoreDbContext();

            // Fetch students belonging to the specified branch
            var studentsToDelete = context.Students
                                         .Where(std => std.Branch == branch)
                                         .ToList();

            // Perform Bulk Delete using EF Extensions
            context.BulkDelete(studentsToDelete);
        }

        // Retrieves and displays students from a specified branch.
        public static void DisplayStudentsByBranch(string branch)
        {
            using var context = new EFCoreDbContext();

            // Fetch all students where Branch matches the specified value
            var studentsList = context.Students
                                      .AsNoTracking() // Improves performance for read-only operations
                                      .Where(std => std.Branch == branch)
                                      .ToList();

            if (studentsList.Any())
            {
                Console.WriteLine($"\nStudents in {branch} Branch:");
                foreach (var student in studentsList)
                {
                    Console.WriteLine($"\tID: {student.StudentId}, Name: {student.FirstName} {student.LastName}, Branch: {student.Branch}");
                }
            }
            else
            {
                Console.WriteLine($"\nNo students found in {branch} Branch.");
            }
        }
    }
}