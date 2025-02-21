using System.ComponentModel.DataAnnotations.Schema;
namespace EFCoreCodeFirstDemo.Entities
{
    public class Teacher
    {
        public int TeacherId { get; set; }
        public string? Name { get; set; }

        [InverseProperty("OnlineTeacher")]
        public ICollection<Course>? OnlineCourses { get; set; }

        [InverseProperty("OfflineTeacher")]
        public ICollection<Course>? OfflineCourses { get; set; }
    }
}