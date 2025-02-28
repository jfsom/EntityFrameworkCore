using EFCoreCodeFirstDemo.Entities;
namespace EFCoreCodeFirstDemo
{
    public class Program
    {
        static void Main(string[] args)
        {
            using (var context = new EFCoreDbContext())
            {
                // Update an existing Utility Bill
                var utilityBill = context.UtilityBills.FirstOrDefault(b => b.InvoiceNumber == "UB001");
                if (utilityBill != null)
                {
                    utilityBill.Amount = 110.50m; // Update the total amount
                    utilityBill.DueDate = DateTime.Now.AddDays(10); // Update the due date
                    context.SaveChanges();
                    Console.WriteLine("Utility Bill updated.");
                }

                // Update an existing Product Purchase
                var productPurchase = context.ProductPurchases.FirstOrDefault(p => p.InvoiceNumber == "PP001");
                if (productPurchase != null)
                {
                    productPurchase.Quantity = 2; // Increase the quantity
                    productPurchase.Amount = productPurchase.Quantity * productPurchase.UnitPrice + productPurchase.ShippingCost;
                    context.SaveChanges();
                    Console.WriteLine("Product Purchase updated.");
                }

                // Update an existing Subscription Service
                var subscription = context.SubscriptionServices.FirstOrDefault(s => s.InvoiceNumber == "SS001");
                if (subscription != null)
                {
                    subscription.AutoRenew = false; // Disable auto-renewal
                    context.SaveChanges();
                    Console.WriteLine("Subscription Service updated.");
                }
            }
        }
    }
}