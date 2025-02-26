using EFCoreCodeFirstDemo.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EFCoreCodeFirstDemo
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Initialize sample student data
                var student1 = new Student
                {
                    FirstName = "Pranaya",
                    LastName = "Rout",
                    Branch = "CSE",
                    Gender = "Male"
                };

                var student2 = new Student
                {
                    FirstName = "Hina",
                    LastName = "Sharma",
                    Branch = "CSE",
                    Gender = "Female"
                };

                // Add new students to the database using ExecuteSqlInterpolated
                int id1 = AddStudent(student1);
                Console.WriteLine($"Newly Added Student Id: {id1}");

                int id2 = AddStudent(student2);
                Console.WriteLine($"Newly Added Student Id: {id2}");

                // Retrieve a single student by ID using FromSqlInterpolated
                var retrievedStudent = GetStudentById(id1);
                if (retrievedStudent != null)
                {
                    Console.WriteLine($"\nRetrieved Student by Id {id1}:");
                    Console.WriteLine($"Id: {retrievedStudent.StudentId}, Name: {retrievedStudent.FirstName} {retrievedStudent.LastName}, Branch: {retrievedStudent.Branch}, Gender: {retrievedStudent.Gender}");
                }

                // Retrieve all students using FromSqlInterpolated
                var allStudents = GetAllStudents();
                Console.WriteLine("\nAll Students:");
                foreach (var student in allStudents)
                {
                    Console.WriteLine($"Id: {student.StudentId}, Name: {student.FirstName} {student.LastName}, Branch: {student.Branch}, Gender: {student.Gender}");
                }

                // Update an existing student using ExecuteSqlInterpolated
                if (retrievedStudent != null)
                {
                    retrievedStudent.FirstName = "Prateek";
                    retrievedStudent.LastName = "Sahoo";
                    UpdateStudent(retrievedStudent);

                    // Retrieve updated student
                    var updatedStudent = GetStudentById(retrievedStudent.StudentId);
                    Console.WriteLine("After Update:");
                    Console.WriteLine($"Id: {updatedStudent?.StudentId}, Name: {updatedStudent?.FirstName} {updatedStudent?.LastName}, Branch: {updatedStudent?.Branch}, Gender: {updatedStudent?.Gender}");
                }

                // Delete a student using ExecuteSqlInterpolated
                if (id2 > 0)
                {
                    DeleteStudent(id2);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        /// Adds a new student to the database using the spInsertStudent stored procedure with ExecuteSqlInterpolated.
        public static int AddStudent(Student student)
        {
            int newStudentId = 0;

            try
            {
                using var context = new EFCoreDbContext();

                var StudentIdOutParam = new SqlParameter
                {
                    ParameterName = "StudentId",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };

                // Execute the stored procedure using ExecuteSqlInterpolated for automatic parameterization
                int NumberOfRowsAffected = context.Database.ExecuteSqlInterpolated($@"
                    EXEC spInsertStudent 
                        @FirstName = {student.FirstName}, 
                        @LastName = {student.LastName}, 
                        @Branch = {student.Branch}, 
                        @Gender = {student.Gender}, 
                        @StudentId = {StudentIdOutParam} OUTPUT"
                );

                if (NumberOfRowsAffected > 0)
                {
                    // Retrieve the value of the OUT parameter
                    newStudentId = (int)StudentIdOutParam.Value;
                }
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Database AddStudent Error: {ex.InnerException?.Message ?? ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"AddStudent Error: {ex.Message}");
            }

            return newStudentId;
        }

        // Retrieves a single student by StudentId using the spGetStudentByStudentId stored procedure with FromSqlInterpolated.
        public static Student? GetStudentById(int studentId)
        {
            Student? student = null;

            try
            {
                using var context = new EFCoreDbContext();

                // Execute the stored procedure using FromSqlInterpolated for automatic parameterization
                student = context.Students
                                .FromSqlInterpolated($@"
                                    EXEC spGetStudentByStudentId 
                                        @StudentId = {studentId}")
                                .AsNoTracking() // No Tracking
                                .AsEnumerable() // Materialize the results by calling ToList, ToArray, or AsEnumerable to ensure the data is retrieved from the database
                                .FirstOrDefault();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Database GetStudentById Error: {ex.InnerException?.Message ?? ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetStudentById Error: {ex.Message}");
            }

            return student;
        }

        // Retrieves all students using the spGetAllStudents stored procedure with FromSqlInterpolated.
        public static List<Student> GetAllStudents()
        {
            var students = new List<Student>();

            try
            {
                using var context = new EFCoreDbContext();

                // Execute the stored procedure using FromSqlInterpolated
                students = context.Students
                                  .FromSqlInterpolated($@"
                                      EXEC spGetAllStudents")
                                  .AsNoTracking() // Optional: No tracking for read-only operations
                                  .ToList();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Database GetAllStudents Error: {ex.InnerException?.Message ?? ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetAllStudents Error: {ex.Message}");
            }

            return students;
        }

        // Updates an existing student using the spUpdateStudent stored procedure with ExecuteSqlInterpolated.
        public static void UpdateStudent(Student student)
        {
            try
            {
                using var context = new EFCoreDbContext();

                // Execute the stored procedure using ExecuteSqlInterpolated for automatic parameterization
                int rowsAffected = context.Database.ExecuteSqlInterpolated($@"
                    EXEC spUpdateStudent 
                        @StudentId = {student.StudentId}, 
                        @FirstName = {student.FirstName}, 
                        @LastName = {student.LastName}, 
                        @Branch = {student.Branch}, 
                        @Gender = {student.Gender}"
                );

                // Output the result
                if (rowsAffected > 0)
                {
                    Console.WriteLine($"\nStudent with ID {student.StudentId} successfully updated.");
                }
                else
                {
                    Console.WriteLine($"\nNo student found with ID {student.StudentId} to update.");
                }
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Database UpdateStudent Error: {ex.InnerException?.Message ?? ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"UpdateStudent Error: {ex.Message}");
            }
        }

        // Deletes a student by StudentId using the spDeleteStudent stored procedure with ExecuteSqlInterpolated.
        public static void DeleteStudent(int studentId)
        {
            try
            {
                using var context = new EFCoreDbContext();

                // Execute the stored procedure using ExecuteSqlInterpolated for automatic parameterization
                int rowsAffected = context.Database.ExecuteSqlInterpolated($@"
                    EXEC spDeleteStudent 
                        @StudentId = {studentId}"
                );

                // Output the result
                if (rowsAffected > 0)
                {
                    Console.WriteLine($"\nStudent with ID {studentId} successfully deleted.");
                }
                else
                {
                    Console.WriteLine($"\nNo student found with ID {studentId} to delete.");
                }
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Database DeleteStudent Error: {ex.InnerException?.Message ?? ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DeleteStudent Error: {ex.Message}");
            }
        }
    }
}