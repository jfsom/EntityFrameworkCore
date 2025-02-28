using EFCoreCodeFirstDemo.Entities;
namespace EFCoreCodeFirstDemo
{
    public class Program
    {
        static void Main(string[] args)
        {
            using (var context = new EFCoreDbContext())
            {
                // Create and seed a Utility Bill
                var utilityBill = new UtilityBill
                {
                    InvoiceNumber = "UB001",
                    BillingDate = DateTime.Now,
                    CustomerName = "Ravi Kumar",
                    CustomerEmail = "ravi.kumar@example.com",
                    BillingAddress = "123 Elm Street",
                    Status = InvoiceStatus.Pending,
                    UtilityType = "Electricity",
                    MeterNumber = "MTR12345",
                    UsageAmount = 250.75m,
                    RatePerUnit = 0.40m, // 0.40 per unit of electricity
                    ServicePeriodStart = DateTime.Now.AddMonths(-1),
                    ServicePeriodEnd = DateTime.Now,
                    UtilityProvider = "ElectricCo",
                    DueDate = DateTime.Now.AddDays(15)
                };
                // Calculate the Amount for the utility bill
                utilityBill.Amount = utilityBill.UsageAmount * utilityBill.RatePerUnit;

                // Create and seed a Product Purchase
                var productPurchase = new ProductPurchase
                {
                    InvoiceNumber = "PP001",
                    BillingDate = DateTime.Now,
                    CustomerName = "Alice Johnson",
                    CustomerEmail = "alice.johnson@example.com",
                    BillingAddress = "456 Oak Avenue",
                    Status = InvoiceStatus.Paid,
                    ProductName = "Laptop",
                    Quantity = 1,
                    UnitPrice = 1500.00m,
                    Vendor = "TechStore",
                    ShippingCost = 25.00m,
                    TrackingNumber = "TRACK123456789"
                };
                // Calculate the Amount for the product purchase
                productPurchase.Amount = (productPurchase.Quantity * productPurchase.UnitPrice) + productPurchase.ShippingCost;


                // Create and seed a Subscription Service
                var subscriptionService = new SubscriptionService
                {
                    InvoiceNumber = "SS001",
                    Amount = 99.99m, //Amount equals to SubscriptionFee
                    BillingDate = DateTime.Now,
                    CustomerName = "Michael Scott",
                    CustomerEmail = "michael.scott@example.com",
                    BillingAddress = "789 Maple Street",
                    Status = InvoiceStatus.Pending,
                    ServiceName = "Netflix Subscription",
                    SubscriptionStart = DateTime.Now,
                    SubscriptionEnd = DateTime.Now.AddYears(1),
                    SubscriptionFee = 99.99m,
                    RenewalFrequency = "Annually",
                    AutoRenew = true
                };

                // Add all invoices to the context
                context.UtilityBills.Add(utilityBill);
                context.ProductPurchases.Add(productPurchase);
                context.SubscriptionServices.Add(subscriptionService);

                // Save changes to the database
                int recordsAdded = context.SaveChanges();

                // Output the result
                Console.WriteLine($"{recordsAdded} records were saved to the database.");
            }
        }
    }
}