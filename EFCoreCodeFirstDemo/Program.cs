using EFCoreCodeFirstDemo.Entities;

namespace EFCoreCodeFirstDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (var context = new EFCoreDbContext())
                {
                    // Creating a new order
                    var order = new Order
                    {
                        OrderId = 1,
                        CustomerId = 101,
                        OrderDate = DateTime.Now
                    };

                    Console.WriteLine($"Creating Order: OrderId = {order.OrderId}, CustomerId = {order.CustomerId}, OrderDate = {order.OrderDate}");

                    // Adding the order to the context
                    context.Orders.Add(order);
                    Console.WriteLine("Order has been added to the context.");

                    // Creating the first order item
                    var orderItem1 = new OrderItem
                    {
                        OrderId = 1,          // Composite foreign key part 1
                        CustomerId = 101,     // Composite foreign key part 2
                        ProductName = "Laptop",
                        Quantity = 2
                    };

                    Console.WriteLine($"Creating OrderItem 1: ProductName = {orderItem1.ProductName}, Quantity = {orderItem1.Quantity}");

                    // Adding the first order item to the context
                    context.OrderItems.Add(orderItem1);
                    Console.WriteLine("OrderItem 1 has been added to the context.");

                    // Creating the second order item
                    var orderItem2 = new OrderItem
                    {
                        OrderId = 1,          // Composite foreign key part 1
                        CustomerId = 101,     // Composite foreign key part 2
                        ProductName = "Desktop",
                        Quantity = 1
                    };

                    Console.WriteLine($"Creating OrderItem 2: ProductName = {orderItem2.ProductName}, Quantity = {orderItem2.Quantity}");

                    // Adding the second order item to the context
                    context.OrderItems.Add(orderItem2);
                    Console.WriteLine("OrderItem 2 has been added to the context.");

                    // Saving all changes to the database
                    context.SaveChanges();
                    Console.WriteLine("Changes have been saved to the database.");
                }

                Console.WriteLine("Order Placed Successfully.");
            }
            catch (Exception ex)
            {
                // Handling any exceptions that occur during the execution
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}