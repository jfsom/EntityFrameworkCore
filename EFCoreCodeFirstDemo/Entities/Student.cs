namespace EFCoreCodeFirstDemo.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Course> Courses { get; set; } // Navigation property for many-to-many
    }
}