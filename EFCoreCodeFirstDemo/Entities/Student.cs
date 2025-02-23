using System.ComponentModel.DataAnnotations;
namespace EFCoreCodeFirstDemo.Entities
{
    public class Student
    {
        public int StudentId { get; set; }
        [MaxLength(10)]
        [MinLength(5)]
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}