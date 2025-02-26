﻿namespace EFCoreCodeFirstDemo.Entities
{
    public class Job
    {
        public int JobId { get; set; } // Primary Key
        public DateTime StartTime { get; set; } // Job start time
        public DateTime? EndTime { get; set; } // Job end time
        public string Status { get; set; } // Job status: Started, Completed, Failed, Partially Completed
        public int TotalPayments { get; set; } // Total number of payments in the job
        public int SuccessfulPayments { get; set; } // Number of successful payments
        public int FailedPayments { get; set; } // Number of failed payments
        public int BatchSize { get; set; } // Number of payments per batch
        public int TotalBatches { get; set; } // Total number of batches for this job

        // Navigation property: A job can have many job details
        public ICollection<JobDetail> JobDetails { get; set; }
    }
}
