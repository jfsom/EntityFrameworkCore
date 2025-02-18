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
                    // Retrieve the student with ID 1 from the database using LINQ's FirstOrDefault method
                    var student = context.Students.FirstOrDefault(s => s.StudentId == 1);

                    // Check if a student with ID 1 was found to avoid null reference exceptions
                    if (student != null)
                    {
                        // Display the student data before deletion
                        Console.WriteLine("Student Data Before Deletion:");
                        Console.WriteLine($"ID: {student.StudentId}");
                        Console.WriteLine($"Name: {student.FirstName} {student.LastName}");
                        Console.WriteLine($"Email: {student.Email}");

                        // Remove the student entity from the DbSet
                        // This marks the entity for deletion in the context
                        context.Students.Remove(student);

                        // Alternatively, you can use the DbContext.Remove method to remove entities
                        // context.Remove(student);

                        // Save the changes to the database
                        // This actually performs the DELETE operation in the database
                        context.SaveChanges();

                        // Display a success message on the console
                        Console.WriteLine("Student record has been successfully deleted from the database.");
                    }
                    else
                    {
                        // Display a message if no student with the specified ID was found
                        Console.WriteLine("No student found with ID 1 to delete.");
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