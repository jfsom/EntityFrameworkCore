using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace EFCoreCodeFirstDemo.Entities
{
    public class Passport
    {
        [Key]
        public int Id { get; set; } //PK
        public string PassportNumber { get; set; }
        public int UserId { get; set; } //FK, Required Relationship
        [ForeignKey("UserId")]
        public User User { get; set; }  // Navigation property
    }
}