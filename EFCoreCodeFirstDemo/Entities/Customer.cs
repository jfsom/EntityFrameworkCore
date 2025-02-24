using EFCoreCodeFirstDemo.Entities;

namespace EFCorePropertyConfigurations.Entities
{
    public class Customer
    {
        public int Id { get; set; } // Will configure it as Identity
        public string FirstName { get; set; } // Will configure max length
        public string LastName { get; set; }
        public string Email { get; set; } // Will configure as required
        public string PhoneNumber { get; set; } // We will make it Optional
        public DateTime? LastLoginDate { get; set; } // Nullable property
        public DateTime CreatedDate { get; set; } // Will configure default value
        public ICollection<Order> Orders { get; set; }
    }
}