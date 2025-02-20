using System.ComponentModel.DataAnnotations;
namespace EFCoreCodeFirstDemo.Entities
{
    public class Student
    {
        [Key]
        public int StudentRegdNo { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}