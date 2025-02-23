namespace StealAllTheCats.Core.Entities.Dtos;

public class CatDataResultDto
{
    public string CatId { get; set; } = null!;
    public long Width { get; set; }
    public long Height { get; set; }
    public string? ImageUrl { get; set; }
    public List<string> Tags { get; set; } = null!;
    public DateTime Created { get; set; }
}
