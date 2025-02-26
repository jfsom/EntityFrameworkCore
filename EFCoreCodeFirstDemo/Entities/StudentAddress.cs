using System.ComponentModel.DataAnnotations;
namespace EFCoreCodeFirstDemo.Entities
{
    public class StudentAddress
    {
        [Key]
        public int StudentId { get; set; }  // PK and FK
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public Student Student { get; set; }
    }
}