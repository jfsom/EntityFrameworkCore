using EFCoreCodeFirstDemo.Entities;
namespace EFCoreCodeFirstDemo
{
    public class Program
    {
        static void Main(string[] args)
        {
            using (var context = new EFCoreDbContext())
            {
                // Retrieve and display all Utility Bills
                var utilityBills = context.UtilityBills.ToList();
                Console.WriteLine("\n--- List of Utility Bills ---");
                foreach (var bill in utilityBills)
                {
                    Console.WriteLine($"Invoice: {bill.InvoiceNumber}, Amount: {bill.Amount}, Utility: {bill.UtilityType}, Usage: {bill.UsageAmount} units, Rate: {bill.RatePerUnit}, Provider: {bill.UtilityProvider}");
                }

                // Retrieve and display all Product Purchases
                var productPurchases = context.ProductPurchases.ToList();
                Console.WriteLine("\n--- List of Product Purchases ---");
                foreach (var purchase in productPurchases)
                {
                    Console.WriteLine($"Invoice: {purchase.InvoiceNumber}, Product: {purchase.ProductName}, Quantity: {purchase.Quantity}, Vendor: {purchase.Vendor}, Shipping Cost: {purchase.ShippingCost}");
                }

                // Retrieve and display all Subscription Services
                var subscriptions = context.SubscriptionServices.ToList();
                Console.WriteLine("\n--- List of Subscription Services ---");
                foreach (var subscription in subscriptions)
                {
                    Console.WriteLine($"Invoice: {subscription.InvoiceNumber}, Service: {subscription.ServiceName}, Fee: {subscription.SubscriptionFee}, Auto-Renew: {subscription.AutoRenew}");
                }
            }
        }
    }
}