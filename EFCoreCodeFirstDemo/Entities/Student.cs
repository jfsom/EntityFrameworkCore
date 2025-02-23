using System.ComponentModel.DataAnnotations;

namespace EFCoreCodeFirstDemo.Entities
{
    public class Student
    {
        public int StudentId { get; set; }  // This will create a NOT NULL column

        // Allows empty strings, but disallows NULL
        [Required(AllowEmptyStrings = false)]
        public string? Name { get; set; }   // Name is now NOT NULL due to Required Attribute
        public string? Address { get; set; } // This will create a NULL column
        public int RollNumber { get; set; }  // This will create a NOT NULL column
    }
}