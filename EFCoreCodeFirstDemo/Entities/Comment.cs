namespace EFCoreCodeFirstDemo.Entities
{
    // Entity representing a Comment on Content
    public class Comment
    {
        public int CommentId { get; set; } // Primary Key
        public string AuthorName { get; set; } // Name of the commenter
        public string Text { get; set; } // Content of the comment
        public DateTime CommentedDate { get; set; } // Date when the comment was made
        public int ContentId { get; set; } // Foreign Key to Content
        public virtual Content Content { get; set; } // Navigation property to Content
    }
}