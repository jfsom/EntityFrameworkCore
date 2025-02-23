namespace EFCoreCodeFirstDemo.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public Passport Passport { get; set; }  // Navigation property
    }
}