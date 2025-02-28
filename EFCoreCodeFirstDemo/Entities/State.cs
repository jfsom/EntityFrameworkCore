using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace EFCoreCodeFirstDemo.Entities
{
    [Table("States")]
    public class State
    {
        [Key]
        public int StateId { get; set; }

        [Required]
        [MaxLength(100)]
        public string StateName { get; set; }

        public int CountryId { get; set; } // Foreign Key

        // Navigation Properties
        public Country Country { get; set; }
        public ICollection<City> Cities { get; set; }
    }
}