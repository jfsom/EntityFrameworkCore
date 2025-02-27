using EFCoreCodeFirstDemo.Entities;
namespace EFCoreCodeFirstDemo
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                //Use Inheritance in our Code
                //Now, you can create and work with instances of the derived classes
                //and save them to the database
                using (var context = new EFCoreDbContext())
                {
                    var derivedEntityA = new DerivedEntity1 { Property1 = "SomeValue1", CommonProperty = "SomeCommonValue" };
                    var derivedEntityB = new DerivedEntity2 { Property2 = "SomeValue2", CommonProperty = "SomeCommonValue" };

                    context.BaseEntites.AddRange(derivedEntityA, derivedEntityB);
                    context.SaveChanges();
                    Console.WriteLine("Entities are Added");
                }

                //Query the Inheritance Hierarchy
                //You can query the inheritance hierarchy using LINQ queries
                using (var context = new EFCoreDbContext())
                {
                    var baseEntities = context.BaseEntites.ToList();

                    foreach (var vehicle in baseEntities)
                    {
                        if (vehicle is DerivedEntity1 derivedEntityA)
                        {
                            Console.WriteLine($"\tDerivedEntityA: Id: {derivedEntityA.Id}, Property1: {derivedEntityA.Property1}, CommonProperty: {derivedEntityA.CommonProperty}");
                        }
                        else if (vehicle is DerivedEntity2 derivedEntityB)
                        {
                            Console.WriteLine($"\tDerivedEntityB: Id: {derivedEntityB.Id}, Property2: {derivedEntityB.Property2}, CommonProperty: {derivedEntityB.CommonProperty}");
                        }
                    }
                }

                Console.Read();
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}"); ;
            }
        }
    }
}