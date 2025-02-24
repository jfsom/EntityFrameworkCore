namespace EFCoreCodeFirstDemo.Entities
{
    public class Customer
    {
        public int CustomerId { get; set; } // Primary Key
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; } // Alternate Key (Unique)
        public Address Address { get; set; } // Owned Entity
        public ICollection<Order> Orders { get; set; }
    }
}