namespace EFCoreCodeFirstDemo.Entities
{
    namespace EFCoreCodeFirstDemo.Entities
    {
        public class Branch
        {
            public int BranchId { get; set; }
            public string BranchLocation { get; set; }
            public string? BranchPhoneNumber { get; set; }
            public string? BranchEmail { get; set; }
            public ICollection<Student> Students { get; set; }
            public ICollection<Teacher> Teachers { get; set; }
        }
    }
}