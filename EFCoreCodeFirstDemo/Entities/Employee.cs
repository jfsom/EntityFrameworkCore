namespace EFCoreCodeFirstDemo.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Navigation property
        public Department Department { get; set; }

        // Foreign key property
        public int DepartmentId { get; set; }
    }
}