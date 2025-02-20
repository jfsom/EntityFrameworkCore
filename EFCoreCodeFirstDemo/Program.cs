using EFCoreCodeFirstDemo.Entities;
namespace EFCoreCodeFirstDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new EFCoreDbContext())
            {
                try
                {
                    Console.WriteLine("\nExplicit Loading Student Related Data\n");

                    // Load a student (only student data is loaded initially)
                    var student = context.Students.FirstOrDefault(s => s.StudentId == 1);

                    // Display basic student information
                    if (student != null)
                    {
                        Console.WriteLine($"\nStudent Id: {student.StudentId}, Name: {student.FirstName} {student.LastName}, Gender: {student.Gender} \n");

                        // Explicitly load the Courses collection for the student
                        context.Entry(student).Collection(s => s.Courses).Load();

                        // Loop through the loaded courses and display course names
                        foreach (var course in student.Courses)
                        {
                            Console.WriteLine($"Course: {course.Name}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Student data not found.");
                    }
                }
                catch (Exception ex)
                {
                    // Handle any errors that occur during data retrieval
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }
    }
}