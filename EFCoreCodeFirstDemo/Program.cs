using EFCoreCodeFirstDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using var context = new EcommerceDbContext();
            var dbTransaction = context.Database.BeginTransaction();

            try
            {
                // *** Update Order Status, Payment Status, and Adjust Stock Quantities ***

                // Specify the Order ID that you want to update
                int orderIdToUpdate = 1; // Change this to the actual Order ID

                Console.WriteLine($"Updating Order ID: {orderIdToUpdate}\n");

                // Retrieve the order including related entities:
                // - Include Order Items and their associated Products
                // - Include Payments associated with the order
                var order = context.Orders
                    .Include(o => o.OrderItems)
                        .ThenInclude(oi => oi.Product)
                    .Include(o => o.Payments)
                    .FirstOrDefault(o => o.OrderId == orderIdToUpdate);

                // Check if the order exists
                if (order != null)
                {
                    // Update payment status if payment exists
                    //var payment = order.Payments.FirstOrDefault();

                    foreach (var payment in order.Payments)
                    {
                        // Display current payment status
                        Console.WriteLine($"Current Payment Status: {payment.Status}");

                        // Update the payment status to 'Completed'
                        payment.Status = "Completed";

                        // Display updated payment status
                        Console.WriteLine($"Updated Payment Status: {payment.Status}\n");
                    }

                    //Updating Order Status
                    // Display current order status
                    Console.WriteLine($"Current Order Status: {order.Status}");

                    // Update the order status to 'Processing'
                    order.Status = "Processing";

                    // Display updated order status
                    Console.WriteLine($"Updated Order Status: {order.Status}\n");

                    // Adjust stock quantities for each product in the order
                    Console.WriteLine("Adjusting stock quantities for ordered products...");
                    foreach (var orderItem in order.OrderItems)
                    {
                        var product = orderItem.Product;

                        // Display current stock quantity
                        Console.WriteLine($"Product: {product.ProductName}, Current Stock Quantity: {product.StockQuantity}");

                        // Decrease the stock quantity by the quantity ordered
                        product.StockQuantity = product.StockQuantity - orderItem.Quantity;

                        // Ensure stock quantity does not go negative
                        if (product.StockQuantity < 0)
                        {
                            product.StockQuantity = 0;
                        }

                        // Display updated stock quantity
                        Console.WriteLine($"Updated Stock Quantity: {product.StockQuantity}\n");
                    }

                    // Save all changes to the database
                    context.SaveChanges();

                    dbTransaction.Commit();
                    Console.WriteLine("Order status, payment status, and stock quantities updated successfully.\n");
                }
                else
                {
                    Console.WriteLine($"Order with ID {orderIdToUpdate} not found.\n");
                }

                // Display the updated order and product details for confirmation
                Console.WriteLine("Fetching updated order and product details...\n");

                // Retrieve the updated order
                var updatedOrder = context.Orders
                    .Include(o => o.OrderItems)
                        .ThenInclude(oi => oi.Product)
                    .Include(o => o.Payments)
                    .FirstOrDefault(o => o.OrderId == orderIdToUpdate);

                if (updatedOrder != null)
                {
                    // Display order status
                    Console.WriteLine($"Order ID: {updatedOrder.OrderId}, Order Status: {updatedOrder.Status}");

                    // Display payment status
                    var updatedPayment = updatedOrder.Payments.FirstOrDefault();
                    if (updatedPayment != null)
                    {
                        Console.WriteLine($"Payment Status: {updatedPayment.Status}");
                    }

                    // Display updated stock quantities of products
                    Console.WriteLine("\nUpdated Product Stock Quantities:");
                    foreach (var orderItem in updatedOrder.OrderItems)
                    {
                        var product = orderItem.Product;
                        Console.WriteLine($"Product: {product.ProductName}, Stock Quantity: {product.StockQuantity}");
                    }
                }
                else
                {
                    Console.WriteLine("Order not found after update.\n");
                }
            }
            catch (DbUpdateException ex)
            {
                dbTransaction.Rollback();
                Console.WriteLine($"Database Error: {ex.InnerException?.Message ?? ex.Message}");
            }
            catch (Exception ex)
            {
                dbTransaction.Rollback();
                // Display any errors that occur during the operations
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}