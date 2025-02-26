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

                // Add new students to the database
                int id1 = AddStudent(student1);
                Console.WriteLine($"Newly Added Student Id: {id1}");

                int id2 = AddStudent(student2);
                Console.WriteLine($"Newly Added Student Id: {id2}");

                // Retrieve a single student by ID
                var retrievedStudent = GetStudentById(id1);
                if (retrievedStudent != null)
                {
                    Console.WriteLine("\nRetrieved Student by Id {id1}:");
                    Console.WriteLine($"Id: {retrievedStudent.StudentId}, Name: {retrievedStudent.FirstName} {retrievedStudent.LastName}, Branch: {retrievedStudent.Branch}, Gender: {retrievedStudent.Gender}");
                }

                // Retrieve all students
                var allStudents = GetAllStudents();
                Console.WriteLine("\nAll Students:");
                foreach (var student in allStudents)
                {
                    Console.WriteLine($"Id: {student.StudentId}, Name: {student.FirstName} {student.LastName}, Branch: {student.Branch}, Gender: {student.Gender}");
                }

                // Update an existing student
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

                // Delete a student
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

        // Adds a new student to the database using the spInsertStudent stored procedure.
        public static int AddStudent(Student student)
        {
            int newStudentId = 0;

            try
            {
                using var context = new EFCoreDbContext();

                // Define parameters for the stored procedure
                //Input Parameter
                var firstNameParam = new SqlParameter("@FirstName", student.FirstName);
                var lastNameParam = new SqlParameter("@LastName", student.LastName);
                var branchParam = new SqlParameter("@Branch", student.Branch);
                var genderParam = new SqlParameter("@Gender", student.Gender);

                //Output Parameter
                var studentIdOutParam = new SqlParameter
                {
                    ParameterName = "@StudentId",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };

                // Execute the stored procedure
                context.Database.ExecuteSqlRaw(
                    "EXEC spInsertStudent @FirstName, @LastName, @Branch, @Gender, @StudentId OUTPUT",
                    firstNameParam, lastNameParam, branchParam, genderParam, studentIdOutParam
                );

                // Retrieve the output parameter value
                newStudentId = (int)studentIdOutParam.Value;
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

        // Retrieves a single student by StudentId using the spGetStudentByStudentId stored procedure.
        public static Student? GetStudentById(int studentId)
        {
            Student? student = null;

            try
            {
                using var context = new EFCoreDbContext();

                // Define parameter for the stored procedure
                var studentIdParam = new SqlParameter("@StudentId", studentId);

                // Execute the stored procedure and map the result to the Student entity
                // Execute the non-composable part of the query first using FromSqlRaw and
                // materialize the results by calling ToList, ToArray, or AsEnumerable to ensure the data is retrieved from the database
                student = context.Students
                                .FromSqlRaw("EXEC spGetStudentByStudentId @StudentId", studentIdParam)
                                .AsEnumerable()
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

        // Retrieves all students using the spGetAllStudents stored procedure.
        public static List<Student> GetAllStudents()
        {
            var students = new List<Student>();

            try
            {
                using var context = new EFCoreDbContext();

                // Execute the stored procedure and map results to the Student entity
                students = context.Students
                                  .FromSqlRaw("EXEC spGetAllStudents")
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

        //Updates an existing student using the spUpdateStudent stored procedure.
        public static void UpdateStudent(Student student)
        {
            try
            {
                using var context = new EFCoreDbContext();

                // Define parameters for the stored procedure
                var studentIdParam = new SqlParameter("@StudentId", student.StudentId);
                var firstNameParam = new SqlParameter("@FirstName", student.FirstName);
                var lastNameParam = new SqlParameter("@LastName", student.LastName);
                var branchParam = new SqlParameter("@Branch", student.Branch);
                var genderParam = new SqlParameter("@Gender", student.Gender);

                // Execute the stored procedure
                int rowsAffected = context.Database.ExecuteSqlRaw(
                    "EXEC spUpdateStudent @StudentId, @FirstName, @LastName, @Branch, @Gender",
                    studentIdParam, firstNameParam, lastNameParam, branchParam, genderParam
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

        // Deletes a student by StudentId using the spDeleteStudent stored procedure.
        public static void DeleteStudent(int studentId)
        {
            try
            {
                using var context = new EFCoreDbContext();

                // Define parameter for the stored procedure
                var studentIdParam = new SqlParameter("@StudentId", studentId);

                // Execute the stored procedure
                int rowsAffected = context.Database.ExecuteSqlRaw("EXEC spDeleteStudent @StudentId", studentIdParam);

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