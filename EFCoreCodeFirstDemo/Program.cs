using EFCoreCodeFirstDemo.Entities;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace EFCoreCodeFirstDemo
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Enable implicit distributed transactions in case operations span multiple databases
            TransactionManager.ImplicitDistributedTransactions = true;

            // Define transaction options: Isolation level (ReadCommitted) and timeout duration (default 1 minute)
            var transactionOptions = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted, // Ensures data read is committed, avoiding dirty reads
                Timeout = TransactionManager.DefaultTimeout // Default transaction timeout
            };

            // Start a TransactionScope to encompass operations across different DbContexts
            using (var scope = new TransactionScope(
                TransactionScopeOption.Required, // Requires a new transaction or joins an existing one
                transactionOptions,
                TransactionScopeAsyncFlowOption.Enabled)) // Enables async operations within the transaction
            {
                try
                {
                    int generatedOrderId; // Variable to hold the generated OrderId for logging purposes

                    // 1. Perform operations using ECommerceDbContext to handle order-related activities
                    using (var orderContext = new ECommerceDbContext())
                    {
                        // Create a new order with associated OrderItems and Payment details
                        var order = new Order
                        {
                            OrderDate = DateTime.UtcNow, // Set the order date to the current UTC time
                            TotalAmount = 300.00m, // Set the total amount for the order

                            // Create a list of order items for the order
                            OrderItems = new List<OrderItem>
                            {
                                new OrderItem
                                {
                                    ProductId = 2, // Refers to the Product with ID 2
                                    Quantity = 3, // Quantity of the product ordered
                                    Price = 100.00m // Price per unit
                                }
                            },

                            // Create a payment entry for the order
                            Payment = new Payment
                            {
                                PaymentDate = DateTime.UtcNow, // Set the payment date to the current UTC time
                                Amount = 300.00m, // Payment amount matches the total order amount
                                Status = PaymentStatus.Pending // Initial status of the payment is 'Pending'
                            }
                        };

                        // Add the newly created order to the Orders DbSet
                        orderContext.Orders.Add(order);
                        Console.WriteLine("Order and associated details added to the DbContext...");

                        // Save changes to the database to insert the order, order items, and payment into the database
                        orderContext.SaveChanges();
                        Console.WriteLine("Order saved to the database.");

                        // Store the generated OrderId for logging purposes
                        generatedOrderId = order.OrderId;
                        Console.WriteLine($"Order ID {order.OrderId} created with initial payment status '{order.Payment.Status}'.");
                    }

                    // 2. Perform logging operations using LoggingDbContext
                    using (var logContext = new LoggingDbContext())
                    {
                        // Create a new OrderLog entry to record the successful placement of the order
                        var log = new OrderLog
                        {
                            OrderId = generatedOrderId, // Use the generated OrderId for the log entry
                            LogDate = DateTime.UtcNow, // Set the log date to the current UTC time
                            Message = "Order placed successfully." // Descriptive log message
                        };

                        // Add the log entry to the OrderLogs DbSet
                        logContext.OrderLogs.Add(log);
                        Console.WriteLine("Log entry for the order created...");

                        // Save the log entry to the logging database
                        logContext.SaveChanges();
                        Console.WriteLine($"Log entry saved for Order ID {log.OrderId}.");
                    }

                    // 3. Mark the transaction as complete
                    scope.Complete();
                    Console.WriteLine("Transaction successfully completed. Order placed and logged.");
                }
                catch (DbUpdateException ex)
                {
                    // Handle database-related exceptions such as constraint violations or connection issues
                    Console.WriteLine($"Database error occurred: {ex.InnerException?.Message ?? ex.Message}");

                    // No explicit rollback is needed here. Disposing the TransactionScope without calling Complete() will automatically roll back the transaction.
                }
                catch (Exception ex)
                {
                    // Handle any other general exceptions that may occur
                    Console.WriteLine($"An error occurred: {ex.Message}");

                    // The transaction will be rolled back automatically upon disposal if Complete() is not called.
                }

                // At the end of the using block, the TransactionScope is disposed. If Complete() was not called, the transaction is rolled back automatically.
            }
        }
    }
}