using EFCoreCodeFirstDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using var context = new EcommerceDbContext();
            var dbTransction = context.Database.BeginTransaction();

            try
            {
                // *** Create Operations ***

                // Adding Categories
                Console.WriteLine("Adding new categories...");

                // Create a new 'Electronics' category
                var electronicsCategory = new Category
                {
                    CategoryName = "Electronics",
                    Description = "Electronic devices and gadgets",
                    CreatedDate = DateTime.Now
                };
                context.Categories.Add(electronicsCategory);

                // Create a new 'Clothing' category
                var clothingCategory = new Category
                {
                    CategoryName = "Clothing",
                    Description = "Men's and Women's Clothing",
                    CreatedDate = DateTime.Now
                };
                context.Categories.Add(clothingCategory);

                // Save categories to the database
                context.SaveChanges();
                Console.WriteLine("Categories 'Electronics' and 'Clothing' added successfully.\n");

                // Adding Products
                Console.WriteLine("Adding new products...");

                // Create a new product 'Smartphone' under 'Electronics' category
                var product1 = new Product
                {
                    ProductName = "Smartphone",
                    Description = "Latest model smartphone",
                    Price = 699.99M,
                    StockQuantity = 50,
                    CategoryId = electronicsCategory.CategoryId,
                    CreatedDate = DateTime.Now
                };
                context.Products.Add(product1);

                // Create a new product 'Jeans' under 'Clothing' category
                var product2 = new Product
                {
                    ProductName = "Jeans",
                    Description = "Blue denim jeans",
                    Price = 49.99M,
                    StockQuantity = 100,
                    CategoryId = clothingCategory.CategoryId,
                    CreatedDate = DateTime.Now
                };
                context.Products.Add(product2);

                // Save products to the database
                context.SaveChanges();
                Console.WriteLine("Products 'Smartphone' and 'Jeans' added successfully.\n");

                // Adding a Customer
                Console.WriteLine("Adding a new customer...");

                // Create a new customer 'John Doe'
                var customer = new Customer
                {
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@example.com",
                    Phone = "1234567890",
                    DateOfBirth = new DateOnly(1990, 1, 1),
                    CreatedDate = DateTime.Now
                };
                context.Customers.Add(customer);

                // Save customer to the database
                context.SaveChanges();
                Console.WriteLine($"Customer '{customer.FirstName} {customer.LastName}' added successfully with CustomerID: {customer.CustomerId}\n");

                // Adding Customer Address
                Console.WriteLine("Adding address for the customer...");

                // Create a new address for the customer
                var address1 = new Address
                {
                    CustomerId = customer.CustomerId,
                    AddressLine1 = "123 Main Street",
                    AddressLine2 = null,
                    City = "Anytown",
                    State = "Anystate",
                    PostalCode = "12345",
                    Country = "USA",
                    IsDefault = false,
                    CreatedDate = DateTime.Now
                };
                context.Addresses.Add(address1);

                var address2 = new Address
                {
                    CustomerId = customer.CustomerId,
                    AddressLine1 = "123 Main Street",
                    AddressLine2 = null,
                    City = "Anytown",
                    State = "Anystate",
                    PostalCode = "12345",
                    Country = "USA",
                    IsDefault = true,
                    CreatedDate = DateTime.Now
                };
                context.Addresses.Add(address2);

                // Save address to the database
                context.SaveChanges();
                Console.WriteLine("Customer addresses added successfully.\n");

                // Creating an Order
                Console.WriteLine("Creating a new order for the customer...");

                // Create a new order for the customer with the shipping address
                var order = new Order
                {
                    CustomerId = customer.CustomerId,
                    ShippingAddressId = address2.AddressId,
                    TotalAmount = product1.Price, // We are setting Total amount is the price of the product. But we need to calculate the Total Amount
                    Status = "Pending",
                    CreatedDate = DateTime.Now
                };
                context.Orders.Add(order);

                // Save order to the database
                context.SaveChanges();
                Console.WriteLine($"Order created successfully with OrderID: {order.OrderId}\n");

                // Adding Order Item
                Console.WriteLine("Adding order item to the order...");

                // Create a new order item for the order
                var orderItem = new OrderItem
                {
                    OrderId = order.OrderId,
                    ProductId = product1.ProductId,
                    Quantity = 1,
                    UnitPrice = product1.Price,
                    TotalPrice = product1.Price * 1,
                    CreatedDate = DateTime.Now
                };
                orderItem.TotalPrice = orderItem.UnitPrice * orderItem.Quantity;

                context.OrderItems.Add(orderItem);

                // Save order item to the database
                context.SaveChanges();
                Console.WriteLine("Order item added successfully.\n");

                // Processing Payment
                Console.WriteLine("Processing payment for the order...");

                // Create a new payment for the order
                var payment = new Payment
                {
                    OrderId = order.OrderId,
                    Amount = Convert.ToDecimal(order.TotalAmount),
                    PaymentMethod = "Credit Card",
                    TransactionId = "TXN123456",
                    Status = "Pending",
                    CreatedDate = DateTime.Now
                };
                context.Payments.Add(payment);

                // Save payment to the database
                context.SaveChanges();

                dbTransction.Commit();
                Console.WriteLine("Payment processed successfully.\n");
            }
            catch (DbUpdateException ex)
            {
                dbTransction.Rollback();
                Console.WriteLine($"Database Error: {ex.InnerException?.Message ?? ex.Message}");
            }
            catch (Exception ex)
            {
                dbTransction.Rollback();
                // Display any errors that occur during the operations
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}