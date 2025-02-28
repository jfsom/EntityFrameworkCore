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
                using var context = new EFCoreDbContext();

                // Retrieve all orders, including deleted ones
                var allOrders = context.Orders.IgnoreQueryFilters().ToList();

                Console.WriteLine("\nAll Orders (Including Deleted):");
                foreach (var order in allOrders)
                {
                    Console.WriteLine($"\tOrder ID: {order.OrderId}, Product: {order.ProductName}, Quantity: {order.Quantity}, Order Date: {order.OrderDate}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while fetching active orders: {ex.Message}");
            }
        }
    }
}