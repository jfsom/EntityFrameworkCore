namespace EFCoreCodeFirstDemo.Entities
{
    public class Standard
    {
        public int StandardId { get; set; }
        public string StandardName { get; set; }
        public string Description { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}