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
                var blogPost = new BlogPost
                {
                    Title = "EF Core",
                    Content = "IT is an ORM Framework"
                };

                using var context = new EFCoreDbContext();

                context.BlogPosts.Add(blogPost);
                context.SaveChanges();

                // Assuming you have a DbContext instance named context
                PrintShadowProperties(context, blogPost);

                Console.Read();
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public static void PrintShadowProperties<TEntity>(DbContext context, TEntity entity) where TEntity : class
        {
            var entry = context.Entry(entity);
            var shadowProperties = entry.Metadata.GetProperties()
                                                .Where(p => p.IsShadowProperty())
                                                .Select(p => p.Name);

            Console.WriteLine($"Shadow Properties for {typeof(TEntity).Name}:");
            foreach (var propName in shadowProperties)
            {
                Console.WriteLine(propName);
            }
        }
    }
}