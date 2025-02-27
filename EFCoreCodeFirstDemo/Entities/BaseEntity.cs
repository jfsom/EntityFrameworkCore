using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace EFCoreCodeFirstDemo.Entities
{
    [Table("BaseTable")]
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string CommonProperty { get; set; }
    }
}