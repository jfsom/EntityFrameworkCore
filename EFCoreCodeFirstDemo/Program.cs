using EFCoreCodeFirstDemo.Entities;
namespace EFCoreCodeFirstDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //// Initialize the DbContext, which represents a session with the database
                //using (var context = new EFCoreDbContext())
                //{
                //    Console.WriteLine("==============Branch Wise Report==============");

                //    // LINQ Query Syntax:
                //    // This query joins the Branches and Students tables, groups the students by branch,
                //    // and then prepares to calculate additional information like the number of students 
                //    // and the average enrollment date for each branch.
                //    var branchDetailsQuerySyntax = (from branch in context.Branches
                //                                        // Join the Branches and Students tables on BranchId
                //                                    join student in context.Students on branch.BranchId equals student.Branch.BranchId
                //                                    // Group the students by BranchId and BranchName
                //                                    group student by new { branch.BranchId, branch.BranchName } into branchGroup
                //                                    // Select the grouped data to prepare for client-side processing
                //                                    select new
                //                                    {
                //                                        BranchName = branchGroup.Key.BranchName, // The name of the branch
                //                                        Students = branchGroup.ToList() // Fetch all students in this branch
                //                                    })
                //                                    .AsEnumerable() // Switch to client-side evaluation for further processing
                //                                    .Select(branch => new
                //                                    {
                //                                        BranchName = branch.BranchName, // The name of the branch
                //                                        StudentCount = branch.Students.Count(), // Count the number of students in the branch
                //                                        // Calculate the average enrollment date (as ticks) of the students in the branch
                //                                        AverageEnrollmentDate = branch.Students.Average(s => s.EnrollmentDate.Ticks),
                //                                        // Sort students by LastName ascending, then by FirstName ascending
                //                                        Students = branch.Students.OrderBy(s => s.LastName).ThenBy(s => s.FirstName).ToList()
                //                                    });

                //    // LINQ Method Syntax:
                //    // This query achieves the same goal as the above LINQ Query Syntax but using method syntax.
                //    var branchDetailsMethodSyntax = context.Branches
                //                                           // Join the Branches and Students tables on BranchId
                //                                           .Join(context.Students,
                //                                                 branch => branch.BranchId,
                //                                                 student => student.Branch.BranchId,
                //                                                 (branch, student) => new { branch, student })
                //                                           // Group the joined data by BranchId and BranchName
                //                                           .GroupBy(bs => new { bs.branch.BranchId, bs.branch.BranchName })
                //                                           .AsEnumerable() // Switch to client-side evaluation for further processing
                //                                           .Select(g => new
                //                                           {
                //                                               BranchName = g.Key.BranchName, // The name of the branch
                //                                               StudentCount = g.Count(), // Count the number of students in the branch
                //                                               // Calculate the average enrollment date (as ticks) of the students in the branch
                //                                               AverageEnrollmentDate = g.Average(bs => bs.student.EnrollmentDate.Ticks),
                //                                               // Sort students by LastName ascending, then by FirstName ascending
                //                                               Students = g.Select(bs => bs.student).OrderBy(s => s.LastName).ThenBy(s => s.FirstName).ToList()
                //                                           })
                //                                           .ToList(); // Convert the result to a list for further processing

                //    // Display the results:
                //    // Check if there are any branches with students to display
                //    if (branchDetailsQuerySyntax.Any())
                //    {
                //        // Iterate over each branch in the query result
                //        foreach (var branch in branchDetailsQuerySyntax)
                //        {
                //            // Display the branch name
                //            Console.WriteLine($"\nBranch: {branch.BranchName}");
                //            // Display the number of students in the branch
                //            Console.WriteLine($"Number of Students: {branch.StudentCount}");
                //            // Convert the average enrollment date (in ticks) to a DateTime and display it
                //            Console.WriteLine($"Average Enrollment Date: {new DateTime(Convert.ToInt64(branch.AverageEnrollmentDate)).ToShortDateString()}");

                //            // Display details of each student in the branch
                //            foreach (var student in branch.Students)
                //            {
                //                Console.WriteLine($"    Student: {student.LastName}, {student.FirstName} - Enrollment Date: {student.EnrollmentDate.ToShortDateString()}, Email: {student.Email}");
                //            }
                //        }
                //    }
                //    else
                //    {
                //        // If no branch details are found, display a message indicating so
                //        Console.WriteLine("No branch details found.");
                //    }
                //}
            }
            catch (Exception ex)
            {
                // Exception handling: log the exception message if something goes wrong during the process
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}