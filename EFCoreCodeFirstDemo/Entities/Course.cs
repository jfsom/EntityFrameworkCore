using System.ComponentModel.DataAnnotations.Schema;
namespace EFCoreCodeFirstDemo.Entities
{
    public class Course
    {
        public int CourseId { get; set; }
        public string? CourseName { get; set; }
        public string? Description { get; set; }
        public int? OnlineTeacherId { get; set; }

        [InverseProperty("OnlineCourses")]
        public Teacher? OnlineTeacher { get; set; }
        public int? OfflineTeacherId { get; set; }

        [InverseProperty("OfflineCourses")]
        public Teacher? OfflineTeacher { get; set; }
    }
}