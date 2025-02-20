using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreCodeFirstDemo.Entities
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }

        // Foreign key properties
        public int OrderId { get; set; }
        public int CustomerId { get; set; }

        public string ProductName { get; set; }
        public int Quantity { get; set; }

        // Apply ForeignKey attribute on the navigation property, pointing to multiple foreign key properties
        [ForeignKey(nameof(OrderId) + "," + nameof(CustomerId))]
        public Order Order { get; set; }
    }
}