using System.ComponentModel.DataAnnotations.Schema;
namespace EFCoreCodeFirstDemo.Entities
{
    // Base Class representing a general payment
    public abstract class Payment
    {
        public int PaymentId { get; set; } // Primary Key, Unique identifier for each payment record in the database.
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }  // The total amount of the payment
        public DateTime PaymentDate { get; set; } // The date and time when the payment was made
        public string Currency { get; set; } // The currency in which the payment was made (e.g., INR, USD)
    }
}