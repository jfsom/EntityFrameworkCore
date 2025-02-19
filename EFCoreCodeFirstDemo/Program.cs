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
                    // Fetch the student from the database
                    var student = context.Students.FirstOrDefault(s => s.StudentId == 1);

                    if (student != null)
                    {
                        Console.WriteLine($"Initial State: {context.Entry(student).State}");

                        // Update the student's phone number
                        student.PhoneNumber = "555-123-4567";

                        // EF Core will automatically mark the entity as Modified
                        Console.WriteLine($"State after modifying PhoneNumber: {context.Entry(student).State}");

                        // Save changes to the database
                        context.SaveChanges();
                        Console.WriteLine("Student phone number updated successfully.");

                        // State after saving changes should be Unchanged
                        Console.WriteLine($"State after SaveChanges: {context.Entry(student).State}");
                    }
                    else
                    {
                        Console.WriteLine("Student not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}