namespace EFCoreCodeFirstDemo.Entities
{
    public class OrderLog
    {
        public int OrderLogId { get; set; } // Primary Key
        public int OrderId { get; set; } // Foreign Key
        public DateTime LogDate { get; set; }
        public string Message { get; set; }
    }
}
