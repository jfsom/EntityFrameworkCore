namespace EFCoreCodeFirstDemo.Entities
{
    public class Product : ITimestampedEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } // We will apply a default max length
        public decimal Price { get; set; } // We will set default precision                               
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}