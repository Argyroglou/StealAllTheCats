using AutoMapper;
using StealAllTheCats.Application.Dtos;
using StealAllTheCats.Core.Entities.Dtos;

namespace StealAllTheCats.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<GetCatByIdRequest, GetCatDataByIdInput>();
        CreateMap<GetCatsRequest, GetCatsDataInput>();
        CreateMap<CatDataResultDto, GetCatResponseData>();
    }
}
