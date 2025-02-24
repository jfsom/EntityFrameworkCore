using Microsoft.EntityFrameworkCore;
namespace EFCoreCodeFirstDemo.Entities
{
    [Owned]
    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
    }
}