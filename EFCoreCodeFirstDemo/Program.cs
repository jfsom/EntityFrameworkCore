using EFCoreCodeFirstDemo.Entities;
namespace EFCoreCodeFirstDemo
{
    public class Program
    {
        static void Main(string[] args)
        {
            using (var context = new EFCoreDbContext())
            {
                // Create and seed Article content
                var article = new Article
                {
                    Title = "Understanding EF Core TPT Inheritance",
                    Author = "Pranaya Rout",
                    PublishedDate = DateTime.Now,
                    ContentType = ContentType.Article,
                    Status = ContentStatus.Published,
                    Content = "This is a comprehensive guide on implementing TPT Inheritance...",
                    Summary = "EF Core TPT Inheritance",
                    ReadingTime = 10,
                    FeaturedImage = "https://example.com/image.jpg",
                    LastEditedDate = DateTime.Now,
                    MetaTitle = "EF Core TPT",
                    MetaKeywords = "EF Core, Inheritance, TPT",
                    MetaDescription = "Learn about TPT inheritance in EF Core with examples."
                };

                // Create and seed Video content
                var video = new Video
                {
                    Title = "Learn EF Core with Videos",
                    Author = "Rakesh Kumar",
                    PublishedDate = DateTime.Now,
                    ContentType = ContentType.Video,
                    Status = ContentStatus.Published,
                    VideoUrl = "http://example.com/learn-efcore.mp4",
                    ThumbnailUrl = "https://example.com/thumbnail.jpg",
                    Duration = 3600,
                    Resolution = "1080p",
                    HasSubtitles = true,
                    Subtitles = "http://example.com/subtitles.srt",
                    MetaKeywords = "EF Core, Video, Learning",
                    MetaDescription = "Learn EF Core through comprehensive video tutorials."
                };

                // Create and seed Image content
                var image = new Image
                {
                    Title = "EF Core Infographic",
                    Author = "Hina Sharma",
                    PublishedDate = DateTime.Now,
                    ContentType = ContentType.Image,
                    Status = ContentStatus.Published,
                    Caption = "EF Core Architecture Diagram",
                    ImageUrl = "http://example.com/efcore-infographic.jpg",
                    AltText = "EF Core Infographic",
                    Dimensions = "1920x1080",
                    Photographer = "Hina Sharma"
                };

                // Add the new content to the context
                context.Articles.Add(article);
                context.Videos.Add(video);
                context.Images.Add(image);

                // Save the changes to the database
                int recordsAdded = context.SaveChanges();

                // Output the result
                Console.WriteLine($"{recordsAdded} records were saved to the database.");

                // Confirm insertion
                Console.WriteLine("Content items have been successfully added.");
            }
        }
    }
}