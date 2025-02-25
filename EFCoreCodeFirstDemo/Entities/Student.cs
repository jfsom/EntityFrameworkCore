namespace EFCoreCodeFirstDemo.Entities
{
    public class Student
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; } = string.Empty; // Ensure non-null values
        public string LastName { get; set; } = string.Empty;
        public string Branch { get; set; } = string.Empty;
    }
}