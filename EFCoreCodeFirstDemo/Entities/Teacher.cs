namespace EFCoreCodeFirstDemo.Entities
{
    public class Teacher
    {
        public int TeacherId { get; set; }  // PK (INT, Identity)
        public string FullName { get; set; }  // NVARCHAR(MAX), NOT NULL
        public DateTime HireDate { get; set; }  // DateTime, NOT NULL
        public TimeSpan WorkHours { get; set; }  // TIME, NOT NULL
        public decimal Salary { get; set; }  // DECIMAL(18,2), NOT NULL
        public bool IsTenured { get; set; }  // BIT, NOT NULL
        public virtual ICollection<Course> Courses { get; set; }  // One-to-Many relationship
    }
}