using StealAllTheCats.Core.Entities.Dtos.HttpApiDto;

namespace StealAllTheCats.Core.Interfaces;

public interface ICatApiClient
{
    Task<List<FetchCatsApiResponse>> FetchCatsAsync();
}
