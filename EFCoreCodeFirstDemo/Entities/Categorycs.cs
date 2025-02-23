namespace EFCoreCodeFirstDemo.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }

        // One to Many Self-referential relationship (Parent and Subcategories)
        // Foreign Key
        public int? ParentCategoryId { get; set; }
        public Category ParentCategory { get; set; }
        public ICollection<Category> Subcategories { get; set; } = new List<Category>();

        // One to Many Relationship with Products
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
