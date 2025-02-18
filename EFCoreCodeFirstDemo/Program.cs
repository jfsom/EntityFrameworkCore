using EFCoreCodeFirstDemo.Entities;

namespace EFCoreCodeFirstDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Create an instance of your EFCoreDbContext to interact with the database
                using (var context = new EFCoreDbContext())
                {
                    // Retrieve the student whose Id is 1 from the database using Find method
                    // The Find method takes the Primary Key
                    var student = context.Students.Find(1);

                    // Check if a student was found with ID 1
                    if (student != null)
                    {
                        // Display the original data before updating
                        Console.WriteLine("Original Student Data:");
                        Console.WriteLine($"Name: {student.FirstName} {student.LastName}");
                        Console.WriteLine($"Email: {student.Email}");

                        // Modify the student's properties that need to be updated
                        student.FirstName = "Paresh"; // Changing the first name
                        student.LastName = "Mohanty"; // Changing the last name
                        student.Email = "Paresh.Mohanty@dotnettutrials.net"; // Changing the email

                        // Save the changes to the database
                        context.SaveChanges();

                        // Display a success message on the console
                        Console.WriteLine("Student data has been successfully updated.");

                        // Display the updated data
                        Console.WriteLine("Updated Student Data:");
                        Console.WriteLine($"Name: {student.FirstName} {student.LastName}");
                        Console.WriteLine($"Email: {student.Email}");
                    }
                    else
                    {
                        // Display a message if no student was found
                        Console.WriteLine("No student found to update.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}"); ;
            }
        }
    }
}