using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreCodeFirstDemo.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [ForeignKey("Department")]
        public int DepartmentReferenceId { get; set; }

        //Related Standard Navigational Property
        public Department Department { get; set; }
    }
}