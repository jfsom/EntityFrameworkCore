using EFCoreCodeFirstDemo.Entities;
namespace EFCoreCodeFirstDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Initialize the DbContext
                using (var context = new EFCoreDbContext())
                {
                    // Define the search criteria (searching for a student with the first name "Alice")
                    string searchFirstName = "Alice";

                    // LINQ Query Syntax to search for a student by first name
                    var searchResultQS = (from student in context.Students
                                          where student.FirstName == searchFirstName
                                          select student).ToList();

                    // LINQ Method Syntax to search for a student by first name
                    var searchResultMS = context.Students //accesses the Students DbSet
                                              .Where(s => s.FirstName == searchFirstName) //filters students with the given first name
                                              .ToList(); //executes the query and returns the result as a list

                    // Check if any student is found
                    if (searchResultQS.Any())
                    {
                        // Iterate through the result and display the student's details
                        foreach (var student in searchResultQS)
                        {
                            Console.WriteLine($"Student Found: {student.FirstName} {student.LastName}, Email: {student.Email}");
                        }
                    }
                    else
                    {
                        // Output if no student is found
                        Console.WriteLine("No student found with the given first name.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Exception handling: log the exception message
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}