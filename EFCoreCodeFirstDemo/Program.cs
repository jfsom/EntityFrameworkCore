using EFCoreCodeFirstDemo.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCoreCodeFirstDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // Perform Update operation
            UpdatePayment();
        }

        // Updates a specific payment record in the database.
        static void UpdatePayment()
        {
            using (var context = new EFCoreDbContext())
            {
                // Prompt user to enter the Payment ID to update
                Console.Write("Enter the Payment ID to update: ");
                if (int.TryParse(Console.ReadLine(), out int paymentId))
                {
                    // Retrieve the payment with the specified Payment ID
                    var paymentToUpdate = context.Payments.Find(paymentId);
                    // Since all payments are in one table, the Find method looks up the record by PaymentId.
                    //EF Core reads the PaymentType discriminator column to determine the actual type of the payment(CardPayment, UPIPayment, or CashOnDeliveryPayment
                    //F Core instantiates the correct derived class based on the discriminator value.

                    if (paymentToUpdate != null)
                    {
                        Console.WriteLine($"Updating Payment ID: {paymentToUpdate.PaymentId}, Type: {paymentToUpdate.GetType().Name}");

                        // Update common properties
                        Console.Write("Enter new amount (leave blank to keep current): ");
                        var amountInput = Console.ReadLine();
                        if (decimal.TryParse(amountInput, out decimal newAmount))
                        {
                            paymentToUpdate.Amount = newAmount;
                        }

                        // Update properties based on payment type
                        // Uses is pattern matching to check if paymentToUpdate is a CardPayment.
                        if (paymentToUpdate is CardPayment cardPayment)
                        {
                            Console.Write("Enter new Card Holder Name (leave blank to keep current): ");
                            var newName = Console.ReadLine();
                            if (!string.IsNullOrEmpty(newName))
                            {
                                cardPayment.CardHolderName = newName;
                            }
                            //If you want you can also update other properties as required
                        }
                        else if (paymentToUpdate is UPIPayment upiPayment)
                        {
                            Console.Write("Enter new UPI ID (leave blank to keep current): ");
                            var newUPIId = Console.ReadLine();
                            if (!string.IsNullOrEmpty(newUPIId))
                            {
                                upiPayment.UPIId = newUPIId;
                            }
                        }
                        else if (paymentToUpdate is CashOnDeliveryPayment codPayment)
                        {
                            Console.Write("Has payment been received? (y/n): ");
                            var paymentReceivedInput = Console.ReadLine();
                            if (paymentReceivedInput?.ToLower() == "y")
                            {
                                codPayment.PaymentReceived = true;
                                codPayment.PaymentReceivedDate = DateTime.Now;
                            }
                        }

                        // Save changes to the database
                        context.SaveChanges();

                        Console.WriteLine("Payment has been updated successfully.\n");
                    }
                    else
                    {
                        Console.WriteLine("Payment not found for update.\n");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Payment ID.\n");
                }
            }
        }
    }
}