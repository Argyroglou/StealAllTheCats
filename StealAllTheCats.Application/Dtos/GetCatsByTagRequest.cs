using StealAllTheCats.Application.Dtos.Base;

namespace StealAllTheCats.Application.Dtos;

public class GetCatsByTagRequest : BaseRequest
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string Tag { get; set; } = string.Empty;
}
