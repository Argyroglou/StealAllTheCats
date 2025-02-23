
namespace StealAllTheCats.Core.Entities.Dtos;

public class GetCatsDataInput
{
    public long Page { get; set; } = 1;
    public long PageSize { get; set; } = 10;
    public string? Tag { get; set; }
}
