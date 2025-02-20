using Microsoft.EntityFrameworkCore;
namespace EFCoreCodeFirstDemo.Entities
{
    [PrimaryKey(nameof(RegdNo), nameof(SerialNo))]
    public class Student
    {
        public int RegdNo { get; set; }
        public int SerialNo { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}