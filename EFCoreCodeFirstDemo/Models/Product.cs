using System.ComponentModel.DataAnnotations.Schema;
namespace EFCoreCodeFirstDemo.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
    }
}