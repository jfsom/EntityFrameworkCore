using EFCoreCodeFirstDemo.Entities;

namespace EFCoreCodeFirstDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Create an instance of your EFCoreDbContext to interact with the database
                using (var context = new EFCoreDbContext())
                {
                    // Create two new Branch objects
                    var CSEBranch = new Branch
                    {
                        BranchName = "Computer Science",
                        Description = "Focuses on software development and computing technologies.",
                        PhoneNumber = "123-456-7890",
                        Email = "cs@dotnettutorials.net"
                    };

                    var ElectricalBranch = new Branch
                    {
                        BranchName = "Electrical Engineering",
                        Description = "Focuses on electrical systems and circuit design.",
                        PhoneNumber = "987-654-3210",
                        Email = "ee@dotnettutorials.net"
                    };

                    // Adding the Branch objects to the Branches DbSet
                    // This prepares the object to be inserted into the database
                    context.Branches.Add(CSEBranch);
                    context.Branches.Add(ElectricalBranch);

                    // Alternatively, you can use the DbContext.Add method to add entities
                    // context.Add(CSEBranch);
                    // context.Add(ElectricalBranch);

                    // Create two new Student objects
                    var student1 = new Student
                    {
                        FirstName = "Pranaya",
                        LastName = "Rout",
                        DateOfBirth = new DateTime(2000, 1, 15),
                        Gender = "Female",
                        Email = "Pranaya.Rout@dotnettutorials.net",
                        PhoneNumber = "555-1234",
                        EnrollmentDate = DateTime.Now,
                        Branch = CSEBranch // Assign the Computer Science branch
                    };

                    var student2 = new Student
                    {
                        FirstName = "Rakesh",
                        LastName = "Kumar",
                        DateOfBirth = new DateTime(1999, 10, 22),
                        Gender = "Male",
                        Email = "Rakesh.Kumar@dotnettutorials.net",
                        PhoneNumber = "555-5678",
                        EnrollmentDate = DateTime.Now,
                        Branch = ElectricalBranch // Assign the Electrical Engineering branch
                    };

                    // Adding the Student objects to the Students DbSet
                    // This prepares the object to be inserted into the database
                    context.Students.Add(student1);
                    context.Students.Add(student2);

                    // Alternatively, you can use the DbContext.Add method to add entities
                    // context.Add(student1);
                    // context.Add(student2);

                    // Save the changes to the database
                    // This actually performs the INSERT operation in the database
                    context.SaveChanges();

                    // Display a success message on the console
                    Console.WriteLine("Branch and Student records have been successfully inserted into the database.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}"); ;
            }
        }
    }
}