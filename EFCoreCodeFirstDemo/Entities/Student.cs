using System.ComponentModel.DataAnnotations;
namespace EFCoreCodeFirstDemo.Entities
{
    public class Student
    {
        public int StudentId { get; set; }
        [MaxLength(50)]
        public string? FirstName { get; set; }
        [MinLength(5)]
        public string? LastName { get; set; }
    }
}