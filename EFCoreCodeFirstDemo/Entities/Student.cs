using System.Reflection;

namespace EFCoreCodeFirstDemo.Entities
{
    public class Student
    {
        public int StudentId { get; set; }  // SQL Type: INT (NOT NULL, Primary Key)
        public string FirstName { get; set; }  // SQL Type: NVARCHAR(MAX) (NOT NULL)
        public string? LastName { get; set; }  // SQL Type: NVARCHAR(MAX) (NULL)
        public DateTime? DateOfBirth { get; set; }  // SQL Type: DATETIME2 (NULL)
        public decimal GPA { get; set; }  // SQL Type: DECIMAL(18, 2) (NOT NULL)
        public bool IsActive { get; set; }  // SQL Type: BIT (NOT NULL)
        public byte[] ProfilePicture { get; set; }  // SQL Type: VARBINARY(MAX) (NOT NULL)
        public virtual Gender Gender { get; set; }  // SQL Type: INT (NOT NULL, because enums are stored as INT)
        public virtual Address Address { get; set; }  // SQL Type: This would create a foreign key with default INT (if Address is required, it would be NOT NULL)
        public virtual ICollection<Course> Courses { get; set; }  // SQL Type: This would create a join table for many-to-many relationships
    }
}