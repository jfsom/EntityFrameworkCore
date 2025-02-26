using EFCoreCodeFirstDemo.Entities;

namespace EFCoreCodeFirstDemo
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                //Creating the Disconnected Entity Graph
                //Student Entity Graph (Student plus Standard, and StudentAddress)
                //Student is the Main Entity
                //Standard, and StudentAddress are the Child Entities
                var student = new Student()
                {
                    //Root Entity with key
                    //StudentId = 1, //It is Identity, so you cannot set Explicit Value
                    FirstName = "Pranaya",
                    LastName = "Rout",
                    StandardId = 1,
                    Standard = new Standard()   //Child Entity with key value
                    {
                        // StandardId = 1, //It is Identity, so you cannot set Explicit Value
                        StandardName = "STD1",
                        Description = "STD1 Description"
                    },
                    StudentAddress = new StudentAddress() //Child Entity with Empty Key
                    {
                        Address1 = "Address Line1",
                        Address2 = "Address Line2"
                    }
                };

                //Creating an Instance of the Context class
                using var context = new EFCoreDbContext();

                //Attaching the Disconnected Student Entity Graph to the Context Object 
                context.Students.Add(student);

                //Checking the Entity State of Each Entity of student Entity Graph
                foreach (var entity in context.ChangeTracker.Entries())
                {
                    Console.WriteLine($"Entity: {entity.Entity.GetType().Name}, State: {entity.State} ");
                }

                // Save changes to persist the changes to the database
                context.SaveChanges();
                Console.WriteLine("Entity Graph Added");
                Console.Read();
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}"); ;
            }
        }
    }
}