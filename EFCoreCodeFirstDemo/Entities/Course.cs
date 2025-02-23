namespace EFCoreCodeFirstDemo.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string CourseName { get; set; }
        public List<StudentCourse> StudentCourses { get; set; } // Navigation property to join entity
    }
}