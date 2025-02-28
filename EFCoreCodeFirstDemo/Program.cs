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

                var blog = new Blog { Url = "http://dotnettutorials.net" };
                context.Blogs.Add(blog);
                context.SaveChanges();

                var post = new Post { Title = "Hello World", Content = "Welcome to my Blog!" };
                context.Posts.Add(post);

                // Set the shadow foreign key value
                context.Entry(post).Property("BlogId").CurrentValue = blog.BlogId;
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