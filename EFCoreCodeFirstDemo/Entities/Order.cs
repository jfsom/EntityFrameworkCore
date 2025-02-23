using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreCodeFirstDemo.Entities
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; } // Identity Primary Key
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; } // Default current date
        public string Status { get; set; } // Pending, Failed, Successful, Default is Pending
        public int CustomerId { get; set; } // FK to Customer.SerialNumber
        public Customer Customer { get; set; } // Navigation Property
        public List<OrderItem> OrderItems { get; set; }
    }
}