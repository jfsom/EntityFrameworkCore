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

                var postsWithBlogs = context.Posts
                            .Select(p => new
                            {
                                Post = p,
                                BlogId = EF.Property<int>(p, "BlogId") // Access the shadow property
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