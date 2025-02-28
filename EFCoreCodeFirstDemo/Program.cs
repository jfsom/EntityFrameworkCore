using EFCoreCodeFirstDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                using var context = new EcommerceDbContext();

                // *** Delete an Order and Associated Order Items and Payments ***

                // Specify the Order ID that you want to delete
                int orderIdToDelete = 1; // Change this to the actual Order ID you want to delete

                Console.WriteLine($"Attempting to delete Order ID: {orderIdToDelete}\n");

                // Retrieve the order including related entities:
                // - Include Order Items
                // - Include Payments
                var order = context.Orders
                    .Include(o => o.OrderItems)
                    .Include(o => o.Payments)
                    .FirstOrDefault(o => o.OrderId == orderIdToDelete);

                // Check if the order exists
                if (order != null)
                {
                    // Display order details
                    Console.WriteLine("Order Details:");
                    Console.WriteLine($"Order ID: {order.OrderId}, Date: {order.OrderDate}, Status: {order.Status},Total Amount: {order.TotalAmount}\n");

                    // Display associated order items
                    Console.WriteLine("Associated Order Items:");
                    foreach (var orderItem in order.OrderItems)
                    {
                        Console.WriteLine($"\tOrder Item ID: {orderItem.OrderItemId}, Product ID: {orderItem.ProductId}, Quantity: {orderItem.Quantity}, Total Price: {orderItem.TotalPrice}");
                    }

                    // Display associated payments
                    Console.WriteLine("\nAssociated Payments:");
                    foreach (var payment in order.Payments)
                    {
                        Console.WriteLine($"\tPayment ID: {payment.PaymentId}, Amount: {payment.Amount}, Status: {payment.Status}");
                    }

                    Console.WriteLine("\nProceeding to delete the order and its associated order items and payments...\n");

                    // Remove order items first due to foreign key constraints
                    if (order.OrderItems.Any())
                    {
                        context.OrderItems.RemoveRange(order.OrderItems);
                        Console.WriteLine("Order items deleted successfully.");
                    }

                    // Remove payments associated with the order
                    if (order.Payments.Any())
                    {
                        context.Payments.RemoveRange(order.Payments);
                        Console.WriteLine("Payments deleted successfully.");
                    }

                    // Remove the order
                    context.Orders.Remove(order);
                    Console.WriteLine("Order deleted successfully.");

                    // Save all changes to the database
                    context.SaveChanges();
                    Console.WriteLine("\nAll changes have been saved to the database.");

                    // Confirm Deletion
                    Console.WriteLine("\nVerifying that the order and associated records have been deleted...");

                    // Attempt to retrieve the order again
                    var deletedOrder = context.Orders
                        .Include(o => o.OrderItems)
                        .Include(o => o.Payments)
                        .FirstOrDefault(o => o.OrderId == orderIdToDelete);

                    if (deletedOrder == null)
                    {
                        Console.WriteLine("Order deletion confirmed. The order and its associated order items and payments have been successfully deleted.");
                    }
                    else
                    {
                        Console.WriteLine("Order deletion failed. The order still exists in the database.");
                    }
                }
                else
                {
                    Console.WriteLine($"Order with ID {orderIdToDelete} not found. No deletion performed.");
                }
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Database Error: {ex.InnerException?.Message ?? ex.Message}");
            }
            catch (Exception ex)
            {
                // Display any errors that occur during the operations
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}