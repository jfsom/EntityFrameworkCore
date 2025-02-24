using EFCoreCodeFirstDemo.Entities;
namespace EFCorePropertyConfigurations.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; } // Required
        public OrderStatus Status { get; set; } // Will configure enum mapping and also default value as Pending
        public DateTime CreatedDate { get; set; } // Will configure default value
        public byte[] RowVersion { get; set; } // Concurrency Token
        public int CustomerId { get; set; } // Foreign Key
        public Customer Customer { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}