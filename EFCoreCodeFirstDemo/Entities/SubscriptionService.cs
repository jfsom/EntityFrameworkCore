using System.ComponentModel.DataAnnotations.Schema;
namespace EFCoreCodeFirstDemo.Entities
{
    // Derived Class representing a Subscription Service
    public class SubscriptionService : Invoice
    {
        public string ServiceName { get; set; }        // Name of the Subscription Service
        public DateTime SubscriptionStart { get; set; } // Subscription Start Date
        public DateTime SubscriptionEnd { get; set; }   // Subscription End Date
        [Column(TypeName = "decimal(18,2)")]
        public decimal SubscriptionFee { get; set; } //Subscription Fee  
        public string RenewalFrequency { get; set; } // Renewal Frequency (e.g., Monthly, Annually)
        public bool AutoRenew { get; set; }   // Indicates if the subscription auto-renews        
    }
}