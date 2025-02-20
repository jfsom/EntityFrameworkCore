using Microsoft.EntityFrameworkCore;

namespace EFCoreCodeFirstDemo.Entities
{
    [PrimaryKey("OrderId", "CustomerId")] //Composite Primary Key
    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }

        // Navigation property for the dependent entity
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}