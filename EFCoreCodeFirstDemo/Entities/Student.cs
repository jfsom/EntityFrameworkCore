namespace EFCoreCodeFirstDemo.Entities
{
    public class Student
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int BranchId { get; set; }
        public virtual Branch Branch { get; set; } //Marking the Property as Virtual to Support Lazy Loading
        public virtual Address Address { get; set; } //Marking the Property as Virtual to Support Lazy Loading
        public virtual ICollection<Course> Courses { get; set; } //Marking the Property as Virtual to Support Lazy Loading
    }
}