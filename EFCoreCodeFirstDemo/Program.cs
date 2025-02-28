using EFCoreCodeFirstDemo.Entities;
namespace EFCoreCodeFirstDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                using var context = new EFCoreDbContext();

                // Retrieve all active (non-deleted) orders
                var activeOrders = context.Orders.ToList();

                Console.WriteLine("\nActive Orders:");
                foreach (var order in activeOrders)
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