using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace EFCoreCodeFirstDemo.Entities
{
    public class Product
    {
        public int ProductId { get; set; } // Primary Key

        [MaxLength(100)]
        public string Name { get; set; }

        [Range(0, double.MaxValue)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }
        public string? Description { get; set; }
        public bool IsInStock { get; set; }
        // Navigation Property - One Product has many OrderItems
        public ICollection<OrderItem> OrderItems { get; set; }

        // Concurrency Token
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}