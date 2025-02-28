using EFCoreCodeFirstDemo.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCoreCodeFirstDemo
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Initialize the DbContext for interacting with the ECommerce database
                using var context = new ECommerceDbContext();
                Console.WriteLine("Creating an Order");

                // 1. Create a new order with multiple order items and an initial payment
                var order = new Order
                {
                    // Set the order date to the current UTC time
                    OrderDate = DateTime.UtcNow,

                    // Set the total amount for the order
                    TotalAmount = 250.00m,

                    // Define the order items associated with this order
                    OrderItems = new List<OrderItem>
                    {
                        // First item in the order
                        new OrderItem
                        {
                            ProductId = 1, // Refers to the product with ID 1
                            Quantity = 2,  // 2 units ordered
                            Price = 100.00m // Price per unit
                        },
                        // Second item in the order
                        new OrderItem
                        {
                            ProductId = 2, // Refers to the product with ID 2
                            Quantity = 1,  // 1 unit ordered
                            Price = 50.00m // Price per unit
                        }
                    },

                    // Define the payment details associated with the order
                    Payment = new Payment
                    {
                        PaymentDate = DateTime.UtcNow, // Set the payment date as the current UTC time
                        Amount = 250.00m, // Payment amount matches the total order amount
                        Status = PaymentStatus.Pending // Set the initial payment status to 'Pending'
                    }
                };

                // Add the new order to the DbSet in the context (but not yet saved to the database)
                context.Orders.Add(order);
                Console.WriteLine("Order added to the DbContext... Preparing to save changes.");

                // 2. Save the changes to the database, which inserts the order, order items, and payment
                context.SaveChanges();
                Console.WriteLine($"Order placed successfully with Order ID: {order.OrderId}.");

            }
            catch (DbUpdateException ex)
            {
                // Catch database update errors, such as constraint violations or connection issues
                Console.WriteLine($"Database Error placing order: {ex.InnerException?.Message ?? ex.Message}");

                // Any error during SaveChanges() automatically rolls back the transaction
            }
            catch (Exception ex)
            {
                // Catch any general exceptions that occur during the process
                Console.WriteLine($"Error placing order: {ex.Message}");

                // If an exception occurs, the changes are rolled back, and the database state remains consistent
            }
        }
    }
}