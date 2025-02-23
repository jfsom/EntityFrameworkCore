using System;
namespace FluentInterfaceDesignPattern
{
    public class Program
    {
        static void Main(string[] args)
        {
            FluentStudent student = new FluentStudent();
            student.StudentRegedNumber("BQPPR123456")
                   .NameOfTheStudent("Pranaya Rout")
                   .BornOn("10/10/1992")
                   .StudyOn("CSE")
                   .StaysAt("BBSR, Odisha");

            Console.Read();
        }
    }

    public class Student
    {
        public string RegdNo { get; set; }
        public string Name { get; set; }
        public DateTime DOB { get; set; }
        public string Branch { get; set; }
        public string Address { get; set; }
    }

    public class FluentStudent
    {
        private Student student = new Student();
        public FluentStudent StudentRegedNumber(string RegdNo)
        {
            student.RegdNo = RegdNo;
            return this;
        }
        public FluentStudent NameOfTheStudent(string Name)
        {
            student.Name = Name;
            return this;
        }
        public FluentStudent BornOn(string DOB)
        {
            student.DOB = Convert.ToDateTime(DOB);
            return this;
        }
        public FluentStudent StudyOn(string Branch)
        {
            student.Branch = Branch;
            return this;
        }
        public FluentStudent StaysAt(string Address)
        {
            student.Address = Address;
            return this;
        }
    }
}