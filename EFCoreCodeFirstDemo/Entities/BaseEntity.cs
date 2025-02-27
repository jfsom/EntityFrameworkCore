using System.ComponentModel.DataAnnotations;
namespace EFCoreCodeFirstDemo.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string CommonProperty { get; set; }
    }
}