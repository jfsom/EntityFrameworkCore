namespace EFCoreCodeFirstDemo.Entities
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }

        // Self-Referential Relationship
        public int? ManagerId { get; set; }
        public Employee Manager { get; set; }
        public ICollection<Employee> Subordinates { get; set; } = new List<Employee>();
    }
}
