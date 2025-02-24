using EFCoreCodeFirstDemo.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCoreCodeFirstDemo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                // Create a DbContext instance for the database interaction, using 'await using' to ensure proper disposal after the operations are completed
                await using var context = new EFCoreDbContext();

                await CreateProductAsync(context);
                await ReadProductsAsync(context);
                await UpdateProductAsync(context);
                await DeleteProductAsync(context);
            }
            catch (DbUpdateException ex)
            {
                // Handle database update exceptions
                Console.WriteLine($"Database Error Occurred: {ex.InnerException?.Message ?? ex.Message}");
            }
            catch (Exception ex)
            {
                // Handle any other exceptions
                Console.WriteLine($"Error Occurred: {ex.Message}");
            }
        }

        // Asynchronous method for creating products in the database
        private static async Task CreateProductAsync(EFCoreDbContext context)
        {
            // Check if products already exist, this ensures we don't insert duplicates
            if (context.Products.Any())
                return;

            // Define two new products
            var product1 = new Product
            {
                Name = "Laptop",
                Description = "A high-performance laptop",
                Price = 1200.00m
            };

            var product2 = new Product
            {
                Name = "Desktop",
                Description = "A high-performance Desktop",
                Price = 1500.00m
            };

            // Add products asynchronously to the DbSet
            await context.Products.AddAsync(product1);
            await context.Products.AddAsync(product2);

            // Save the changes asynchronously to the database
            await context.SaveChangesAsync();

            Console.WriteLine($"Products Added Successfully.\n");
        }

        // Asynchronous method for reading all products from the database
        private static async Task ReadProductsAsync(EFCoreDbContext context)
        {
            // Fetch the list of all products asynchronously
            var products = await context.Products.ToListAsync();

            // Display each product's details
            Console.WriteLine("Products in database:");
            foreach (var product in products)
            {
                Console.WriteLine($"\tProduct Id: {product.Id}, Name: {product.Name}, Price: ${product.Price}");
            }
        }

        // Asynchronous method for updating the price and description of a specific product
        private static async Task UpdateProductAsync(EFCoreDbContext context)
        {
            // Find the first product with the name "Laptop" asynchronously
            var product = await context.Products.FirstOrDefaultAsync(prd => prd.Name == "Laptop");

            if (product != null)
            {
                // Update the product's price and description
                product.Price += 100.00m;
                product.Description = "A high-performance laptop with Core i7 Processor";

                // Save changes to the database asynchronously
                await context.SaveChangesAsync();

                Console.WriteLine("\nProduct with Name 'Laptop' Price and Description Updated.");
            }
            else
            {
                Console.WriteLine($"\nProduct with Name 'Laptop' not found.\n");
            }
        }

        // Asynchronous method for deleting a specific product by Id
        private static async Task DeleteProductAsync(EFCoreDbContext context)
        {
            // Find the product with ID 1 asynchronously
            var product = await context.Products.FindAsync(1);

            if (product != null)
            {
                // Remove the product from the context
                // No RemoveAsync Method available
                context.Products.Remove(product);

                // Save changes to the database asynchronously
                await context.SaveChangesAsync();

                Console.WriteLine("\nProduct with ID 1 deleted.");
            }
            else
            {
                Console.WriteLine($"\nProduct with ID 1 not found.\n");
            }
        }
    }
}