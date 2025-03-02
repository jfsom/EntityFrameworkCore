using System.ComponentModel.DataAnnotations;
namespace EFCoreCodeFirstDemo.Models
{
    public class Student
    {
        public int StudentId { get; set; }

        [ConcurrencyCheck]
        public int RegdNumber { get; set; }
        [ConcurrencyCheck]
        public string? Name { get; set; }
        public string? Branch { get; set; }
    }
}