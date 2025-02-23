using System.Text.Json.Serialization;

namespace StealAllTheCats.Core.Entities.Dtos.HttpApiDto;

public class FetchCatsApiResponse
{
    [JsonPropertyName("breeds")]
    public List<Breed>? Breeds { get; set; }

    [JsonPropertyName("categories")]
    public List<Category>? Categories { get; set; }

    [JsonPropertyName("id")]
    public string Id { get; set; } = null!;

    [JsonPropertyName("url")]
    public string? Url { get; set; }

    [JsonPropertyName("width")]
    public long Width { get; set; }

    [JsonPropertyName("height")]
    public long Height { get; set; }
}

public class Category
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }
}

public class Breed
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Origin { get; set; }
    public string? LifeSpan { get; set; }

    [JsonPropertyName("temperament")]
    public string? Temperament { get; set; }
    public Weight? Weight { get; set; }
}

public class Weight
{
    [JsonPropertyName("imperial")]
    public string? Imperial { get; set; }

    [JsonPropertyName("metric")]
    public string? Metric { get; set; }
}
