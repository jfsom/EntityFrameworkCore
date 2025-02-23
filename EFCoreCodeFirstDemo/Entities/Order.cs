using System.ComponentModel.DataAnnotations;
namespace EFCoreCodeFirstDemo.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; } // Primary Key
        public DateTime OrderDate { get; set; }
        public string OrderNumber { get; set; }
        public List<OrderItem> OrderItems { get; set; } // Navigation property
    }
}