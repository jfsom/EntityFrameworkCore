using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace EFCoreCodeFirstDemo.Entities
{
    [Table("Countries")]
    public class Country
    {
        [Key]
        public int CountryId { get; set; }

        [Required]
        [MaxLength(100)]
        public string CountryName { get; set; }

        [Required]
        [MaxLength(10)]
        public string CountryCode { get; set; }

        // Navigation Property
        public ICollection<State> States { get; set; }
    }
}
