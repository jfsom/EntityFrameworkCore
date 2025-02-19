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
                    // Lazy Loading Example
                    Console.WriteLine("Lazy Loading Student and related data\n");

                    // Load a student (only student data is loaded initially)
                    var student = context.Students.FirstOrDefault(s => s.StudentId == 1);

                    // Display basic student information
                    Console.WriteLine($"\nStudent Id: {student?.StudentId}, Name: {student?.FirstName} {student?.LastName}, Gender: {student?.Gender} \n");

                    // Accessing the Branch property triggers lazy loading
                    // EF Core will issue a SQL query to load the related Branch
                    if (student != null)
                    {
                        Console.WriteLine($"\nBranch Location: {student.Branch?.BranchLocation}, Email: {student.Branch?.BranchEmail}, Phone: {student.Branch?.BranchPhoneNumber}  \n");

                        // Accessing the Address property triggers lazy loading
                        // EF Core will issue a SQL query to load the related Address
                        Console.WriteLine($"\nAddress: {student.Address?.Street}, {student.Address?.City}, {student.Address?.State}, Pin: {student.Address?.PostalCode} \n");

                        // Accessing the Courses collection triggers lazy loading
                        // EF Core will issue a SQL query to load the related Courses and their related Subjects
                        //foreach (var course in student.Courses)
                        //{
                        //    Console.WriteLine($"Course Enrolled: {course.Name}");
                        //You can also access the Subjects of each as follows
                        //foreach (var subject in course.Subjects)
                        //{
                        //    Console.WriteLine($"    Subject: {subject.SubjectName}");
                        //}
                        //}
                    }
                }
                catch (Exception ex)
                {
                    // Handle any errors that occur during data retrieval
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }

            // Final Output
            Console.WriteLine("\nLazy loading of related entities completed.");
        }
    }
}