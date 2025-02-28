using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreCodeFirstDemo.Entities
{
    // Abstract Base Class representing a general Invoice
    public abstract class Invoice
    {
        public int InvoiceId { get; set; } // Primary Key
        public string InvoiceNumber { get; set; } // Unique Invoice Number
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; } // Invoice Amount
        public DateTime BillingDate { get; set; } // Date of Billing
        public string CustomerName { get; set; } // Customer Name
        public string CustomerEmail { get; set; } // Customer Email
        public string BillingAddress { get; set; } // Customer's Billing Address
        public InvoiceStatus Status { get; set; } // Invoice Status (Paid, Pending, Overdue)
    }

    // Enum representing the status of the invoice
    public enum InvoiceStatus
    {
        Pending,
        Paid,
        Overdue
    }
}