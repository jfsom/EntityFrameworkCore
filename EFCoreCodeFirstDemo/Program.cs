using EFCoreCodeFirstDemo.Entities;
using Microsoft.EntityFrameworkCore;
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
                    //Root Entity with Empty key
                    FirstName = "Pranaya",
                    LastName = "Rout",
                    StandardId = 1,
                    //Make Sure the StandardId with Value 1 Exists in the Database, else you will get Exception
                    Standard = new Standard()   //Child Entity with key value
                    {
                        StandardId = 1,
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
                context.Attach(student).State = EntityState.Added;

                //Checking the Entity State of Each Entity of student Entity Graph
                foreach (var entity in context.ChangeTracker.Entries())
                {
                    Console.WriteLine($"Entity: {entity.Entity.GetType().Name}, State: {entity.State} ");
                }

                // Save changes to persist the changes to the database
                context.SaveChanges();
                Console.WriteLine("Entity Graph Saved Successfully");

                Console.Read();
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}"); ;
            }
        }
    }
}