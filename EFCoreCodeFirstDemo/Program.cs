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

                var blogPostsWithAudit = context.BlogPosts
                                .Select(bp => new
                                {
                                    Title = bp.Title,
                                    Content = bp.Content,
                                    CreatedAt = EF.Property<DateTime>(bp, "CreatedAt"),
                                    LastUpdatedAt = EF.Property<DateTime>(bp, "LastUpdatedAt")
                                })
                                .ToList();

                foreach (var blogPost in blogPostsWithAudit)
                {
                    Console.WriteLine($"Title: {blogPost.Title}, CreatedAt: {blogPost.CreatedAt}, LastUpdatedAt:{blogPost.LastUpdatedAt}");
                    Console.WriteLine($"\tContent: {blogPost.Content}");
                }

                Console.Read();
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}