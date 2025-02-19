namespace EFCoreCodeFirstDemo.Entities
{
    public class Address
    {
        public int AddressId { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public int? StudentId { get; set; }
        public Student Student { get; set; }
        public int? TeacherId { get; set; }
        public Teacher Teacher { get; set; }
    }
}