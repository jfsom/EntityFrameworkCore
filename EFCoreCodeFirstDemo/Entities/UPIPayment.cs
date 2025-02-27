namespace EFCoreCodeFirstDemo.Entities
{
    // Derived Class representing a UPI payment
    public class UPIPayment : Payment
    {
        public string? UPIId { get; set; } // The UPI ID (e.g., user@bank) associated with the payer's bank account.
        public string? UPITransactionId { get; set; } // A unique identifier assigned to the UPI transaction for tracking purposes.
    }
}