namespace EFCoreCodeFirstDemo.Entities
{
    public class Student
    {
        public int StudentId { get; set; }  // This will create a NOT NULL column
        public string? Name { get; set; }   // This will create a NULL column
        public string? Address { get; set; } // This will create a NULL column
        public int RollNumber { get; set; }  // This will create a NOT NULL column
    }
}