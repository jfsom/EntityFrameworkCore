using System.Diagnostics;
using EFCoreCodeFirstDemo.Entities;

namespace EFCoreBulkInsertBenchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new EFCoreDbContext();

            // Generate the products
            var products = GenerateProducts(2000);

            // Standard EF Core Insert
            var efCoreTime = InsertWithStandardEFCore(products);

            // Z.EntityFramework.Extensions.EFCore Insert
            var zEntityExtensionsTime = InsertWithZEntityExtensions(products);

            // EFCore.BulkExtensions Insert
            var bulkExtensionsTime = InsertWithEFCoreBulkExtensions(products);

            // Display the results
            Console.WriteLine("\nBulk Insert Performance Benchmark:");
            Console.WriteLine($"Standard EF Core Insert: {efCoreTime} ms");
            Console.WriteLine($"Z.EntityFramework.Extensions.EFCore Insert: {zEntityExtensionsTime} ms");
            Console.WriteLine($"EFCore.BulkExtensions Insert: {bulkExtensionsTime} ms");
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
        static long InsertWithStandardEFCore(List<Product> products)
        {
            using (var context = new EFCoreDbContext())
            {
                // Clear existing data to ensure a fair benchmark
                context.Products.RemoveRange(context.Products);
                context.SaveChanges();

                var stopwatch = Stopwatch.StartNew();

                context.Products.AddRange(products);
                context.SaveChanges();

                stopwatch.Stop();
                return stopwatch.ElapsedMilliseconds;
            }
        }

        // Inserts products using EFCore.BulkExtensions BulkInsert.
        static long InsertWithEFCoreBulkExtensions(List<Product> products)
        {
            // Insert using EFCore.BulkExtensions
            using (var context = new EFCoreDbContext())
            {
                // Clear existing data
                context.Products.RemoveRange(context.Products);
                context.SaveChanges();

                var stopwatch = Stopwatch.StartNew();

                //EFCore.BulkExtensions.DbContextBulkExtensions.BulkInsert(context, products);
                //EFCore.BulkExtensions.DbContextBulkExtensions.BulkInsert(context, products);

                stopwatch.Stop();
                return stopwatch.ElapsedMilliseconds;
            }
        }

        // Inserts products using Z.EntityFramework.Extensions.EFCore BulkInsert.
        static long InsertWithZEntityExtensions(List<Product> products)
        {
            using (var context = new EFCoreDbContext())
            {
                // Clear existing data
                context.Products.RemoveRange(context.Products);
                context.SaveChanges();

                var stopwatch = Stopwatch.StartNew();

                DbContextExtensions.BulkInsert(context, products);

                stopwatch.Stop();
                return stopwatch.ElapsedMilliseconds;
            }
        }
    }
}