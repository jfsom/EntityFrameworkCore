using EFCoreCodeFirstDemo.Entities;
using System.Diagnostics.Metrics;
using System.Diagnostics;

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
                    // When the student is retrieved, its initial state is Unchanged, indicating that no changes have been made.
                    var student = context.Students.FirstOrDefault(s => s.StudentId == 1);

                    if (student != null)
                    {
                        Console.WriteLine($"Initial State: {context.Entry(student).State}");

                        //The Remove method is called on the student entity.
                        //This method marks the entity as Deleted.
                        context.Students.Remove(student);

                        //After calling Remove, the entity’s state changes to Deleted, which is tracked by EF Core.
                        // The state should now be Deleted
                        Console.WriteLine($"State after marking for deletion: {context.Entry(student).State}");

                        // When SaveChanges() is called, EF Core generates a DELETE SQL statement to remove the student's record from the database.
                        // Save changes to the database (this will delete the record)
                        context.SaveChanges();

                        //After SaveChanges() completes, the entity is no longer tracked by the context because it has been deleted.
                        Console.WriteLine("Student record deleted successfully.");
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