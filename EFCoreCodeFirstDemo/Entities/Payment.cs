using System.ComponentModel.DataAnnotations.Schema;
namespace EFCoreCodeFirstDemo.Entities
{
    public class Payment
    {
        public int PaymentId { get; set; } // Primary Key
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; } // Amount to be paid
        public string Currency { get; set; } // Currency type, e.g., USD
        public string Status { get; set; } // Payment status: Pending, Completed, Failed, Cancelled
        public string TransactionId { get; set; } // External Transaction ID
        public string? FailureReason { get; set; } // Reason for failure, if any

        public int OrderId { get; set; } // Foreign Key to Order
        public Order Order { get; set; } // Navigation property to Order
    }
}
