using EFCoreCodeFirstDemo.Entities;
namespace EFCoreCodeFirstDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using EFCoreDbContext context = new EFCoreDbContext();

                //Fetch the Student Details whose Id is 1
                var studentId1 = context.Students.Find(1);
                if (studentId1 != null)
                {
                    studentId1.Name = "Name Updated";
                    studentId1.Branch = "Branch Updated";
                    context.SaveChanges();
                }

                //Console.ReadKey();
                //Console.ReadLine();
                //Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}"); ;
            }
        }
    }
}