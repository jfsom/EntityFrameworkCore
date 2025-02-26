using System.Diagnostics;
using EFCoreCodeFirstDemo.Entities;

namespace EFCoreBulkUpdateBenchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            // Standard EF Core Update
            var efCoreUpdateTime = UpdateWithStandardEFCore();

            // Z.EntityFramework.Extensions.EFCore Update
            var zEntityExtensionsUpdateTime = UpdateWithZEntityExtensions();

            // EFCore.BulkExtensions Update
            var bulkExtensionsUpdateTime = UpdateWithEFCoreBulkExtensions();

            // Display the results
            Console.WriteLine("\nBulk Update Performance Benchmark:");
            Console.WriteLine($"Standard EF Core Update: {efCoreUpdateTime} ms");
            Console.WriteLine($"Z.EntityFramework.Extensions.EFCore Update: {zEntityExtensionsUpdateTime} ms");
            Console.WriteLine($"EFCore.BulkExtensions Update: {bulkExtensionsUpdateTime} ms");
        }

        // Generates a list of Product instances.
        static List<Product> GenerateProducts(int count)
        {
            var products = new List<Product>();
            for (int i = 0; i < count; i++)
            {
                products.Add(new Product
                {
                    Name = $"Product_{i}",
                    Price = (decimal)(new Random().NextDouble() * 100),
                    Quantity = 100,
                    Description = $"Product_{i} Description",
                    CreatedDate = DateTime.Now,
                    IsAvailable = true,
                    ModifiedBy = "Admin"
                });
            }
            return products;
        }

        // Inserts products using standard EF Core AddRange and SaveChanges.
        static void InsertWithStandardEFCore(List<Product> products)
        {
            using (var context = new EFCoreDbContext())
            {
                //Then add new data
                context.Products.AddRange(products);
                context.SaveChanges();
            }
        }

        // Modify the products for updating
        static List<Product> ModifyProductsForUpdate(List<Product> products)
        {
            foreach (var product in products)
            {
                product.Price += 10; // Increase price by 10
                product.Quantity += 1;
                product.Description = "Changed";
                product.ModifiedBy = "System";
                product.IsAvailable = !product.IsAvailable; // Flip availability
            }
            return products;
        }

        // Updates products using standard EF Core Update and SaveChanges.
        static long UpdateWithStandardEFCore()
        {
            using (var context = new EFCoreDbContext())
            {
                // Clear existing data
                context.Products.RemoveRange(context.Products);
                context.SaveChanges();

                // Generate initial products and insert them into the database
                var initialProducts = GenerateProducts(2000);

                // Ensure the data is pre-inserted
                InsertWithStandardEFCore(initialProducts);

                // Modify the products to update them
                var updatedProducts = ModifyProductsForUpdate(initialProducts);

                var stopwatch = Stopwatch.StartNew();

                context.Products.UpdateRange(updatedProducts);
                context.SaveChanges();

                stopwatch.Stop();
                return stopwatch.ElapsedMilliseconds;
            }
        }

        // Updates products using EFCore.BulkExtensions BulkUpdate.
        static long UpdateWithEFCoreBulkExtensions()
        {
            using (var context = new EFCoreDbContext())
            {
                // Clear existing data
                context.Products.RemoveRange(context.Products);
                context.SaveChanges();

                // Generate initial products and insert them into the database
                var initialProducts = GenerateProducts(2000);

                // Ensure the data is pre-inserted
                InsertWithStandardEFCore(initialProducts);

                // Modify the products to update them
                var updatedProducts = ModifyProductsForUpdate(initialProducts);

                var stopwatch = Stopwatch.StartNew();

                // Using EFCore.BulkExtensions for bulk update
                //EFCore.BulkExtensions.DbContextBulkExtensions.BulkUpdate(context, updatedProducts);

                stopwatch.Stop();
                return stopwatch.ElapsedMilliseconds;
            }
        }

        // Updates products using Z.EntityFramework.Extensions.EFCore BulkUpdate.
        static long UpdateWithZEntityExtensions()
        {
            using (var context = new EFCoreDbContext())
            {
                // Clear existing data
                context.Products.RemoveRange(context.Products);
                context.SaveChanges();

                // Generate initial products and insert them into the database
                var initialProducts = GenerateProducts(2000);

                // Ensure the data is pre-inserted
                InsertWithStandardEFCore(initialProducts);

                // Modify the products to update them
                var updatedProducts = ModifyProductsForUpdate(initialProducts);

                var stopwatch = Stopwatch.StartNew();

                // Using Z.EntityFramework.Extensions for bulk update
                DbContextExtensions.BulkUpdate(context, updatedProducts);
                stopwatch.Stop();
                return stopwatch.ElapsedMilliseconds;
            }
        }
    }
}