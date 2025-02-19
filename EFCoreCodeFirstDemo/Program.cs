using EFCoreCodeFirstDemo.Entities;
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
                    // Sorting students by Gender ascending and EnrollmentDate descending using Query Syntax
                    var sortedStudentsQuerySyntax = (from student in context.Students
                                                     orderby student.Gender ascending, student.EnrollmentDate descending
                                                     select student).ToList();

                    // Sorting students by LastName ascending and EnrollmentDate descending using Method Syntax
                    var sortedStudentsMethodSyntax = context.Students
                                                            .OrderBy(s => s.Gender) // Primary sort by Gender in ascending order
                                                            .ThenByDescending(s => s.EnrollmentDate) // Secondary sort by EnrollmentDate in descending order
                                                            .ToList();
                    // Check if any students are found
                    if (sortedStudentsQuerySyntax.Any())
                    {
                        // Iterate through the sorted students and display their details
                        foreach (var student in sortedStudentsQuerySyntax)
                        {
                            // Output the student's details including Gender and enrollment date
                            Console.WriteLine($"Student: {student.LastName} {student.FirstName}, Gender: {student.Gender}, Enrollment Date: {student.EnrollmentDate.ToShortDateString()}");
                        }
                    }
                    else
                    {
                        // Output if no students are found
                        Console.WriteLine("No students found.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Exception handling: log the exception message
                // This catches any errors that occur during database access or LINQ operations
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}