using AutoMapper;
using StealAllTheCats.Application.Dtos;
using StealAllTheCats.Core.Entities.Dtos;
using StealAllTheCats.Core.Entities.Persistent;

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
