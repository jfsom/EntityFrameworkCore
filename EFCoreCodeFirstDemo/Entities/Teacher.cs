namespace EFCoreCodeFirstDemo.Entities
{
    public class Teacher
    {
        public int TeacherId { get; set; }
        public string? Name { get; set; }
        public ICollection<Course>? OnlineCourses { get; set; }
    }
}