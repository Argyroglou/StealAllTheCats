using StealAllTheCats.Application.Dtos.Base;
using StealAllTheCats.Core.Entities.Dtos;

namespace StealAllTheCats.Application.Dtos;

public class GetCatsResponse : BaseResponse<List<GetCatResponseData>>
{
}

public class GetCatResponseData
{
    public string CatId { get; set; } = null!;
    public long Width { get; set; }
    public long Height { get; set; }
    public string? ImageUrl { get; set; }
    public List<string> Tags { get; set; } = null!;
    public DateTime Created { get; set; }
}
