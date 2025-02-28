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
                using var context = new EFCoreDbContext();
                var usersWithStatus = context.Users
                             .Select(u => new
                             {
                                 User = u,
                                 IsActive = EF.Property<bool>(u, "IsActive") // Access the shadow property
                             })
                             .ToList();

                Console.Read();
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}