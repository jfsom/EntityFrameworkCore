using EFCoreCodeFirstDemo.Entities;

namespace EFCorePropertyConfigurations.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; } // Will configure column name
        public decimal Price { get; set; } // Will configure precision and scale
        public string Description { get; set; } //Will Ignore this property
        public byte[] RowVersion { get; set; } // Will configure as concurrency token
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}