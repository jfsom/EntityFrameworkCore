using EFCoreCodeFirstDemo.Entities;
namespace EFCoreCodeFirstDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // Perform Delete operations
            DeletePayment();
        }

        // Deletes a specific payment record from the database.
        static void DeletePayment()
        {
            using (var context = new EFCoreDbContext())
            {
                // Prompt user to enter the Payment ID to delete
                Console.Write("Enter the Payment ID to delete: ");
                if (int.TryParse(Console.ReadLine(), out int paymentId))
                {
                    // Retrieve the payment with the specified Payment ID
                    var paymentToDelete = context.Payments.Find(paymentId);

                    if (paymentToDelete != null)
                    {
                        Console.WriteLine($"Deleting Payment ID: {paymentToDelete.PaymentId}, Type: {paymentToDelete.GetType().Name}");

                        // Remove the payment from the context
                        context.Payments.Remove(paymentToDelete);

                        // Save changes to the database
                        context.SaveChanges();

                        Console.WriteLine("Payment has been deleted successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Payment not found for deletion.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Payment ID.");
                }
            }
        }
    }
}