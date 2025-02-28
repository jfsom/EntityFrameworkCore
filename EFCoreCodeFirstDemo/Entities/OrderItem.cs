using System.ComponentModel.DataAnnotations.Schema;
namespace EFCoreCodeFirstDemo.Entities
{
    public class OrderItem
    {
        public int OrderItemId { get; set; } // Primary Key
        public int ProductId { get; set; } // Foreign Key to Product
        public int Quantity { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public int OrderId { get; set; } // Foreign Key to Order
        // Navigation Properties
        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}