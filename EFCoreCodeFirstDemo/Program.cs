using Microsoft.EntityFrameworkCore;
using EFCoreCodeFirstDemo.Entities;
namespace EFCoreCodeFirstDemo
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Create an instance DbContext
            using var context = new EFCoreDbContext();

            // Ensure the database and tables are deleted 
            context.Database.EnsureDeleted();

            // Ensure the database and tables are created based on the model
            context.Database.EnsureCreated();

            // Insert sample data using raw SQL since ApplicationLog is keyless
            context.Database.ExecuteSqlRaw(@"
                    INSERT INTO Logs (LoggedAt, LogLevel, Message) VALUES 
                    ('2024-04-01 10:00:00', 'INFO', 'Application started.'),
                    ('2024-04-01 10:05:00', 'WARN', 'Low disk space.'),
                    ('2024-04-01 10:10:00', 'ERROR', 'Unhandled exception occurred.')
                ");

            // Query and display the audit logs
            var logs = context.ApplicationLogs.ToListAsync().Result;

            Console.WriteLine("Application Logs:");
            foreach (var log in logs)
            {
                Console.WriteLine($"{log.LoggedAt} [{log.LogLevel}] {log.Message}");
            }

            // Example: Update a specific log entry using raw SQL
            context.Database.ExecuteSqlInterpolated($@"
                    UPDATE Logs 
                    SET Message = 'Disk space critically low.' 
                    WHERE LoggedAt = '2024-04-01 10:05:00'
                ");

            // Example: Delete a specific log entry using raw SQL
            context.Database.ExecuteSqlInterpolated($@"
                    DELETE FROM Logs 
                    WHERE LoggedAt = '2024-04-01 10:10:00'
                ");

            // Query again to see the updates
            logs = context.ApplicationLogs.ToListAsync().Result;

            Console.WriteLine("\nUpdated Application Logs:");
            foreach (var log in logs)
            {
                Console.WriteLine($"{log.LoggedAt} [{log.LogLevel}] {log.Message}");
            }
        }
    }
}