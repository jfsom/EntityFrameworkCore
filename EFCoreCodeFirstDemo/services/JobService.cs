using EFCoreCodeFirstDemo.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCoreCodeFirstDemo.Services
{
    public class JobService
    {
        // Creates a new job and stores it in the database.
        public async Task<Job> CreateNewJobAsync()
        {
            using var context = new EFCoreDbContext();

            var job = new Job
            {
                StartTime = DateTime.Now,
                Status = "Started",
                SuccessfulPayments = 0, // Initializing
                FailedPayments = 0,     // Initializing
                TotalPayments = 0,
                BatchSize = 0,
                TotalBatches = 0
            };

            context.Jobs.Add(job);
            await context.SaveChangesAsync();

            // Log job start
            Logger.Log($"Job {job.JobId} started at {job.StartTime}.");

            return job;
        }

        // Marks the job as completed and updates successful/failed payment counts.
        public async Task CompleteJobAsync(Job job)
        {
            using var context = new EFCoreDbContext();

            job.EndTime = DateTime.Now;

            // Determine the job status based on the number of failed payments
            if (job.FailedPayments > 0 && job.SuccessfulPayments > 0)
            {
                job.Status = "Partially Completed"; // Some payments failed
            }
            else if (job.FailedPayments == 0)
            {
                job.Status = "Completed"; // All payments succeeded
            }
            else
            {
                job.Status = "Failed"; // All payments failed
            }

            context.Entry(job).State = EntityState.Modified;
            await context.SaveChangesAsync();

            // Log job completion
            Logger.Log($"Job {job.JobId} completed at {job.EndTime}. Successful payments: {job.SuccessfulPayments}, Failed payments: {job.FailedPayments}.");
        }

        // Logs details of the payments processed by a job.
        public async Task LogJobDetailsAsync(Job job, Payment payment, string previousStatus, bool isSuccess)
        {
            using var context = new EFCoreDbContext();

            var jobDetail = new JobDetail
            {
                JobId = job.JobId,
                PaymentId = payment.PaymentId,
                PreviousStatus = previousStatus,
                NewStatus = payment.Status,
                IsSuccess = isSuccess
            };

            context.JobDetails.Add(jobDetail);

            // Update job success/failure counters
            if (isSuccess)
            {
                job.SuccessfulPayments++;
            }
            else
            {
                job.FailedPayments++;
            }

            await context.SaveChangesAsync();

            // Log job detail
            Logger.Log($"Payment {payment.PaymentId}: Status updated from {previousStatus} to {payment.Status}. Success: {isSuccess}.");
        }
    }
}