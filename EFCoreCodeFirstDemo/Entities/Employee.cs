using System.ComponentModel.DataAnnotations.Schema;
namespace EFCoreCodeFirstDemo.Entities
{
    public class Employee
    {
        public int EmployeeId { get; set; } //PK

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfJoining { get; set; }

        public DateTime DateOfBirth { get; set; }

        // Navigation property for related Department
        public int DepartmentId { get; set; } //FK
        public Department Department { get; set; }

        // Dynamically calculated property
        public int Tenure
        {
            get
            {
                var today = DateTime.Today;
                int years = today.Year - DateOfJoining.Year;

                // Adjust if the employee's anniversary date hasn't occurred yet this year
                if (DateOfJoining.Date > today.AddYears(-years))
                    years--;

                return years;
            }
        }

        public int Age
        {
            get
            {
                var today = DateTime.Today;
                var age = today.Year - DateOfBirth.Year;

                // Adjust age if the birthday hasn't occurred yet this year
                if (DateOfBirth.Date > today.AddYears(-age))
                    age--;

                return age;
            }
        }

        public string FullName => $"{FirstName} {LastName}";
    }
}