using EFCoreCodeFirstDemo.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCoreCodeFirstDemo
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            // Initialize the DbContext for interacting with the ECommerce database
            using var context = new ECommerceDbContext();

            // Begin a manual transaction using the context
            using var transaction = await context.Database.BeginTransactionAsync();
            Console.WriteLine("Transaction started...");

            try
            {
                // 1. Create a new order with order items and initial payment status (Pending)
                var order = new Order
                {
                    OrderDate = DateTime.UtcNow, // Set the current UTC time for the order date
                    TotalAmount = 150.00m, // Set the total amount of the order

                    // Define the items in the order (in this case, one item)
                    OrderItems = new List<OrderItem>
                    {
                        new OrderItem
                        {
                            ProductId = 1, // The ProductId of the item being ordered
                            Quantity = 1, // Number of units ordered
                            Price = 150.00m // Price per unit
                        }
                    },

                    // Define the payment details associated with the order
                    Payment = new Payment
                    {
                        PaymentDate = DateTime.UtcNow, // Set the payment date as the current UTC time
                        Amount = 150.00m, // Set the payment amount to match the order total
                        Status = PaymentStatus.Pending // Set the initial payment status to 'Pending'
                    }
                };

                // Add the new order to the Orders DbSet in the context
                context.Orders.Add(order);
                Console.WriteLine("Order and associated payment details added to the DbContext...");

                // Save changes asynchronously to the database (this inserts the order, items, and payment)
                await context.SaveChangesAsync();
                Console.WriteLine($"Order ID {order.OrderId} created with initial payment status '{order.Payment.Status}'.");

                // 2. Update the product inventory based on the ordered item
                var orderItem = order.OrderItems.First(); // Get the first (and only) order item
                var product = await context.Products.FirstOrDefaultAsync(p => p.ProductId == orderItem.ProductId); // Fetch the product from the database

                if (product != null)
                {
                    // Reduce the product's quantity based on the ordered quantity
                    product.Quantity -= orderItem.Quantity;

                    // Update the 'In Stock' status based on the new quantity
                    product.IsInStock = product.Quantity > 0;

                    // Update the product in the context
                    context.Products.Update(product);
                    Console.WriteLine($"Updating inventory for product '{product.Name}'...");

                    // Save changes asynchronously to reflect the updated inventory in the database
                    await context.SaveChangesAsync();
                    Console.WriteLine($"Product '{product.Name}' inventory updated. New Quantity: {product.Quantity}. In Stock: {product.IsInStock}");
                }
                else
                {
                    // If the product is not found, throw an exception to trigger a rollback
                    throw new Exception("Product not found in the inventory.");
                }

                // 3. Update the payment status to 'Completed' after successful inventory update
                order.Payment.Status = PaymentStatus.Completed; // Update the payment status to 'Completed'
                context.Payments.Update(order.Payment); // Update the payment entity in the context
                Console.WriteLine($"Updating payment status for Order ID {order.OrderId}...");

                // Save changes asynchronously to update the payment status in the database
                await context.SaveChangesAsync();
                Console.WriteLine($"Payment status for Order ID {order.OrderId} updated to '{order.Payment.Status}'.");

                // Commit the transaction if all operations are successful
                await transaction.CommitAsync();
                Console.WriteLine($"Transaction committed successfully for Order ID {order.OrderId}.");
            }
            catch (DbUpdateException ex)
            {
                // Rollback the transaction if a database update error occurs
                await transaction.RollbackAsync();
                Console.WriteLine($"Transaction rolled back due to Database Error: {ex.InnerException?.Message ?? ex.Message}");
            }
            catch (Exception ex)
            {
                // Rollback the transaction if any other general error occurs
                await transaction.RollbackAsync();
                Console.WriteLine($"Transaction rolled back due to an error: {ex.Message}");
            }
        }
    }
}