using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreCodeFirstDemo.Entities
{
    [Table("StudentInfo", Schema = "Admin")]  // Mapping the entity to the StudentInfo table in Admin schema
    public class Student
    {
        public int StudentId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}