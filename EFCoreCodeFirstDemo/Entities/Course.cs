namespace EFCoreCodeFirstDemo.Entities
{
    public class Course
    {
        public int CourseId { get; set; }  // PK (INT, Identity)
        public string Title { get; set; }  // NVARCHAR(MAX), NOT NULL
        public double Credits { get; set; }  // FLOAT, NOT NULL
        public int TeacherId { get; set; }  // FK to Teacher (INT, NOT NULL)
        public virtual Teacher Teacher { get; set; }  // Navigation property
        public virtual ICollection<Student> Students { get; set; }  // Many-to-Many relationship
    }
}