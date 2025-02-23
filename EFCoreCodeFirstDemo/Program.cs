using EFCoreCodeFirstDemo.Entities;

namespace EFCoreCodeFirstDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var context = new EFCoreDbContext())
            {
                // Ensure the database is fresh by deleting and recreating it
                context.Database.EnsureDeleted(); // Delete existing database
                context.Database.EnsureCreated(); // Create new database based on the model

                // Seed Data
                Console.WriteLine("Seeding data into the database...");

                // Create a new product
                var product = new Product
                {
                    ProductId = Guid.NewGuid(), // PK without Identity; must provide a unique value
                    Name = "Laptop",
                    Category = "ELECT",
                    Price = 1500.00m,
                    Quantity = 10,
                };
                context.Products.Add(product);

                // Create a new customer
                var customer = new Customer
                {
                    // CustomerId is a computed column
                    // SerialNumber is Identity and will be auto-generated
                    Name = "Pranaya Rout",
                    Email = "pranaya.rout@example.com",
                    PhoneNumber = "123-456-7890"
                };
                context.Customers.Add(customer);

                // Save changes to generate identity and computed fields for Product and Customer
                context.SaveChanges();

                // Create a new order
                var order = new Order
                {
                    // OrderId is Identity and will be auto-generated
                    CustomerId = customer.CustomerId,
                    TotalAmount = 1500.00m
                    // OrderDate has Default value as current date
                    // Status has a default value of Pending
                };
                context.Orders.Add(order);

                // Save changes to generate identity and computed fields for Order
                context.SaveChanges();

                // Create a new order item
                var orderItem = new OrderItem
                {
                    // OrderItemId is Identity and will be auto-generated
                    OrderId = order.OrderId, // FK to Order.OrderId
                    ProductId = product.ProductId, // FK to Product.ProductId
                    Quantity = 1,
                    Price = 1500.00m
                    // TotalPrice is a computed column based on Quantity * Price
                };
                context.OrderItems.Add(orderItem);

                // Save changes to generate identity and computed fields for OrderItem
                context.SaveChanges();

                // Display the data
                Console.WriteLine("\nData saved successfully. Displaying the data:\n");

                // Use the entities we have in memory, which have been updated with database-generated values
                Console.WriteLine($"Product:");
                Console.WriteLine($"\tProductId: {product.ProductId}, Name: {product.Name}, SerialNumber: {product.SerialNumber}");
                Console.WriteLine($"\tSKU: {product.SKU}, CreatedOn: {product.CreatedOn}, CreatedBy: {product.CreatedBy}");

                Console.WriteLine($"\nCustomer:");
                Console.WriteLine($"\tCustomerId: {customer.CustomerId}, Name: {customer.Name}");
                Console.WriteLine($"\tEmail: {customer.Email}, PhoneNumber: {customer.PhoneNumber}");

                Console.WriteLine($"\nOrder:");
                Console.WriteLine($"\tOrderId: {order.OrderId}, TotalAmount: {order.TotalAmount}, OrderDate: {order.OrderDate}");
                Console.WriteLine($"\tStatus: {order.Status}, CustomerId: {order.CustomerId}");

                Console.WriteLine($"\nOrderItem:");
                Console.WriteLine($"\tOrderItemId: {orderItem.OrderItemId}, OrderId: {orderItem.OrderId}, ProductId: {orderItem.ProductId}");
                Console.WriteLine($"\tQuantity: {orderItem.Quantity}, Price: {orderItem.Price}, TotalPrice: {orderItem.TotalPrice}");
            }
        }
    }
}