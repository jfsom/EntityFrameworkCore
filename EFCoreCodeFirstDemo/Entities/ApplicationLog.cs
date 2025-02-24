namespace EFCoreCodeFirstDemo.Entities
{
    public class ApplicationLog
    {
        public DateTime LoggedAt { get; set; } // Time of the log entry
        public string LogLevel { get; set; } // Log level (e.g., Information, Warning, Error)
        public string Message { get; set; } // Log message content
    }
}