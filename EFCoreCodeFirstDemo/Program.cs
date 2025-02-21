using EFCoreCodeFirstDemo.Entities;
using Microsoft.EntityFrameworkCore;
namespace EFCoreCodeFirstDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var context = new EFCoreDbContext();

            // Fetch and display departments with their employees
            var departments = context.Departments.Include(d => d.Employees).ToList();

            Console.WriteLine("Departments and Employees:");
            foreach (var department in departments)
            {
                Console.WriteLine($"Department: {department.Name}");
                foreach (var employee in department.Employees)
                {
                    Console.WriteLine($"\tEmployee: {employee.FullName}");
                    Console.WriteLine($"\tTenure: {employee.Tenure} years and Date of Joining: {employee.DateOfJoining:yyyy-MM-dd}");
                    Console.WriteLine($"\tAge: {employee.Age} years and Date of Birth: {employee.DateOfBirth:yyyy-MM-dd}");
                    Console.WriteLine(); //Line Break
                }
            }
        }
    }
}