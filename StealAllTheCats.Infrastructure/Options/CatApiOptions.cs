namespace StealAllTheCats.Infrastructure.Options;

public class CatApiOptions
{
    public string BaseAddress { get; set; } = null!;
    public long FetchLimit { get; set; } = 25; //Default value
    public string ApiKey { get; set; } = null!;
}

