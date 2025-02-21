namespace EFCoreCodeFirstDemo.Entities
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; }

        // Navigation property for related Employees
        public ICollection<Employee> Employees { get; set; }
    }
}