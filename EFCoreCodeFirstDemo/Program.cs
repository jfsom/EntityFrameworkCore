using System.Diagnostics;
using EFCoreCodeFirstDemo.Entities;

namespace EFCoreBulkDeleteBenchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            // Generate initial products and insert them into the database
            var initialProducts1 = GenerateProducts(2000);
            var initialProducts2 = GenerateProducts(2000);
            var initialProducts3 = GenerateProducts(2000);

            // Ensure the data is pre-inserted
            InsertWithStandardEFCore(initialProducts1);
            InsertWithStandardEFCore(initialProducts2);
            InsertWithStandardEFCore(initialProducts3);

            // Standard EF Core Delete
            var efCoreDeleteTime = DeleteWithStandardEFCore(initialProducts1);

            // Z.EntityFramework.Extensions.EFCore Delete
            var zEntityExtensionsDeleteTime = DeleteWithZEntityExtensions(initialProducts2);

            // EFCore.BulkExtensions Delete
            var bulkExtensionsDeleteTime = DeleteWithEFCoreBulkExtensions(initialProducts3);

            // Display the results
            Console.WriteLine("\nBulk Delete Performance Benchmark:");
            Console.WriteLine($"Standard EF Core Delete: {efCoreDeleteTime} ms");
            Console.WriteLine($"Z.EntityFramework.Extensions.EFCore Delete: {zEntityExtensionsDeleteTime} ms");
            Console.WriteLine($"EFCore.BulkExtensions Delete: {bulkExtensionsDeleteTime} ms");
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
                var stopwatch = Stopwatch.StartNew();

                context.Products.AddRange(products);
                context.SaveChanges();

                stopwatch.Stop();
                return stopwatch.ElapsedMilliseconds;
            }
        }

        // Deletes products using standard EF Core RemoveRange and SaveChanges.
        static long DeleteWithStandardEFCore(List<Product> products)
        {
            using (var context = new EFCoreDbContext())
            {
                var stopwatch = Stopwatch.StartNew();

                context.Products.RemoveRange(products); // Delete using standard EF Core
                context.SaveChanges();

                stopwatch.Stop();
                return stopwatch.ElapsedMilliseconds;
            }
        }

        // Deletes products using EFCore.BulkExtensions BulkDelete.
        static long DeleteWithEFCoreBulkExtensions(List<Product> products)
        {
            using (var context = new EFCoreDbContext())
            {
                var stopwatch = Stopwatch.StartNew();

                // Bulk delete using EFCore.BulkExtensions
                //EFCore.BulkExtensions.DbContextBulkExtensions.BulkDelete(context, products);

                stopwatch.Stop();
                return stopwatch.ElapsedMilliseconds;
            }
        }

        // Deletes products using Z.EntityFramework.Extensions.EFCore BulkDelete.
        static long DeleteWithZEntityExtensions(List<Product> products)
        {
            using (var context = new EFCoreDbContext())
            {
                var stopwatch = Stopwatch.StartNew();

                // Bulk delete using Z.EntityFramework.Extensions
                DbContextExtensions.BulkDelete(context, products);

                stopwatch.Stop();
                return stopwatch.ElapsedMilliseconds;
            }
        }
    }
}