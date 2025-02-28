using System.ComponentModel.DataAnnotations.Schema;
namespace EFCoreCodeFirstDemo.Entities
{
    public class Order
    {
        public int OrderId { get; set; } // Primary Key
        public DateTime OrderDate { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }
        // Navigation Property - One Order has many OrderItems
        public ICollection<OrderItem> OrderItems { get; set; }
        // Navigation Property - One Order has one Payment
        public Payment Payment { get; set; }
    }
}
