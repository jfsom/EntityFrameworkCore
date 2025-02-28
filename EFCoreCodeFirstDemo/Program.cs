using EFCoreCodeFirstDemo.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCoreCodeFirstDemo
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                // Initialize the ECommerceDbContext and start the transaction
                using var context = new ECommerceDbContext();

                // Begin a transaction asynchronously
                await using var transaction = await context.Database.BeginTransactionAsync();
                Console.WriteLine("Transaction started...");

                try
                {
                    // 1. Create a new order with order items and a pending payment
                    var order = new Order
                    {
                        OrderDate = DateTime.UtcNow, // Set the current UTC time for the order date
                        TotalAmount = 500.00m, // Total amount for the order
                        OrderItems = new List<OrderItem>
                        {
                            // Create a new order item for the order
                            new OrderItem { ProductId = 4, Quantity = 5, Price = 100.00m }
                        },
                        Payment = new Payment
                        {
                            PaymentDate = DateTime.UtcNow, // Set the payment date as current UTC time
                            Amount = 500.00m, // Payment amount matches the order total
                            Status = PaymentStatus.Pending // Initially set payment status to Pending
                        }
                    };

                    // Add the new order to the context
                    context.Orders.Add(order);
                    Console.WriteLine("Order and its details added to the DbContext...");

                    // Save the changes asynchronously to store the order in the database
                    await context.SaveChangesAsync();
                    Console.WriteLine("Order saved to the database with a pending payment.");

                    // 2. Update the product inventory based on the order
                    var orderItem = order.OrderItems.First(); // Get the first (and only) order item
                    var product = await context.Products.FirstOrDefaultAsync(p => p.ProductId == orderItem.ProductId); // Fetch the product from the database

                    if (product != null)
                    {
                        // Update the product's inventory (subtract the ordered quantity)
                        product.Quantity -= orderItem.Quantity;
                        product.IsInStock = product.Quantity > 0; // Set IsInStock based on remaining quantity
                        context.Products.Update(product); // Update the product in the context

                        Console.WriteLine($"Product ID {product.ProductId} inventory updated. Remaining stock: {product.Quantity}");

                        // Save the changes asynchronously to update the product inventory in the database
                        await context.SaveChangesAsync();
                        Console.WriteLine("Product inventory updated successfully.");
                    }
                    else
                    {
                        // If the product is not found, throw an exception
                        throw new Exception("Product not found.");
                    }

                    // 3. Process the payment for the order
                    order.Payment.Status = PaymentStatus.Completed; // Set the payment status to Completed
                    context.Payments.Update(order.Payment); // Update the payment in the context
                    Console.WriteLine("Payment status updated to 'Completed'...");

                    // Save the changes asynchronously to update the payment status in the database
                    await context.SaveChangesAsync();
                    Console.WriteLine("Payment processed and updated in the database.");

                    // Commit the transaction after all operations have succeeded
                    await transaction.CommitAsync();
                    Console.WriteLine("Transaction committed successfully.");
                    Console.WriteLine($"Order ID {order.OrderId} placed, inventory updated, and payment processed successfully.");
                }
                catch (Exception ex)
                {
                    // If any error occurs, roll back the transaction
                    await transaction.RollbackAsync();
                    Console.WriteLine($"Transaction failed. Rolling back changes. Error: {ex.Message}");
                }
            }
            catch (DbUpdateException ex)
            {
                // Handle database update exceptions (e.g., constraint violations)
                Console.WriteLine($"Database Error: {ex.InnerException?.Message ?? ex.Message}");
            }
            catch (Exception ex)
            {
                // Handle all other exceptions
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}