using EFCoreCodeFirstDemo.Entities;
namespace EFCoreCodeFirstDemo
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                var blogPost = new BlogPost
                {
                    Title = "EF Core",
                    Content = "IT is an ORM Framework"
                };

                using var context = new EFCoreDbContext();

                context.BlogPosts.Add(blogPost);
                context.SaveChanges();
                Console.WriteLine("New BlogPost Added..");
                // Entity Framework Core will set the "CreatedAt" and "LastUpdatedAt" Shadow Properties Value

                blogPost.Content = "Entity Framework Core is Updated";
                context.SaveChanges();
                // Entity Framework Core will update the "LastUpdatedAt" shadow property value.

                Console.WriteLine("BlogPost Updated..");
                Console.Read();
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}