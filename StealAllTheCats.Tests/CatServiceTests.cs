using Xunit;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StealAllTheCats.Application.Services;
using StealAllTheCats.Core.Entities.Dtos;
using StealAllTheCats.Core.Entities.Dtos.HttpApiDto;
using StealAllTheCats.Core.Entities.Persistent;
using StealAllTheCats.Core.Interfaces;
using StealAllTheCats.Infrastructure.Database;
using Microsoft.Extensions.Options;
using StealAllTheCats.Infrastructure.Options;

namespace StealAllTheCats.Tests
{
    public class CatServiceTests
    {
        private readonly Mock<ICatApiClient> _catApiClientMock;
        private readonly Mock<ILogger<CatService>> _loggerMock;
        private readonly ApplicationDbContext _dbContext;
        private readonly ICatService _catService;
        private readonly IOptions<CatApiOptions> _options;

        public CatServiceTests()
        {
            _catApiClientMock = new Mock<ICatApiClient>();
            _loggerMock = new Mock<ILogger<CatService>>();

            // Set up in-memory database for testing
            // Set up in-memory database for testing with a unique name
            //var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            //    .UseInMemoryDatabase(databaseName: $"StealAllTheCats_{Guid.NewGuid()}")
            //    .Options;


            //_dbContext = new ApplicationDbContext(options);
            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();

            _catService = new CatService(_dbContext, _catApiClientMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task FetchAndStoreCatsAsync_ShouldFetchAndSaveCats()
        {
            // Arrange
            var apiCats = new List<FetchCatsApiResponse>
            {
                new() { Id = "1", Url = "http://example.com/cat1.jpg", Breeds = [new() { Temperament = "Playful" }] },
                new() { Id = "2", Url = "http://example.com/cat2.jpg", Breeds = [new() { Temperament = "Calm" }] }
            };

            _catApiClientMock
                .Setup(client => client.FetchCatsAsync())
                .ReturnsAsync(apiCats);

            var cancellationToken = CancellationToken.None;

            // Act
            var result = await _catService.FetchAndStoreCatsAsync(cancellationToken);

            // Assert
            _catApiClientMock.Verify(client => client.FetchCatsAsync(), Times.Once);
            Assert.Equal(2, result.Count);
            Assert.Contains(result, c => c.Id == "1");
            Assert.Contains(result, c => c.Id == "2");

            // Verify that cats were saved in the DB
            var dbCats = _dbContext.Cats.ToList();
            Assert.Equal(2, dbCats.Count);
        }
    }
}
