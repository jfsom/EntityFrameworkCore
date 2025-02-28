using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreCodeFirstDemo.Entities
{
    // Derived Class representing a Utility Bill
    public class UtilityBill : Invoice
    {
        public string UtilityType { get; set; }   // Type of Utility (e.g., Electricity, Gas, Water)
        public string MeterNumber { get; set; }   // Meter Number for the utility
        [Column(TypeName = "decimal(18,2)")]
        public decimal UsageAmount { get; set; }  // Quantity of Utility Used (e.g., kWh, gallons), not the Amount

        [Column(TypeName = "decimal(18,2)")]
        public decimal RatePerUnit { get; set; } //Rate Per Unit
        public DateTime ServicePeriodStart { get; set; } // Start of Service Period
        public DateTime ServicePeriodEnd { get; set; }   // End of Service Period
        public string UtilityProvider { get; set; } // Name of the utility provider
        public DateTime DueDate { get; set; } // Due Date for the bill
    }
}