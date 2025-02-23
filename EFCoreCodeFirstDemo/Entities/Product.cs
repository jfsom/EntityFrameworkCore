using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace EFCoreCodeFirstDemo.Entities
{
    [Index(nameof(SKU), IsUnique = true)]
    [Index(nameof(SerialNumber), IsUnique = true)]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] // Application must provide the GUID.
        public Guid ProductId { get; set; } // Non-Identity Primary Key

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SerialNumber { get; set; } // Non-Primary Key, Identity
        public string Name { get; set; }
        public string Category { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        //PROD-{Category}-{SerialNumber}
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string SKU { get; set; } // Unique, Non-Primary Key
        public DateTime CreatedOn { get; set; } // Default value, Current Date
        public string CreatedBy { get; set; } // Fixed value System
    }
}