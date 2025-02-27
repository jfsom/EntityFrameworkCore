using EFCoreCodeFirstDemo.Entities;
namespace EFCoreCodeFirstDemo
{
    public class Program
    {
        static void Main(string[] args)
        {
            using (var context = new EFCoreDbContext())
            {
                // The Contents DbSet will returns records from the base Contents table.
                // Since TPT creates separate tables for each derived class,
                // this query will hit multiple tables(Joining with Articles, Videos, Images)
                // But we can only access the properties which are available in base Content type
                var contents = context.Contents.ToList();

                //We loop through the contents and display the common properties
                //such as ContentId, Title, ContentType, Author, and PublishedDate.
                Console.WriteLine("----- List of All Content -----");
                foreach (var content in contents)
                {
                    Console.WriteLine($"Content ID: {content.ContentId}, Title: {content.Title}, Type: {content.ContentType}, Author: {content.Author}, Published: {content.PublishedDate.ToShortDateString()}");
                }

                //Separate queries for Articles, Videos, and Images are run, and
                //we display specific properties relevant to each derived type

                // Query and display details of all Articles
                var articles = context.Articles.ToList();
                Console.WriteLine("\n----- List of Articles -----");
                foreach (var article in articles)
                {
                    Console.WriteLine($"Article ID: {article.ContentId}, Title: {article.Title}, Summary: {article.Summary}, Reading Time: {article.ReadingTime} minutes");
                }

                // Query and display details of all Videos
                var videos = context.Videos.ToList();
                Console.WriteLine("\n----- List of Videos -----");
                foreach (var video in videos)
                {
                    Console.WriteLine($"Video ID: {video.ContentId}, Title: {video.Title}, URL: {video.VideoUrl}, Duration: {video.Duration / 60} minutes");
                }

                // Query and display details of all Images
                var images = context.Images.ToList();
                Console.WriteLine("\n----- List of Images -----");
                foreach (var image in images)
                {
                    Console.WriteLine($"Image ID: {image.ContentId}, Title: {image.Title}, URL: {image.ImageUrl}, Dimensions: {image.Dimensions}, Photographer: {image.Photographer}");
                }
            }
        }
    }
}