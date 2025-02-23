namespace EFCoreCodeFirstDemo.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string CourseName { get; set; }
        public List<Student> Students { get; set; } // Navigation property for many-to-many
    }
}