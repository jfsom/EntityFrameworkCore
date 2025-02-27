namespace EFCoreCodeFirstDemo.Entities
{
    // Base Class representing general content
    public abstract class Content
    {
        public int ContentId { get; set; } // Primary Key
        public string Title { get; set; } // Title of the content
        public string Author { get; set; } // Author of the content
        public DateTime PublishedDate { get; set; } // Date when the content was published
        public ContentType ContentType { get; set; } // Type of the content (e.g., Article, Video, Image)
        public ContentStatus Status { get; set; } // Status of the content (Draft, Published, Archived)

        // Navigation Properties
        public virtual ICollection<Comment> Comments { get; set; }// Comments related to the content
    }

    // Enum for Content Status
    public enum ContentStatus
    {
        Draft,
        Published,
        Archived
    }

    // Enum for Content Type
    public enum ContentType
    {
        Article,
        Video,
        Image
    }
}