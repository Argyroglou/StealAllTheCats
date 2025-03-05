using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StealAllTheCats.Core.Entities.Dtos;
using StealAllTheCats.Core.Entities.Dtos.HttpApiDto;
using StealAllTheCats.Core.Entities.Persistent;
using StealAllTheCats.Core.Interfaces;
using StealAllTheCats.Infrastructure.Database;

namespace StealAllTheCats.Application.Services;

public class CatService(ApplicationDbContext dbContext
    , ICatApiClient catApiClient
    , ILogger<CatService> logger) : ICatService
{
    public async Task<List<FetchCatsApiResponse>> FetchAndStoreCatsAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Fetching cat data from API");

        try
        {
            var apiCats = await catApiClient.FetchCatsAsync();
            logger.LogInformation("Fetched {count} cats from the API", apiCats.Count);

            var existingCatIds = dbContext.Cats
                .Select(c => c.CatId)
                .ToHashSet();

            var existingTags = await dbContext.Tags
                .ToDictionaryAsync(t => t.Name, StringComparer.OrdinalIgnoreCase, cancellationToken);

            List<Cat> catsToAdd = [];
            foreach (var cat in apiCats)
            {
                if (existingCatIds.Contains(cat.Id))
                    continue;

                var tagNames = ExtractTagNames(cat);
                var tags = GetOrCreateTags(tagNames, existingTags);

                catsToAdd.Add(new Cat
                {
                    CatId = cat.Id,
                    Width = cat.Width,
                    Height = cat.Height,
                    ImageUrl = cat.Url,
                    Tags = tags
                });
            }

            if (catsToAdd.Count > 0)
            {
                dbContext.Cats.AddRange(catsToAdd);
                await dbContext.SaveChangesAsync(cancellationToken);
                logger.LogInformation("Cat data saved to database: {Count}", catsToAdd.Count);
            }

            return apiCats;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while fetching and storing cats.");
            throw;
        }
    }

    public async Task<CatDataResultDto> GetCatByIdAsync(GetCatDataByIdInput input, CancellationToken cancellationToken)
    {
        logger.LogInformation("In Retrieving cat method with ID {id}", input.Id);
        try
        {
            var catDto = await dbContext.Cats
                .AsNoTracking()
                .Where(c => c.Id == input.Id)
                .Select(c => new CatDataResultDto
                {
                    CatId = c.CatId,
                    ImageUrl = c.ImageUrl,
                    Width = c.Width,
                    Height = c.Height,
                    Tags = c.Tags.Select(t => t.Name).ToList(),
                    Created = c.Created
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (catDto is null)
            {
                logger.LogInformation("Cat with ID {id} not found", input.Id);
            }

            logger.LogInformation("Retrieved cat with ID {id}", input.Id);

            return catDto ?? new CatDataResultDto();
        }
        catch (Exception ex)
        {
            logger.LogError("An error occurred {error} while retrieving the cat with ID {id}", ex, input.Id);
            throw;
        }
    }

    public async Task<List<CatDataResultDto>> GetCatsAsync(GetCatsDataInput input, CancellationToken cancellationToken)
    {
        try
        {
            var query = dbContext.Cats.AsQueryable();

            if (!string.IsNullOrWhiteSpace(input.Tag))
            {
                query = query.Where(c => c.Tags.Any(t => t.Name.Contains(input.Tag)));
            }

            var cats = await query
                .AsNoTracking()
                .Skip((int)((input.Page - 1) * input.PageSize))
                .Take((int)input.PageSize)
                .Select(c => new CatDataResultDto
                {
                    CatId = c.CatId,
                    ImageUrl = c.ImageUrl,
                    Width = c.Width,
                    Height = c.Height,
                    Tags = c.Tags.Select(t => t.Name).ToList(),
                    Created = c.Created
                })
                .ToListAsync(cancellationToken);

            if(cats.Count == 0)
            {
                logger.LogInformation("No cats found for the previous input");
            }

            return cats;
        }
        catch (Exception ex)
        {
            logger.LogError("An error occurred {error} while retrieving cats ", ex);
            throw;
        }
    }

    private static List<string> ExtractTagNames(FetchCatsApiResponse cat)
    {
        if (cat.Breeds == null)
            return [];

        List<string> tagNames = [];

        foreach (var breed in cat.Breeds)
        {
            if (string.IsNullOrWhiteSpace(breed.Temperament))
                continue;

            var temperamentTags = breed.Temperament
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(t => t.Trim())
                .Distinct();

            tagNames.AddRange(temperamentTags);
        }

        return [.. tagNames.Distinct()];
    }

    private static List<Tag> GetOrCreateTags(List<string> tagNames, Dictionary<string, Tag> existingTags)
    {
        List<Tag> tags = [];

        foreach (var tagName in tagNames)
        {
            if (!existingTags.TryGetValue(tagName, out var tag))
            {
                tag = new Tag { Name = tagName };
                existingTags[tagName] = tag;
            }
            tags.Add(tag);
        }
        return tags;
    }
}
