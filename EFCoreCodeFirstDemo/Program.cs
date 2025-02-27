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
                    context.BaseEntites.Add(derivedEntityA);

                    var derivedEntityB = new DerivedEntity2 { Property2 = "SomeValue2", CommonProperty = "SomeCommonValue" };
                    context.BaseEntites.Add(derivedEntityB);

                    context.SaveChanges();
                    Console.WriteLine("Entities are Added");
                }

                //Query the Inheritance Hierarchy
                //You can query the inheritance hierarchy using LINQ queries
                using (var context = new EFCoreDbContext())
                {
                    var DerivedEntities1 = context.BaseEntites.OfType<DerivedEntity1>().ToList();
                    foreach (DerivedEntity1 derivedEntity1 in DerivedEntities1)
                    {
                        Console.WriteLine($"\tDerivedEntityA: Id: {derivedEntity1.Id}, Property1: {derivedEntity1.Property1}, CommonProperty: {derivedEntity1.CommonProperty}");
                    }

                    var DerivedEntities2 = context.BaseEntites.OfType<DerivedEntity2>().ToList();
                    foreach (DerivedEntity2 derivedEntity2 in DerivedEntities2)
                    {
                        Console.WriteLine($"\tDerivedEntityA: Id: {derivedEntity2.Id}, Property2: {derivedEntity2.Property2}, CommonProperty: {derivedEntity2.CommonProperty}");
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