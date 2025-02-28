namespace EFCoreCodeFirstDemo.Entities
{
    public class BlogPost
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        // No CreatedAt or LastUpdatedAt properties here
    }
}
