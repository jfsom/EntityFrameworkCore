using EFCorePropertyConfigurations.Entities;
using EFCoreCodeFirstDemo.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCoreCodeFirstDemo
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Initialize the database context
            using (var context = new EFCoreDbContext())
            {
                // Step 1: Create and add some products
                CreateProducts(context);

                // Step 2: Create and add some customers
                CreateCustomers(context);

                // Step 3: Create and add some orders with related order items
                CreateOrdersWithOrderItems(context);

                // Step 4: Display all customers and their respective orders
                DisplayAllOrders(context);
            }
        }

        // Step 1: Create and save a list of products to the database
        private static void CreateProducts(EFCoreDbContext context)
        {
            // Check if products already exist to avoid duplication
            if (context.Products.Any())
                return;

            // List of products to add
            var products = new List<Product>
            {
                new Product { Name = "Laptop", Price = 1200m },
                new Product { Name = "Smartphone", Price = 800m },
                new Product { Name = "Headphones", Price = 150m },
                new Product { Name = "Tablet", Price = 500m }
            };

            // Add products to the context
            context.Products.AddRange(products);

            // Save changes to the database
            context.SaveChanges();
            Console.WriteLine("Products have been added to the database.");
        }

        // Step 2: Create and save a list of customers to the database
        private static void CreateCustomers(EFCoreDbContext context)
        {
            // Check if customers already exist to avoid duplication
            if (context.Customers.Any())
                return;

            // List of customers to add
            var customers = new List<Customer>
            {
                new Customer
                {
                    FirstName = "Pranaya",
                    LastName = "Rout",
                    Email = "Pranaya.Rout@dotnettutorials.net",
                    PhoneNumber = "123-456-7890"
                },
                new Customer
                {
                    FirstName = "Rakesh",
                    LastName = "Kumar",
                    Email = "Rakesh.Kumar@dotnettutorials.net",
                    PhoneNumber = "098-765-4321"
                }
            };

            // Add customers to the context
            context.Customers.AddRange(customers);

            // Save changes to the database
            context.SaveChanges();
            Console.WriteLine("Customers have been added to the database.");
        }

        // Step 3: Create and save orders with related order items
        private static void CreateOrdersWithOrderItems(EFCoreDbContext context)
        {
            // Check if orders already exist to avoid duplication
            if (context.Orders.Any())
                return;

            // Fetch the customers who are going to place orders
            var customer1 = context.Customers.First(c => c.Email == "Pranaya.Rout@dotnettutorials.net");
            var customer2 = context.Customers.First(c => c.Email == "Rakesh.Kumar@dotnettutorials.net");

            // Fetch existing products
            var laptop = context.Products.First(p => p.Name == "Laptop");
            var smartphone = context.Products.First(p => p.Name == "Smartphone");

            // Create orders for each customer
            var orders = new List<Order>
            {
                new Order
                {
                    CustomerId = customer1.Id,
                    OrderDate = DateTime.Now,
                    //Status = OrderStatus.Pending, By Default it should be Pending
                    OrderItems = new List<OrderItem>
                    {
                        new OrderItem
                        {
                            ProductId = laptop.ProductId,
                            UnitPrice = laptop.Price,
                            Quantity = 1
                        },
                        new OrderItem
                        {
                            ProductId = smartphone.ProductId,
                            UnitPrice = smartphone.Price,
                            Quantity = 2
                        }
                    }
                },
                new Order
                {
                    CustomerId = customer2.Id,
                    OrderDate = DateTime.Now,
                    Status = OrderStatus.Completed,
                    OrderItems = new List<OrderItem>
                    {
                        new OrderItem
                        {
                            ProductId = smartphone.ProductId,
                            UnitPrice = smartphone.Price,
                            Quantity = 2
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

        // Step 4: Display all customers and their orders, including order items
        private static void DisplayAllOrders(EFCoreDbContext context)
        {
            // Fetch all customers with their orders and order items using eager loading
            var customersWithOrders = context.Customers
                .Include(c => c.Orders)          // Eager load related orders
                    .ThenInclude(o => o.OrderItems)  // Eager load related order items
                        .ThenInclude(oi => oi.Product) //Eager Load Related Products of OrderItems
                .ToList();

            Console.WriteLine("\n---------------------- All Orders for All Customers ----------------------\n");

            // Loop through each customer
            foreach (var customer in customersWithOrders)
            {
                Console.WriteLine($"Customer: {customer.FirstName} {customer.LastName} ({customer.Email})");

                // Loop through each order of the customer
                foreach (var order in customer.Orders)
                {
                    Console.WriteLine($"Order ID: {order.OrderId}, Order Status: {order.Status}, Order Date: {order.OrderDate:yyyy-MM-dd}");
                    Console.WriteLine("Order Items:");

                    // Loop through each order item
                    foreach (var item in order.OrderItems)
                    {
                        Console.WriteLine($"\tProduct Name: Name: {item.Product.Name}, Unit Price: {item.UnitPrice}, Quantity: {item.Quantity}");
                        Console.WriteLine($"\tTotal Price for Item: {item.TotalPrice}");
                    }

                    // Calculate total order cost
                    var totalOrderCost = order.OrderItems.Sum(oi => oi.TotalPrice);
                    Console.WriteLine($"Total Order Cost: {totalOrderCost}\n");
                }
            }
        }
    }
}