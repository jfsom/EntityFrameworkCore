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
                    var derivedEntityA = new DerivedEntityA { PropertyA = "SomeValueA", CommonProperty = "SomeCommonValue" };
                    var derivedEntityB = new DerivedEntityB { PropertyB = "SomeValueB", CommonProperty = "SomeCommonValue" };

                    //context.BaseEntites.Add(derivedEntityA);
                    //context.BaseEntites.Add(derivedEntityB);

                    //List<BaseEntity> entities = new List<BaseEntity>();
                    //entities.Add(derivedEntityA);
                    //entities.Add(derivedEntityB);
                    //context.BaseEntites.AddRange(entities);
                    context.BaseEntites.AddRange(derivedEntityA, derivedEntityB);
                    context.SaveChanges();
                    Console.WriteLine("Entities are Added");
                }

                //Query the Inheritance Hierarchy
                //You can query the inheritance hierarchy using LINQ queries
                using (var context = new EFCoreDbContext())
                {
                    var baseEntities = context.BaseEntites.ToList();

                    foreach (var entity in baseEntities)
                    {
                        if (entity is DerivedEntityA derivedEntityA)
                        {
                            Console.WriteLine($"\tDerivedEntityA: Id: {derivedEntityA.Id}, PropertyA: {derivedEntityA.PropertyA}, CommonProperty: {derivedEntityA.CommonProperty}");
                        }
                        else if (entity is DerivedEntityB derivedEntityB)
                        {
                            Console.WriteLine($"\tDerivedEntityB: Id: {derivedEntityB.Id}, PropertyA: {derivedEntityB.PropertyB}, CommonProperty: {derivedEntityB.CommonProperty}");
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