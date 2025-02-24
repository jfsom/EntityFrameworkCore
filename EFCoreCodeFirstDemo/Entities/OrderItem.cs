namespace EFCoreCodeFirstDemo.Entities
{
    public class OrderItem : ITimestampedEntity
    {
        public int Id { get; set; }
        public int OrderId { get; set; } //FK
        public Order Order { get; set; }
        public int ProductId { get; set; } //FK
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}