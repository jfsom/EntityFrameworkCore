using System.ComponentModel.DataAnnotations.Schema;
namespace EFCoreCodeFirstDemo.Entities
{
    public class DerivedEntityA : BaseEntity
    {
        public string PropertyA { get; set; }
    }

    public class DerivedEntityB : BaseEntity
    {
        public string PropertyB { get; set; }
    }
}