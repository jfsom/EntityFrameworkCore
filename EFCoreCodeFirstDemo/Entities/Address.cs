namespace EFCoreCodeFirstDemo.Entities
{
    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public int AddressId { get; set; }
        public string? State { get; set; }
        public string Country { get; set; }
        public int? StudentId { get; set; }
        public virtual Student Student { get; set; }
        public int? TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
}