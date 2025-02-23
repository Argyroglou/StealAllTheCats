namespace StealAllTheCats.Core.Entities.Persistent;
public class Cat
{
    public long Id { get; set; }
    public string CatId { get; set; } = null!;
    public long Width { get; set; }
    public long Height { get; set; }
    public string? ImageUrl { get; set; }
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public List<Tag> Tags { get; set; } = null!;
}