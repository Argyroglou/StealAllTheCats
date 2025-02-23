using StealAllTheCats.Application.Dtos.Base;

namespace StealAllTheCats.Application.Dtos;

public class GetCatByIdRequest : BaseRequest
{
    public long Id { get; set; }
}
