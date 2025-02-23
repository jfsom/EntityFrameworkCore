using EFCoreCodeFirstDemo.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCoreCodeFirstDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Step 1: Initialize the DbContext
                using var context = new EFCoreDbContext();

                // Step 2: Retrieve an order to delete (in this case, we're deleting the order with ID 1)
                var orderToDelete = context.Orders
                    .Include(ord => ord.OrderItems) //Eager Load the Related Order Items
                    .FirstOrDefault(o => o.Id == 1); // Retrieve order with ID = 1

                if (orderToDelete != null)
                {
                    // Output the order details before deletion
                    Console.WriteLine($"Order to be deleted: {orderToDelete.OrderNumber}");

                    // Output the related order items before deletion
                    foreach (var item in orderToDelete.OrderItems)
                    {
                        Console.WriteLine($"\tOrderItemId: {item.Id}, Product Name: {item.ProductName}");
                    }

                    // Step 3: Delete the order
                    context.Orders.Remove(orderToDelete); // Remove the order (this will trigger cascade delete for related order items)

                    // Step 4: Save the changes to the database
                    context.SaveChanges(); // This will delete the order and its related order items (due to cascade delete)

                    // Output the success message
                    Console.WriteLine($"Order '{orderToDelete.OrderNumber}' and its related items were successfully deleted.");
                }
                else
                {
                    // Output if the order with the specified ID was not found
                    Console.WriteLine("Order not found. No deletion performed.");
                }

                // Step 5: Verify that the order and related order items are deleted by attempting to retrieve them again
                var deletedOrder = context.Orders.FirstOrDefault(o => o.Id == 1); // Trying to find the deleted order
                if (deletedOrder == null)
                {
                    Console.WriteLine("Order with ID 1 has been deleted from the database.");
                }

                // Verify the related OrderItems are also deleted
                var deletedOrderItems = context.OrderItems.Where(oi => oi.OrderId == 1).ToList();
                if (deletedOrderItems.Count == 0)
                {
                    Console.WriteLine("All related OrderItems for Order ID 1 have been deleted.");
                }
                else
                {
                    Console.WriteLine("Some OrderItems for Order ID 1 are still present, which should not happen with Cascade delete.");
                }
            }
            catch (Exception ex)
            {
                // Handle any errors that occur during the operation
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}