using StealAllTheCats.Core.Entities.Dtos;
using StealAllTheCats.Core.Entities.Dtos.HttpApiDto;

namespace StealAllTheCats.Core.Interfaces;
public interface ICatService
{
    Task<List<FetchCatsApiResponse>> FetchAndStoreCatsAsync(CancellationToken cancellationToken = default);
    Task<CatDataResultDto> GetCatByIdAsync(GetCatDataByIdInput input, CancellationToken cancellationToken = default);
    Task<List<CatDataResultDto>> GetCatsAsync(GetCatsDataInput input, CancellationToken cancellationToken = default);
}
