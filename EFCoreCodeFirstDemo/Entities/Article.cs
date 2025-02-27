namespace EFCoreCodeFirstDemo.Entities
{
    // Derived Class representing an Article
    public class Article : Content
    {
        public string Content { get; set; } // Full content of the article
        public string Summary { get; set; } // Brief summary of the article
        public int? ReadingTime { get; set; } // Estimated reading time in minutes
        public string? FeaturedImage { get; set; } // URL of the featured image
        public DateTime? LastEditedDate { get; set; } // Date when the article was last edited
        public string? MetaKeywords { get; set; } // SEO keywords
        public string? MetaDescription { get; set; } // SEO meta description
        public string? MetaTitle { get; set; } // SEO Title
    }
}