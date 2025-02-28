using EFCoreCodeFirstDemo.Entities;
using Microsoft.EntityFrameworkCore;
namespace EFCoreCodeFirstDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                // Simulating CustomerId from authentication
                int customerId = 1; // Example CustomerId

                using var context = new EFCoreDbContext(customerId);

                // Retrieve Orders for the current tenant
                var customer = context.Customers
                            .Include(ord => ord.Orders)
                            .FirstOrDefault();

                if (customer != null)
                {
                    Console.WriteLine($"Customer ID: {customerId}, Name: {customer.Name}, Email: {customer.Email}");
                    Console.WriteLine($"Customer Orders");
                    foreach (var order in customer.Orders)
                    {
                        Console.WriteLine($"\tOrder ID: {order.OrderId}, Product Name: {order.ProductName}, Order Date: {order.OrderDate.ToShortDateString()}");
                    }
                }
                else
                {
                    Console.WriteLine($"Customer ID: {customerId} Not Found");
                }
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Database error: {ex.InnerException?.Message ?? ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while fetching cutsomer-specific orders: {ex.Message}");
            }
        }
    }
}