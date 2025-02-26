namespace EFCoreCodeFirstDemo.Entities
{
    public class Student
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int StandardId { get; set; }
        public virtual Standard Standard { get; set; }
        public virtual StudentAddress StudentAddress { get; set; }
    }
}