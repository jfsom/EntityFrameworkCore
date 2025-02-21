using Microsoft.EntityFrameworkCore;
namespace EFCoreCodeFirstDemo.Entities
{
    [Index(nameof(RegistrationNumber))] // Index on the RegistrationNumber column
    public class Student
    {
        public int StudentId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int RegistrationNumber { get; set; }
    }
}