using EFCoreCodeFirstDemo.Entities;
using EFCoreCodeFirstDemo.Services;
using Microsoft.EntityFrameworkCore;

namespace EFCoreCodeFirstDemo
{
    public class Program
    {
        private static readonly Random _random = new Random();
        static async Task Main(string[] args)
        {
            try
            {
                // Log application start
                Logger.Log("Job Scheduler Application Started.");

                // Seed the database with sample data if necessary
                await SeedDatabaseAsync();

                // Define batch size (number of payments to process per batch)
                int batchSize = 20;

                // Initialize services
                var jobService = new JobService();
                var paymentService = new PaymentService();

                // Create a new job
                var job = await jobService.CreateNewJobAsync();

                // Start processing pending payments, passing the Job object and batch size to PaymentService
                await paymentService.ProcessPendingPaymentsAsync(job, batchSize);

                Logger.Log("Payment processing operations completed successfully.");
            }
            catch (DbUpdateException ex)
            {
                Logger.Log($"Database Error Occurred: {ex.InnerException?.Message ?? ex.Message}");
            }
            catch (Exception ex)
            {
                Logger.Log($"An unexpected error occurred: {ex.Message}");
            }
            finally
            {
                Logger.Log("Job Scheduler Application Ended.");
            }
        }

        static async Task SeedDatabaseAsync()
        {
            using var context = new EFCoreDbContext();

            if (await context.Customers.AnyAsync())
            {
                Logger.Log("Database already contains data. Skipping seeding.");
                return;
            }

            Logger.Log("Seeding database with sample data...");

            var customers = new List<Customer>();
            for (int i = 1; i <= 20; i++)
            {
                customers.Add(new Customer
                {
                    Name = $"Customer_{i}",
                    Email = $"customer{i}@example.com"
                });
            }

            context.Customers.AddRange(customers);
            await context.SaveChangesAsync();

            var orders = new List<Order>();
            for (int i = 1; i <= 100; i++)
            {
                var order = new Order
                {
                    OrderDate = DateTime.Now.AddDays(-_random.Next(0, 30)),
                    Status = "Pending",
                    CustomerId = customers[_random.Next(customers.Count)].CustomerId,
                    Payment = new Payment
                    {
                        Amount = Math.Round((decimal)(_random.NextDouble() * 1000), 2),
                        Currency = "USD",
                        Status = "Pending",
                        TransactionId = Guid.NewGuid().ToString()
                    }
                };

                orders.Add(order);
            }

            context.Orders.AddRange(orders);
            await context.SaveChangesAsync();

            Logger.Log("Seeding completed.");
        }
    }
}