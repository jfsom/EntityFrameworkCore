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
                // Create an instance of the DbContext to interact with the database
                using var context = new EcommerceDbContext();

                // *** Retrieve and Display Orders with Details ***

                Console.WriteLine("Fetching and displaying all orders with customer, order items, and payment details...\n");

                // Fetch orders including related data:
                var orders = context.Orders
                    .Include(o => o.Customer) // Include customer information
                    .Include(o => o.OrderItems) // Include order items
                        .ThenInclude(oi => oi.Product) // Include product details for each order item
                    .Include(o => o.Payments) // Include payments associated with the order
                    .Include(o => o.ShippingAddress) // Include shipping address
                    .ToList();

                // Check if any orders exist
                if (orders.Any())
                {
                    // Iterate through each order
                    foreach (var order in orders)
                    {
                        // Display basic order information
                        Console.WriteLine($"Order ID: {order.OrderId}, Date: {order.OrderDate}, Status: {order.Status}, Total Amount: {order.TotalAmount}");

                        // Display customer information
                        Console.WriteLine($"Customer: {order.Customer.FirstName} {order.Customer.LastName}");
                        Console.WriteLine($"Email: {order.Customer.Email}");
                        Console.WriteLine($"Phone: {order.Customer.Phone}");

                        // Display shipping address
                        var address = order.ShippingAddress;
                        Console.WriteLine("\nShipping Address:");
                        Console.WriteLine($"\t{address.AddressLine1}");
                        if (!string.IsNullOrEmpty(address.AddressLine2))
                        {
                            Console.WriteLine($"\t{address.AddressLine2}");
                        }
                        Console.WriteLine($"\t{address.City}, {address.State}, {address.PostalCode}, {address.Country}");

                        // Display order items
                        Console.WriteLine("\nOrder Items:");
                        foreach (var item in order.OrderItems)
                        {
                            Console.WriteLine($"\tProduct: {item.Product.ProductName}, Quantity: {item.Quantity}, Unit Price: {item.UnitPrice}, Total Price: {item.TotalPrice}");
                            Console.WriteLine(); // Blank line for readability
                        }

                        // Display payment details
                        Console.WriteLine("Payments:");
                        foreach (var payment in order.Payments)
                        {
                            Console.WriteLine($"\tPayment ID: {payment.PaymentId}, Amount: {payment.Amount}, Payment Method: {payment.PaymentMethod}");
                            Console.WriteLine($"\tPayment Date: {payment.PaymentDate}, Transaction ID: {payment.TransactionId}, Payment Status: {payment.Status}");
                            Console.WriteLine(); // Blank line for readability
                        }

                        Console.WriteLine(); // Separator for readability
                    }
                }
                else
                {
                    Console.WriteLine("No orders found in the database.");
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