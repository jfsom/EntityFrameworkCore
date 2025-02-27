namespace EFCoreCodeFirstDemo.Entities
{
    // Derived Class representing a Video
    public class Video : Content
    {
        public string VideoUrl { get; set; } // URL of the video
        public int Duration { get; set; } // Duration of the video in seconds
        public string ThumbnailUrl { get; set; } // URL of the thumbnail image
        public string Resolution { get; set; } // Video resolution (e.g., 1080p)
        public bool HasSubtitles { get; set; } // Indicates if subtitles are available
        public string? Subtitles { get; set; } // URL of the subtitles file                                      
        public string? MetaKeywords { get; set; } // SEO keywords
        public string? MetaDescription { get; set; } // SEO meta description
    }
}