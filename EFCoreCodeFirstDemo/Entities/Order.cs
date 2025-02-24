namespace EFCoreCodeFirstDemo.Entities
{
    public class Order
    {
        public int OrderId { get; set; } // Primary Key
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; } // Enum stored as string
        public int CustomerId { get; set; } // Foreign Key
        public Customer Customer { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}