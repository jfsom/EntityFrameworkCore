namespace EFCoreCodeFirstDemo.Entities
{
    public class JobDetail
    {
        public int JobDetailId { get; set; } // Primary Key
        public int JobId { get; set; } // Foreign Key to Job
        public int PaymentId { get; set; } // Foreign Key to Payment
        public string PreviousStatus { get; set; } // Previous payment status
        public string NewStatus { get; set; } // New payment status after update
        public bool IsSuccess { get; set; } // Indicates whether the update was successful
        public Job Job { get; set; } // Navigation property to Job
        public Payment Payment { get; set; } // Navigation property to Payment
    }
}