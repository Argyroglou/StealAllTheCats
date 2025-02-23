using System.Text.Json;
using Microsoft.Extensions.Options;
using StealAllTheCats.Core.Entities.Dtos.HttpApiDto;
using StealAllTheCats.Core.Interfaces;
using StealAllTheCats.Infrastructure.Options;

namespace StealAllTheCats.Infrastructure.ApiClients;

public class CatApiClient(HttpClient httpClient, IOptions<CatApiOptions> catOptions) : ICatApiClient
{
    private readonly CatApiOptions _catApiOptions = catOptions.Value;

    public async Task<List<FetchCatsApiResponse>> FetchCatsAsync()
    {
        var url = $"{_catApiOptions.BaseAddress}?limit={_catApiOptions.FetchLimit}";
        
        httpClient.DefaultRequestHeaders.Clear();
        httpClient.DefaultRequestHeaders.Add("x-api-key", _catApiOptions.ApiKey);

        var response = await httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var jsonString = await response.Content.ReadAsStringAsync();

        var cats = JsonSerializer.Deserialize<List<FetchCatsApiResponse>>(jsonString);

        return cats ?? [];
    }
}