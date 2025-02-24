namespace EFCoreCodeFirstDemo.Entities
{
    public interface ITimestampedEntity
    {
        DateTime CreatedAt { get; set; }
        DateTime UpdatedAt { get; set; }
    }
}
