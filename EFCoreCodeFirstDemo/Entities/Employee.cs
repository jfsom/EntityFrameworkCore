using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreCodeFirstDemo.Entities
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }

        [ForeignKey("Manager")]
        public int? ManagerId { get; set; }
        public Employee Manager { get; set; }

        [InverseProperty("Manager")]
        public ICollection<Employee> Subordinates { get; set; } = new List<Employee>();
    }
}