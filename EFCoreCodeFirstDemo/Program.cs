using EFCoreCodeFirstDemo.Entities;
namespace EFCoreCodeFirstDemo
{
    public class Program
    {
        static void Main(string[] args)
        {
            using (var context = new EFCoreDbContext())
            {
                // Delete a Utility Bill
                var utilityBill = context.UtilityBills.FirstOrDefault(b => b.InvoiceNumber == "UB001");
                if (utilityBill != null)
                {
                    context.UtilityBills.Remove(utilityBill);
                    context.SaveChanges();
                    Console.WriteLine("Utility Bill deleted.");
                }

                // Delete a Product Purchase
                var productPurchase = context.ProductPurchases.FirstOrDefault(p => p.InvoiceNumber == "PP001");
                if (productPurchase != null)
                {
                    context.ProductPurchases.Remove(productPurchase);
                    context.SaveChanges();
                    Console.WriteLine("Product Purchase deleted.");
                }

                // Delete a Subscription Service
                var subscription = context.SubscriptionServices.FirstOrDefault(s => s.InvoiceNumber == "SS001");
                if (subscription != null)
                {
                    context.SubscriptionServices.Remove(subscription);
                    context.SaveChanges();
                    Console.WriteLine("Subscription Service deleted.");
                }
            }
        }
    }
}