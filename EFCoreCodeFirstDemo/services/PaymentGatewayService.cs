using EFCoreCodeFirstDemo.Entities;

namespace EFCoreCodeFirstDemo.Services
{
    public class PaymentGatewayService
    {
        private readonly Random _random = new Random();
        private readonly List<string> _statuses = new List<string> { "Pending", "Completed", "Failed", "Cancelled" };

        // Simulates a network call to fetch payment status.
        public async Task<string> GetUpdatedPaymentStatusAsync(Payment payment)
        {
            try
            {
                // Randomly simulate network issues (e.g., gateway down).
                if (_random.Next(1, 10) > 8)
                {
                    throw new Exception("Payment gateway is temporarily unavailable.");
                }

                // Simulate network delay.
                await Task.Delay(200);

                // If the current status is "Pending", assign a new status.
                return payment.Status == "Pending" ? _statuses[_random.Next(_statuses.Count)] : payment.Status;
            }
            catch (Exception ex)
            {
                // Handle gateway error by throwing an exception with a specific message.
                throw new Exception($"Error accessing payment gateway for Payment ID {payment.PaymentId}: {ex.Message}");
            }
        }
    }
}