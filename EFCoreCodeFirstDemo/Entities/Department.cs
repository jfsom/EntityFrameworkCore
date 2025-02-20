namespace EFCoreCodeFirstDemo.Entities
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Navigation property
        public ICollection<Employee> Employees { get; set; }
    }
}