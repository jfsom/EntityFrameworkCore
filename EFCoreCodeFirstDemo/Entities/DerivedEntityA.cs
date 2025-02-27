using System.ComponentModel.DataAnnotations.Schema;
namespace EFCoreCodeFirstDemo.Entities
{
    [Table("DerivedTable1")]
    public class DerivedEntity1 : BaseEntity
    {
        public string Property1 { get; set; }
    }

    [Table("DerivedTable2")]
    public class DerivedEntity2 : BaseEntity
    {
        public string Property2 { get; set; }
    }
}