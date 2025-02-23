using EFCoreCodeFirstDemo.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCoreCodeFirstDemo
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Initialize the database context
                using (var context = new EFCoreDbContext())
                {
                    // Adding new Students and Courses
                    AddStudentsAndCourses(context);

                    // Fetching and displaying the data
                    DisplayStudentsAndCourses(context);
                }
            }
            catch (DbUpdateException dbex)
            {
                Console.WriteLine($"Database Error: {dbex.InnerException?.Message ?? dbex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Occurred: {ex.Message}");
            }
        }

        // Method to add students and courses
        static void AddStudentsAndCourses(EFCoreDbContext context)
        {
            // Creating new courses
            var course1 = new Course { CourseName = "ASP.NET Core" };
            var course2 = new Course { CourseName = "Machine Learning" };
            var course3 = new Course { CourseName = "Cloud Computing" };

            // Creating new students
            var student1 = new Student { Name = "Pranaya Rout" };
            var student2 = new Student { Name = "Rakesh Kumar" };
            var student3 = new Student { Name = "Anurag Mohanty" };

            // Creating StudentCourse join entities with EnrollmentDate
            var studentCourses = new List<StudentCourse>
            {
                new StudentCourse { Student = student1, Course = course1, EnrollmentDate = DateTime.Now },
                new StudentCourse { Student = student1, Course = course2, EnrollmentDate = DateTime.Now },
                new StudentCourse { Student = student2, Course = course2, EnrollmentDate = DateTime.Now },
                new StudentCourse { Student = student2, Course = course3, EnrollmentDate = DateTime.Now },
                new StudentCourse { Student = student3, Course = course1, EnrollmentDate = DateTime.Now },
                new StudentCourse { Student = student3, Course = course3, EnrollmentDate = DateTime.Now }
            };

            // Adding the students and courses via the join table
            context.StudentCourses.AddRange(studentCourses);

            // Save changes to the database
            context.SaveChanges();

            Console.WriteLine("Students, Courses, and Enrollments have been added to the database.\n");
        }

        // Method to fetch and display students with their enrolled courses and enrollment dates
        static void DisplayStudentsAndCourses(EFCoreDbContext context)
        {
            // Fetch all students and their related courses using Include for eager loading
            var students = context.Students     //Fetch the Students Data
                .Include(s => s.StudentCourses) // Eager Load the Related StudentCourses
                .ThenInclude(sc => sc.Course)   // Then Load the related Courses
                .ToList();

            // Iterate through each student and display the courses they are enrolled in
            foreach (var student in students)
            {
                Console.WriteLine($"Student Id: {student.Id}, Name: {student.Name}");

                // If the student is enrolled in any courses, display them
                if (student.StudentCourses.Any())
                {
                    Console.WriteLine("Enrolled in the following courses:");
                    foreach (var studentCourse in student.StudentCourses)
                    {
                        Console.WriteLine($"\tCourse Id:{studentCourse.Course.Id}, Name:{studentCourse.Course.CourseName}, Enrollment Date: {studentCourse.EnrollmentDate}");
                    }
                }
                else
                {
                    Console.WriteLine("No courses enrolled.");
                }

                Console.WriteLine(); // For spacing
            }
        }
    }
}