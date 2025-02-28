using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreCodeFirstDemo.Entities
{
    public class Payment
    {
        public int PaymentId { get; set; } // Primary Key
        public int OrderId { get; set; } // Foreign Key
        public DateTime PaymentDate { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        public PaymentStatus Status { get; set; } // Pending, Failed, Completed

        // Navigation Property - One Payment belongs to one Order
        public Order Order { get; set; }
    }

    public enum PaymentStatus
    {
        Pending,
        Failed,
        Completed
    }
}
