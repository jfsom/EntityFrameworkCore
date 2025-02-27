namespace EFCoreCodeFirstDemo.Entities
{
    // Derived Class representing a Cash On Delivery payment
    public class CashOnDeliveryPayment : Payment
    {
        public DateTime? ExpectedDeliveryDate { get; set; } // The anticipated date when the product will be delivered to the customer.
        public bool? PaymentReceived { get; set; } // A flag indicating whether the payment has been collected upon delivery.
        public DateTime? PaymentReceivedDate { get; set; } // The date and time when the payment was actually received.
    }
}