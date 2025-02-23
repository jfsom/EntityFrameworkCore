using EFCoreCodeFirstDemo.Entities;
namespace EFCoreCodeFirstDemo
{
    public class Program
    {
        // Define the quantities each user is booking
        private static int userAQuantity = 3;
        private static int userBQuantity = 7;

        static void Main(string[] args)
        {
            try
            {
                //This Propetry will store the Initia Stock
                int InitialStock = 0;

                // Get initial stock from database before any booking
                using (var context = new EFCoreDbContext())
                {
                    var initialProduct = context.Products.Find(1);
                    InitialStock = initialProduct?.StockQuantity ?? 0;
                    Console.WriteLine($"Initial stock in database: {initialProduct?.StockQuantity}");
                }

                // Create two threads to simulate concurrent transactions
                Thread t1 = new Thread(Method1); // Thread for User A's transaction
                Thread t2 = new Thread(Method2); // Thread for User B's transaction

                Console.WriteLine("Booking Started by User A and User B");

                // Start both threads for User A and User B
                t1.Start();
                t2.Start();

                // Ensure both threads finish before proceeding
                t1.Join();
                t2.Join();

                // After the transactions get the final stock in the database
                using (var context = new EFCoreDbContext())
                {
                    var finalProduct = context.Products.Find(1); // Fetch product with Id 1
                    int expectedFinalStock = InitialStock - (userAQuantity + userBQuantity); // Calculate the expected final stock

                    Console.WriteLine($"Expected final stock (Initial stock - User A booking - User B booking): {expectedFinalStock}");
                    Console.WriteLine($"Final stock in database: {finalProduct?.StockQuantity}");

                    if (finalProduct != null && finalProduct.StockQuantity == expectedFinalStock)
                    {
                        Console.WriteLine("Final stock is correct.");
                    }
                    else
                    {
                        Console.WriteLine("Final stock is incorrect. There may be a concurrency issue.");
                    }
                }

                Console.ReadKey(); // Wait for user to press a key before exiting
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}"); // Catch and log any errors
            }
        }

        // User A | Transaction 1 | Booking 3 items
        public static void Method1()
        {
            using EFCoreDbContext context = new EFCoreDbContext(); // Create a new DB context

            // Fetch product with Id 1
            var product1 = context.Products.Find(1);

            // Simulate delay (2 seconds) to mimic real-world concurrency issues
            Thread.Sleep(TimeSpan.FromSeconds(2));

            if (product1 != null) // Ensure product exists
            {
                // Update stock quantity for User A's booking
                product1.StockQuantity -= userAQuantity;

                // Save changes to the database
                context.SaveChanges();

                // Log how many quantities User A booked and the new stock quantity
                Console.WriteLine($"User A booked {userAQuantity} items. Updated stock after User A: {product1.StockQuantity}");
            }
        }

        // User B | Transaction 2 | Booking 7 items
        public static void Method2()
        {
            using EFCoreDbContext context = new EFCoreDbContext(); // Create a new DB context

            // Fetch product with Id 1
            var product1 = context.Products.Find(1);

            // Simulate delay (5 seconds) to allow time for potential concurrency conflict
            Thread.Sleep(TimeSpan.FromSeconds(5));

            if (product1 != null) // Ensure product exists
            {
                // Update stock quantity for User B's booking
                product1.StockQuantity -= userBQuantity;

                // Save changes to the database
                context.SaveChanges();

                // Log how many quantities User B booked and the new stock quantity
                Console.WriteLine($"User B booked {userBQuantity} items. Updated stock after User B: {product1.StockQuantity}");
            }
        }
    }
}