using EFCoreCodeFirstDemo.Entities;
namespace EFCoreCodeFirstDemo
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                using var context = new EFCoreDbContext();
                var user = new User { Name = "Pranaya Rout" };
                context.Users.Add(user);
                context.SaveChanges();

                Console.Read();
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}