using System.ComponentModel.DataAnnotations.Schema;
namespace EFCoreCodeFirstDemo.Entities
{
    public class OrderItem
    {
        public int OrderItemId { get; set; } // Identity Primary Key
        public int OrderId { get; set; } // FK to Order.OrderId
        public Order Order { get; set; } // Navigation Property
        public Guid ProductId { get; set; } // FK to Product.ProductId
        public Product Product { get; set; } // Navigation Property
        public int Quantity { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal TotalPrice { get; set; } // Computed as Quantity * Price
    }
}