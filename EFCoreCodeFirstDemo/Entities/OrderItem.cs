namespace EFCorePropertyConfigurations.Entities
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public decimal UnitPrice { get; set; } // Will configure precision and scale
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; } // Will configure computed column
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}