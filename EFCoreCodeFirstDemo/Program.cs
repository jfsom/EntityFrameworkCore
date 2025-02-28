using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using EFCoreCodeFirstDemo.Models;

namespace EcommerceApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Create an instance of the DbContext
            using var context = new EcommerceDbContext();

            var dbTransaction = context.Database.BeginTransaction();

            try
            {
                // *** Step 1: Create a New Order with Discount Applied ***

                Console.WriteLine("Creating a new order with discount applied...\n");

                // Define the existing CustomerID and ProductID
                int existingCustomerId = 1; // Replace with actual CustomerID
                int existingProductId = 1;   // Replace with actual ProductID

                // Retrieve the existing customer and product from the database
                var customer = context.Customers.FirstOrDefault(c => c.CustomerId == existingCustomerId);
                var product1 = context.Products.FirstOrDefault(p => p.ProductId == existingProductId);
                var product2 = context.Products.FirstOrDefault(p => p.ProductId == 2);

                if (customer == null || product1 == null || product2 == null)
                {
                    Console.WriteLine("Customer or Product not found. Cannot proceed with order creation.");
                    return;
                }

                // Retrieve the customer's default shipping address
                var shippingAddress = context.Addresses
                    .FirstOrDefault(a => a.CustomerId == customer.CustomerId && a.IsDefault == true);

                if (shippingAddress == null)
                {
                    Console.WriteLine("Shipping address not found for the customer.");
                    return;
                }

                // Create a new order for the customer
                var newOrder = new Order
                {
                    CustomerId = customer.CustomerId,
                    ShippingAddressId = shippingAddress.AddressId,
                    Status = "Pending",
                    CreatedDate = DateTime.Now
                };
                context.Orders.Add(newOrder);

                try
                {
                    context.SaveChanges(); // Save to get the OrderID

                    Console.WriteLine($"Order created successfully with OrderID: {newOrder.OrderId}\n");

                    // Add an order item to the new order
                    int quantityOrdered = 2; // Assuming the customer orders 2 units

                    List<OrderItem> orderItems = new List<OrderItem>()
                    {
                        new OrderItem
                        {
                            OrderId = newOrder.OrderId,
                            ProductId = product1.ProductId,
                            Quantity = quantityOrdered,
                            UnitPrice = product1.Price,
                            TotalPrice = product1.Price * quantityOrdered,
                            CreatedDate = DateTime.Now
                        },
                        new OrderItem
                        {
                            OrderId = newOrder.OrderId,
                            ProductId = product2.ProductId,
                            Quantity = 1,
                            UnitPrice = product2.Price,
                            TotalPrice = product2.Price * 1,
                            CreatedDate = DateTime.Now
                        }
                    };

                    context.OrderItems.AddRange(orderItems);

                    // Update the order's total amount before discount
                    decimal totalAmountBeforeDiscount = orderItems.Sum(ord => ord.TotalPrice);

                    // Apply the discount using the stored function 'CalculateDiscount'
                    decimal? discount = context.Orders
                        .Where(o => o.OrderId == newOrder.OrderId)
                        .Select(o => EcommerceDbContext.CalculateDiscount(totalAmountBeforeDiscount))
                        .FirstOrDefault();

                    // Ensure discount is not null
                    discount ??= 0;

                    // Calculate the total amount after applying the discount
                    decimal totalAmountAfterDiscount = totalAmountBeforeDiscount - discount.Value;

                    // Update the order's total amount
                    newOrder.TotalAmount = totalAmountAfterDiscount;

                    // Save changes to persist updates
                    context.SaveChanges();
                    Console.WriteLine($"Order item added, discount applied, and order total amount updated.\n");

                    // Display discount details
                    Console.WriteLine($"Amount Before Discount: {totalAmountBeforeDiscount}, Discount Applied: {discount.Value}, Amount After Discount: {totalAmountAfterDiscount}\n");

                    // Process payment for the order
                    var payment = new Payment
                    {
                        OrderId = newOrder.OrderId,
                        Amount = Convert.ToDecimal(newOrder.TotalAmount), // Amount after discount
                        PaymentMethod = "Credit Card",
                        TransactionId = "TXN" + DateTime.Now.Ticks,
                        Status = "Processing",
                        //Discount = discount,
                        CreatedDate = DateTime.Now
                    };
                    context.Payments.Add(payment);
                    context.SaveChanges();
                    Console.WriteLine("Payment processed for the new order with discounted amount.\n");

                    dbTransaction.Commit();
                    // *** Step 2: Use the View 'OrderDetailsView' ***
                }
                catch (Exception)
                {
                    dbTransaction.Rollback();
                }

                Console.WriteLine("Fetching order details using the 'OrderDetailsView'...\n");

                // Retrieve order details for the new order
                var orderDetails = context.OrderDetailsViews
                    .Where(od => od.OrderId == newOrder.OrderId)
                    .ToList();

                if (orderDetails.Any())
                {
                    foreach (var detail in orderDetails)
                    {
                        Console.WriteLine($"Order ID: {detail.OrderId}, Date: {detail.OrderDate}, Status: {detail.OrderStatus}");
                        Console.WriteLine($"Customer Name: {detail.CustomerName}, Shipping Address: {detail.ShippingAddress}");
                        Console.WriteLine($"Product Name: {detail.ProductName}, Quantity: {detail.Quantity}, Unit Price: {detail.UnitPrice}, Total Price: {detail.TotalPrice} \n");
                    }
                }
                else
                {
                    Console.WriteLine("No order details found for the new order.");
                }

                // *** Step 3: Use the Stored Procedure 'GetOrdersByCustomerID' ***

                Console.WriteLine("\nFetching orders for the customer using the stored procedure 'GetOrdersByCustomerID'...\n");

                // Define the parameter
                var customerIdParam = new SqlParameter("@CustomerID", customer.CustomerId);

                // Execute the stored procedure
                var orders = context.Orders
                    .FromSqlRaw("EXEC GetOrdersByCustomerID @CustomerID", customerIdParam)
                    .ToList();

                if (orders.Any())
                {
                    foreach (var order in orders)
                    {
                        Console.WriteLine($"Order ID: {order.OrderId}, Date: {order.OrderDate}, Status: {order.Status}, Total Amount: {order.TotalAmount}");
                    }
                }
                else
                {
                    Console.WriteLine("No orders found for the customer.");
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