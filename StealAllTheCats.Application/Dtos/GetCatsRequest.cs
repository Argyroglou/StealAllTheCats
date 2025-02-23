using StealAllTheCats.Application.Dtos.Base;

namespace StealAllTheCats.Application.Dtos;

public class GetCatsRequest : BaseRequest
{
    public long Page { get; set; } = 1;
    public long PageSize { get; set; } = 10;
    public string? Tag { get; set; }
}
