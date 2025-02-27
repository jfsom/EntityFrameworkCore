using EFCoreCodeFirstDemo.Entities;
namespace EFCoreCodeFirstDemo
{
    public class Program
    {
        static void Main(string[] args)
        {
            using (var context = new EFCoreDbContext())
            {
                // Fetch an existing article by its primary key (ContentId)
                var article = context.Articles.FirstOrDefault(a => a.ContentId == 1);

                if (article != null)
                {
                    // Update the article's properties
                    article.Title = "Updated: Understanding EF Core TPT Inheritance";
                    article.Summary = "Updated Summary for EF Core TPT Inheritance";
                    article.ReadingTime = 12;
                    article.LastEditedDate = DateTime.Now;

                    // Save the changes to the database
                    context.SaveChanges();

                    // Output result
                    Console.WriteLine($"Article (ID: {article.ContentId}) has been updated.");
                }
                else
                {
                    Console.WriteLine("Article not found.");
                }

                // Fetch an existing video by its primary key (ContentId)
                var video = context.Videos.FirstOrDefault(v => v.ContentId == 3);

                if (video != null)
                {
                    // Update the video's properties
                    video.Title = "Updated: Learn EF Core with Videos";
                    video.Duration = 4500; // Updated to 75 minutes
                    video.HasSubtitles = false; // Removing subtitles

                    // Save the changes to the database
                    context.SaveChanges();

                    // Output result
                    Console.WriteLine($"Video (ID: {video.ContentId}) has been updated.");
                }
                else
                {
                    Console.WriteLine("Video not found.");
                }

                // Fetch an existing image by its primary key (ContentId)
                var image = context.Images.FirstOrDefault(i => i.ContentId == 2);

                if (image != null)
                {
                    // Update the image's properties
                    image.AltText = "Updated EF Core Infographic Alt Text";
                    image.Photographer = "Updated: Alice Johnson";

                    // Save the changes to the database
                    context.SaveChanges();

                    // Output result
                    Console.WriteLine($"Image (ID: {image.ContentId}) has been updated.");
                }
                else
                {
                    Console.WriteLine("Image not found.");
                }
            }
        }
    }
}