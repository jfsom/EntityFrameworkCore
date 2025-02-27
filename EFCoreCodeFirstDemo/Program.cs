using EFCoreCodeFirstDemo.Entities;

namespace EFCoreCodeFirstDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // Perform INSERT and READ operations
            CreatePayments();
            ReadPayments();
        }

        // Creates sample payment records and saves them to the database.
        static void CreatePayments()
        {
            using (var context = new EFCoreDbContext())
            {
                // Create a new CardPayment (Credit Card)
                var cardPaymentCredit = new CardPayment
                {
                    Amount = 1500.00m,
                    PaymentDate = DateTime.Now,
                    Currency = "INR",
                    CardNumber = "4111111111111111",
                    CardHolderName = "Ravi Kumar",
                    ExpiryDate = new DateTime(2024, 12, 31),
                    CVV = "123",
                    CardType = CardType.Credit
                };

                // Create a new CardPayment (Debit Card)
                var cardPaymentDebit = new CardPayment
                {
                    Amount = 2000.00m,
                    PaymentDate = DateTime.Now,
                    Currency = "INR",
                    CardNumber = "5111111111111111",
                    CardHolderName = "Anjali Mehta",
                    ExpiryDate = new DateTime(2025, 11, 30),
                    CVV = "456",
                    CardType = CardType.Debit
                };

                // Create a new UPIPayment
                var upiPayment = new UPIPayment
                {
                    Amount = 750.00m,
                    PaymentDate = DateTime.Now,
                    Currency = "INR",
                    UPIId = "ravi@upi",
                    UPITransactionId = "TXN1234567890"
                };

                // Create a new CashOnDeliveryPayment
                var codPayment = new CashOnDeliveryPayment
                {
                    Amount = 500.00m,
                    PaymentDate = DateTime.Now,
                    Currency = "INR",
                    ExpectedDeliveryDate = DateTime.Now.AddDays(3),
                    PaymentReceived = false
                };

                // Add payments to the context
                context.Payments.AddRange(cardPaymentCredit, cardPaymentDebit, upiPayment, codPayment);

                // Save changes to the database
                context.SaveChanges();

                Console.WriteLine("Payments have been created and saved to the database.\n");
            }
        }

        //Reads and displays all payment records from the database.
        static void ReadPayments()
        {
            using (var context = new EFCoreDbContext())
            {
                // Retrieve all payments from the database
                var payments = context.Payments.ToList();

                Console.WriteLine("Displaying all payments:");

                foreach (var payment in payments)
                {
                    Console.WriteLine($"Payment ID: {payment.PaymentId}, Amount: {payment.Amount}, Payment Date: {payment.PaymentDate}, Currency: {payment.Currency}, Payment Type: {payment.GetType().Name}");

                    // Use pattern matching to access derived class properties
                    if (payment is CardPayment cardPayment)
                    {
                        Console.WriteLine($"\tCard Type: {cardPayment.CardType}, Card Number: {cardPayment.CardNumber}");
                        Console.WriteLine($"\tCard Holder Name: {cardPayment.CardHolderName}, Expiry Date: {cardPayment.ExpiryDate?.ToShortDateString()}");
                    }
                    else if (payment is UPIPayment upi)
                    {
                        Console.WriteLine($"\tUPI ID: {upi.UPIId}, UPI Transaction ID: {upi.UPITransactionId}");
                    }
                    else if (payment is CashOnDeliveryPayment cod)
                    {
                        Console.WriteLine($"\tExpected Delivery Date: {cod.ExpectedDeliveryDate?.ToShortDateString()}");
                        Console.WriteLine($"\tPayment Received: {cod.PaymentReceived}, Payment Received Date: {cod.PaymentReceivedDate?.ToShortDateString()}");
                    }

                    Console.WriteLine();
                }
            }
        }
    }
}