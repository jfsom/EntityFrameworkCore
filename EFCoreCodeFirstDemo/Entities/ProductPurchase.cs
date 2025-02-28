using System.ComponentModel.DataAnnotations.Schema;
namespace EFCoreCodeFirstDemo.Entities
{
    // Derived Class representing a Product Purchase
    public class ProductPurchase : Invoice
    {
        public string ProductName { get; set; }  // Name of the Product
        public int Quantity { get; set; }        // Quantity Purchased
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }   // Price per Unit
        public string Vendor { get; set; }   // Vendor from whom the product was purchased
        [Column(TypeName = "decimal(18,2)")]
        public decimal ShippingCost { get; set; } // Shipping cost for the product
        public string TrackingNumber { get; set; } // Shipment tracking number
    }
}
