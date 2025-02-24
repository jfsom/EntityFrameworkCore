namespace EFCoreCodeFirstDemo.Entities
{
    public class Order : ITimestampedEntity
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; } // We will store this as string                                       
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation Property
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}