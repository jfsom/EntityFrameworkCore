using Microsoft.EntityFrameworkCore;
namespace EFCoreCodeFirstDemo.Entities
{
    public class Student
    {
        public string StudentId { get; set; }
        public string SerialNo { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}