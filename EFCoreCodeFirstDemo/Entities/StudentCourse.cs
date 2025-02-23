namespace EFCoreCodeFirstDemo.Entities
{
    public class StudentCourse
    {
        public int StudentId { get; set; }      // Foreign Key to Student
        public Student Student { get; set; }    // Navigation property
        public int CourseId { get; set; }       // Foreign Key to Course
        public Course Course { get; set; }      // Navigation property
        public DateTime EnrollmentDate { get; set; } // Additional Property
    }
}