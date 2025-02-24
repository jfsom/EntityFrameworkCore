using Microsoft.EntityFrameworkCore;
using EFCoreCodeFirstDemo.Entities;

namespace EFCoreCodeFirstDemo
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Initialize the database context
            using (var context = new EFCoreDbContext())
            {
                // 1. Creating Some Products
                CreateProducts(context);

                // 2. Creating a Few Customers
                CreateCustomers(context);

                // 3. Creating Orders with Related Order Items
                CreateOrdersWithOrderItems(context);

                // 4. Displaying All Customers All Orders 
                DisplayAllOrders(context);
            }
        }

        // Creates and saves a list of products to the database.
        private static void CreateProducts(EFCoreDbContext context)
        {
            // Check if products already exist to avoid duplication
            if (context.Products.Any())
                return;

            // List of products to add
            var products = new List<Product>
            {
                new Product { Name = "Laptop", Price = 1200m, Description = "High-performance laptop" },
                new Product { Name = "Smartphone", Price = 800m, Description = "Latest model smartphone" },
                new Product { Name = "Headphones", Price = 150m, Description = "Noise-cancelling headphones" },
                new Product { Name = "Tablet", Price = 500m, Description = "10-inch display tablet" }
            };

            // Add products to the context
            context.Products.AddRange(products);

            // Save changes to the database
            context.SaveChanges();
            Console.WriteLine("Products have been added to the database.");
        }

        // Creates and saves a list of customers to the database.
        private static void CreateCustomers(EFCoreDbContext context)
        {
            // Check if customers already exist to avoid duplication
            if (context.Customers.Any())
                return;

            //Adding List of Customers
            var customers = new List<Customer>()
            {
                new Customer
                {
                    FirstName = "Pranaya",
                    LastName = "Rout",
                    Email = "Pranaya.Rout@DotNetTutorials.NET",
                    Address = new Address { Street = "123 Main St", City = "BBSR" }
                },
                new Customer
                {
                    FirstName = "Rakesh",
                    LastName = "Kumar",
                    Email = "Rakesh.Kumar@DotNetTutorials.NET",
                    Address = new Address { Street = "456 Oak St", City = "Mumbai" }
                }
             };

            // Add customers to the context
            context.Customers.AddRange(customers);

            // Save changes to the database
            context.SaveChanges();
            Console.WriteLine("Customers have been added to the database.");
        }

        // Creates orders with related order items for the customers.
        private static void CreateOrdersWithOrderItems(EFCoreDbContext context)
        {
            // Check if orders already exist to avoid duplication
            if (context.Orders.Any())
                return;

            // Fetch existing customers
            //Fetch the existing Customes who wants to Place Order
            var customer1 = context.Customers.First(c => c.Email == "Pranaya.Rout@DotNetTutorials.NET");
            var customer2 = context.Customers.First(c => c.Email == "Rakesh.Kumar@DotNetTutorials.NET");

            // Fetch existing products
            var laptop = context.Products.First(p => p.Name == "Laptop");
            var smartphone = context.Products.First(p => p.Name == "Smartphone");
            var headphones = context.Products.First(p => p.Name == "Headphones");
            var tablets = context.Products.First(p => p.Name == "Tablet");

            // Create orders for each customer
            var orders = new List<Order>
            {
                new Order
                {
                    CustomerId = customer1.CustomerId,
                    OrderDate = DateTime.Now,
                    Status = OrderStatus.Pending,
                    OrderItems = new List<OrderItem>
                    {
                        new OrderItem
                        {
                            ProductId = laptop.ProductId,
                            Quantity = 1,
                            UnitPrice = laptop.Price
                        },
                        new OrderItem
                        {
                            ProductId = headphones.ProductId,
                            Quantity = 2,
                            UnitPrice = headphones.Price
                        }
                    }
                },
                new Order
                {
                    CustomerId = customer2.CustomerId,
                    OrderDate = DateTime.Now.AddDays(-1),
                    Status = OrderStatus.Processing,
                    OrderItems = new List<OrderItem>
                    {
                        new OrderItem
                        {
                            ProductId = smartphone.ProductId,
                            Quantity = 1,
                            UnitPrice = smartphone.Price
                        }
                    }
                },
                new Order
                {
                    CustomerId = customer1.CustomerId,
                    OrderDate = DateTime.Now.AddDays(-2),
                    Status = OrderStatus.Completed,
                    OrderItems = new List<OrderItem>
                    {
                        new OrderItem
                        {
                            ProductId = tablets.ProductId,
                            Quantity = 3,
                            UnitPrice = tablets.Price
                        }
                    }
                }
            };

            // Add orders to the context
            context.Orders.AddRange(orders);

            // Save changes to the database
            context.SaveChanges();
            Console.WriteLine("Orders have been created for the customers.");
        }

        // Displays all orders along with their order items for all customers.
        private static void DisplayAllOrders(EFCoreDbContext context)
        {
            // Step 4: Display all orders of all customers
            var customersWithOrders = context.Customers
                .Include(c => c.Orders) //Eager Load Related Order of Customer
                    .ThenInclude(o => o.OrderItems) //Eager Load Related OrderItems of Orders
                        .ThenInclude(oi => oi.Product) //Eager Load Related Products of OrderItems
                .ToList();

            Console.WriteLine("\n---------------------- All Orders for All Customers ----------------------\n");

            foreach (var customer in customersWithOrders)
            {
                Console.WriteLine($"Customer: {customer.FirstName} {customer.LastName} ({customer.Email}) Address: {customer.Address.Street}, {customer.Address.City}");

                foreach (var order in customer.Orders)
                {
                    Console.WriteLine($"Order ID: {order.OrderId}, Order Status: {order.Status}, Order Date: {order.OrderDate:yyyy-MM-dd} (UTC)");
                    Console.WriteLine("Order Items:");

                    foreach (var item in order.OrderItems)
                    {
                        Console.WriteLine($"\tProduct Name: {item.Product.Name}, Quantity: {item.Quantity}, Description: {item.Product.Description}, Unit Price: ${item.UnitPrice:F2}");
                        Console.WriteLine($"\tTotal Price for Item: ${item.Quantity * item.UnitPrice:F2}");
                    }

                    // Calculate total order cost
                    var totalCost = order.OrderItems.Sum(oi => oi.Quantity * oi.UnitPrice);
                    Console.WriteLine($"Total Order Cost: ${totalCost:F2}");
                    Console.WriteLine(); //Line spacing
                }
            }
        }
    }
}