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
                    // Joining Students and Branches using Query Syntax (LINQ query)
                    var studentsWithBranchesQuerySyntax = (from student in context.Students // Loop over the Students table
                                                           join branch in context.Branches // Perform an inner join with the Branches table
                                                           on student.Branch.BranchId equals branch.BranchId // Define the join condition based on BranchId
                                                           select new // Create an anonymous object containing selected fields from both tables
                                                           {
                                                               student.FirstName, // Select the student's first name
                                                               student.LastName,  // Select the student's last name
                                                               student.Email,     // Select the student's email
                                                               student.EnrollmentDate, // Select the student's enrollment date
                                                               branch.BranchName  // Select the corresponding branch name
                                                           }).ToList(); // Execute the query and convert the result to a list

                    // Joining Students and Branches using Method Syntax (LINQ method chaining)
                    //var studentsWithBranchesMethodSyntax = context.Students // Start with the Students table
                    //                                              .Join(context.Branches, // Join with the Branches table
                    //                                                    student => student.Branch.BranchId, // Define the key from the Students table for the join (BranchId)
                    //                                                    branch => branch.BranchId, // Define the key from the Branches table for the join (BranchId)
                    //                                                    (student, branch) => new // Create an anonymous object for each joined record
                    //                                                    {
                    //                                                        student.FirstName, // Select the student's first name
                    //                                                        student.LastName,  // Select the student's last name
                    //                                                        student.Email,     // Select the student's email
                    //                                                        student.EnrollmentDate, // Select the student's enrollment date
                    //                                                        branch.BranchName  // Select the corresponding branch name
                    //                                                    })
                    //                                              .ToList(); // Execute the query and convert the result to a list

                    // Check if any results are found
                    if (studentsWithBranchesQuerySyntax.Any())
                    {
                        // Iterate through the results and display the details
                        foreach (var item in studentsWithBranchesQuerySyntax)
                        {
                            // Output the student's details along with the branch name
                            Console.WriteLine($"Student: {item.FirstName} {item.LastName}, Email: {item.Email}, Enrollment Date: {item.EnrollmentDate.ToShortDateString()}, Branch: {item.BranchName}");
                        }
                    }
                    else
                    {
                        // Output if no students are found
                        Console.WriteLine("No students found.");
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