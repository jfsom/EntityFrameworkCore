namespace EFCoreCodeFirstDemo.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        // IsActive is not included here, it will be a shadow property with a default value
    }
}
