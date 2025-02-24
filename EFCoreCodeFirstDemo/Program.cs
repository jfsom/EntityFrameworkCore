using Microsoft.EntityFrameworkCore;
using EFCoreCodeFirstDemo.Entities;
namespace EFCoreCodeFirstDemo
{
    public class Program
    {
        static void Main(string[] args)
        {
            using (var context = new EFCoreDbContext())
            {
                //Seed the database with Products
                var products = new[]
                {
                    new Product { Name = "Laptop", Price = 1200m },
                    new Product { Name = "Smartphone", Price = 800m },
                    new Product { Name = "Headphones", Price = 150m }
                };

                context.Products.AddRange(products);
                context.SaveChanges();
                Console.WriteLine("Products added to the database.");

                // Create Orders with OrderItems
                // Fetching existing products from the database
                var laptop = context.Products.Single(p => p.Name == "Laptop");
                var smartphone = context.Products.Single(p => p.Name == "Smartphone");

                var order1 = new Order
                {
                    OrderDate = DateTime.UtcNow,
                    Status = OrderStatus.Pending,
                    OrderItems = new List<OrderItem>
                    {
                        new OrderItem { Product = laptop, Quantity = 1, UnitPrice = laptop.Price },
                        new OrderItem { Product = smartphone, Quantity = 2, UnitPrice = smartphone.Price }
                    }
                };

                var order2 = new Order
                {
                    OrderDate = DateTime.UtcNow.AddDays(-1),
                    Status = OrderStatus.Completed,
                    OrderItems = new List<OrderItem>
                    {
                        new OrderItem { Product = smartphone, Quantity = 1, UnitPrice = smartphone.Price }
                    }
                };

                context.Orders.AddRange(order1, order2);
                context.SaveChanges();
                Console.WriteLine("Orders added to the database.");

                // Fetch and display all orders with their items and products
                var orders = context.Orders
                    .Include(o => o.OrderItems) //Eager Load all Order Items
                        .ThenInclude(oi => oi.Product) //Eager Load Products
                    .ToList();

                Console.WriteLine("\n--- All Order Details ---");
                foreach (var order in orders)
                {
                    Console.WriteLine($"\nOrder ID: {order.Id}");
                    Console.WriteLine($"Order Date: {order.OrderDate.ToString("yyyy-MM-dd HH:mm:ss")}");
                    Console.WriteLine($"Order Status: {order.Status}");
                    Console.WriteLine("Order Items:");

                    // Loop through each OrderItem in the Order
                    foreach (var item in order.OrderItems)
                    {
                        Console.WriteLine($"\tName: {item.Product.Name}, Quantity: {item.Quantity}, Unit Price: ${item.UnitPrice}");
                        Console.WriteLine($"\tTotal Price for Item: ${item.Quantity * item.UnitPrice}\n");
                    }

                    // Calculate total cost for the order
                    var totalCost = order.OrderItems.Sum(oi => oi.Quantity * oi.UnitPrice);
                    Console.WriteLine($"Total Order Cost: ${totalCost}");
                }
            }
        }
    }
}