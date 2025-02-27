using EFCoreCodeFirstDemo.Entities;
namespace EFCoreCodeFirstDemo
{
    public class Program
    {
        static void Main(string[] args)
        {
            using (var context = new EFCoreDbContext())
            {
                // Fetch an article to delete by its primary key (ContentId)
                var article = context.Articles.FirstOrDefault(a => a.ContentId == 1);

                if (article != null)
                {
                    // Remove the article
                    context.Articles.Remove(article);

                    // Save the changes to the database
                    context.SaveChanges();

                    // Output result
                    Console.WriteLine($"Article (ID: {article.ContentId}) has been deleted.");
                }
                else
                {
                    Console.WriteLine("Article not found.");
                }

                // Fetch a video to delete by its primary key (ContentId)
                var video = context.Videos.FirstOrDefault(v => v.ContentId == 3);

                if (video != null)
                {
                    // Remove the video
                    context.Videos.Remove(video);

                    // Save the changes to the database
                    context.SaveChanges();

                    // Output result
                    Console.WriteLine($"Video (ID: {video.ContentId}) has been deleted.");
                }
                else
                {
                    Console.WriteLine("Video not found.");
                }

                // Fetch an image to delete by its primary key (ContentId)
                var image = context.Images.FirstOrDefault(i => i.ContentId == 2);

                if (image != null)
                {
                    // Remove the image
                    context.Images.Remove(image);

                    // Save the changes to the database
                    context.SaveChanges();

                    // Output result
                    Console.WriteLine($"Image (ID: {image.ContentId}) has been deleted.");
                }
                else
                {
                    Console.WriteLine("Image not found.");
                }
            }
        }
    }
}