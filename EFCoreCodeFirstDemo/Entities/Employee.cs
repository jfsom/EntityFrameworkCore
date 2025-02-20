namespace EFCoreCodeFirstDemo.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //To Create a Foreign Key pointing to the Id column of the Departments table
        //it should have the Department Navigation Property
        public Department Department { get; set; }

        //In this case, EF Core automatically create the DepartmentId Foreign Key in the Employees table
    }
}