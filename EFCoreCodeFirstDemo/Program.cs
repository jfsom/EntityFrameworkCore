using EFCoreCodeFirstDemo.Entities;
namespace EFCoreCodeFirstDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Main Method Started");
                Thread t1 = new Thread(Method1);
                Thread t2 = new Thread(Method2);
                t1.Start();
                t2.Start();
                t1.Join();
                t2.Join();
                Console.WriteLine("Main Method Completed");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}"); ;
            }
        }

        public static void Method1()
        {
            using EFCoreDbContext context = new EFCoreDbContext();

            //Fetch the Student Details whose Id is 1
            var studentId1 = context.Students.Find(1);

            //Before Updating Delay the Thread by 2 Seconds
            Thread.Sleep(TimeSpan.FromSeconds(2));

            if (studentId1 != null)
            {
                studentId1.Name = studentId1.Name + "Method3";
                studentId1.Branch = studentId1.Branch + "Method3";
                context.SaveChanges();
                Console.WriteLine("Student Updated by Method3");
            }
        }
        public static void Method2()
        {
            using EFCoreDbContext context = new EFCoreDbContext();

            //Fetch the Student Details whose Id is 1
            var studentId1 = context.Students.Find(1);

            //Before Updating Delay the Thread by 2 Seconds
            Thread.Sleep(TimeSpan.FromSeconds(2));

            if (studentId1 != null)
            {
                studentId1.Name = studentId1.Name + " Method4";
                studentId1.Branch = studentId1.Branch + " Method4";
                context.SaveChanges();
                Console.WriteLine("Student Updated by Method4");
            }
        }
    }
}