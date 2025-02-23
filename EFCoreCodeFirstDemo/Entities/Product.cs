namespace EFCoreCodeFirstDemo.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        // Each product belongs to exactly one Category
        // Foreign Key
        public int CategoryId { get; set; }
        // Navigation Property
        public Category Category { get; set; }
    }
}