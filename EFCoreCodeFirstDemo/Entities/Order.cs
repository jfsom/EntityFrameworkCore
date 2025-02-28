namespace EFCoreCodeFirstDemo.Entities
{
    public class Order
    {
        public int OrderId { get; set; }          // Primary Key
        public string ProductName { get; set; }   // Name of the product
        public int Quantity { get; set; }         // Quantity ordered
        public bool IsDeleted { get; set; }       // Soft delete flag
        public DateTime OrderDate { get; set; }   // Date of the order
    }
}