namespace EFCoreCodeFirstDemo.Entities
{
    public class Course
    {
        public int CourseId { get; set; }
        public string? CourseName { get; set; }
        public string? Description { get; set; }
        public int? OnlineTeacherId { get; set; }
        public Teacher? OnlineTeacher { get; set; }
        public int? OfflineTeacherId { get; set; }
        public Teacher? OfflineTeacher { get; set; }
    }
}