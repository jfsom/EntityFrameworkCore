using EFCoreCodeFirstDemo.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace EFCoreCodeFirstDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialize the DbContext (using a 'using' statement ensures proper resource disposal)
            using (var context = new EFCoreDbContext())
            {
                // Display an informative message for user input
                Console.WriteLine("Please enter the student information:");

                // Capture the student's first name
                Console.Write("First Name (Max 50 characters): ");
                string? firstName = Console.ReadLine();

                // Capture the student's last name
                Console.Write("Last Name (Min 5 characters): ");
                string? lastName = Console.ReadLine();

                try
                {
                    // Validate the Student entity manually before saving to ensure it meets constraints
                    var student = new Student
                    {
                        FirstName = firstName,
                        LastName = lastName
                    };

                    // Perform validation using the DataAnnotationsValidator
                    ValidateStudent(student); // This method will throw an exception if validation fails

                    // If validation passes, add the student to the DbSet and save changes to the database
                    context.Students.Add(student);
                    context.SaveChanges();

                    // Inform the user that the data has been successfully saved
                    Console.WriteLine("Student information has been saved successfully!");
                }
                catch (ValidationException ex) // Catch validation errors (MaxLength, MinLength, etc.)
                {
                    // Display the validation error message to the user
                    Console.WriteLine($"Validation error: {ex.Message}");
                }
                catch (DbUpdateException dbEx) // Catch any database update errors
                {
                    // Handle potential database errors and provide a meaningful message
                    Console.WriteLine($"Database update error: {dbEx.InnerException?.Message ?? dbEx.Message}");
                }
                catch (Exception ex) // Catch any other general exceptions
                {
                    // Display a general error message
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }

                // Output the list of all students to show the saved records
                var students = context.Students.ToList();
                Console.WriteLine("\nList of Students in Database:");
                foreach (var stud in students)
                {
                    Console.WriteLine($"Student ID: {stud.StudentId}, First Name: {stud.FirstName}, Last Name: {stud.LastName}");
                }
            }
        }

        // Custom method to perform validation on the Student entity
        public static void ValidateStudent(Student student)
        {
            // Create a ValidationContext object.
            // This object contains information about the object being validated (student in this case).
            // It is used by the Validator to perform validation according to the attributes (like MaxLength, MinLength) applied on the Student entity.
            // The 'null' values are placeholders for services or items that could be used by the ValidationContext (we don’t need them here, hence null).
            var validationContext = new ValidationContext(student, null, null);

            // Create a list to hold the results of the validation.
            // Each item in this list will be a ValidationResult, 
            // which will contain information about any validation errors that occur.
            var validationResults = new List<ValidationResult>();

            // TryValidateObject is a method from the Validator class that checks if the given object (student)
            // satisfies the validation rules specified by the data annotations (MaxLength, MinLength, etc.).
            // - The 'true' flag at the end ensures that all properties of the object (student) are validated.
            // - If validation fails, the validation errors are added to the 'validationResults' list.
            if (!Validator.TryValidateObject(student, validationContext, validationResults, true))
            {
                // If validation results contain errors (i.e., TryValidateObject returns false),
                // throw a ValidationException to indicate that validation failed.
                // We extract the first validation error from the 'validationResults' list and use its error message.
                throw new ValidationException(validationResults.First().ErrorMessage);
            }

            // If no validation errors occur, the method simply completes, allowing the caller to proceed without error.
        }
    }
}