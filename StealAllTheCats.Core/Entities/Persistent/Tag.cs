namespace StealAllTheCats.Core.Entities.Persistent;
public class Tag
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public List<Cat> Cats { get; set; } = null!;
}