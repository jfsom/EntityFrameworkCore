using Microsoft.EntityFrameworkCore;
namespace EFCoreCodeFirstDemo.Entities
{
    [Index(nameof(FirstName), nameof(LastName), Name = "Index_FirstName_LastName")]
    [Index(nameof(RegistrationNumber), nameof(RollNumber), Name = "Index_RegistrationNumber_RollNumber")]
    public class Student
    {
        public int StudentId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int RegistrationNumber { get; set; }
        public int RollNumber { get; set; }
    }
}