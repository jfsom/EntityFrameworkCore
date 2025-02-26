namespace EFCoreCodeFirstDemo.Entities
{
    public class Customer
    {
        public int CustomerId { get; set; } // Primary Key
        public string Name { get; set; } // Customer name
        public string Email { get; set; } // Customer email

        // One-to-many relationship: A customer can have multiple orders
        public ICollection<Order> Orders { get; set; }
    }
}