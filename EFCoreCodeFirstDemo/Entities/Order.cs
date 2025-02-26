namespace EFCoreCodeFirstDemo.Entities
{
    public class Order
    {
        public int OrderId { get; set; } // Primary Key
        public DateTime OrderDate { get; set; } // Date of order placement
        public string Status { get; set; } // Status of the order (Pending, Processing, Completed, Cancelled)

        public int CustomerId { get; set; } // Foreign Key to Customer
        public Customer Customer { get; set; } // Navigation property to Customer

        // One-to-one relationship: Each order has a single payment.
        public Payment Payment { get; set; }
    }
}