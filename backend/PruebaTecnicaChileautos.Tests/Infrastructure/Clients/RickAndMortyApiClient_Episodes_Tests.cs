
using Moq.Protected;


namespace PruebaTecnicaChileautos.Tests.Infrastructure.Clients
{
    public class RickAndMortyApiClient_Episodes_Tests
    {
        private RickAndMortyApiClient CreateClient(HttpResponseMessage fakeResponse)
        {
            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(fakeResponse);

            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("https://fakeapi.com")
            };

            var logger = new Mock<ILogger<RickAndMortyApiClient>>();
            var opts = Options.Create(new RickAndMortyOptions { BaseUrl = "https://fakeapi.com" });

            return new RickAndMortyApiClient(httpClient, logger.Object, opts);
        }


        [Fact]
        public async Task GetAllEpisodesAsync_ReturnsData_WhenApiReturnsOk()
        {
            var fake = new ApiResponse<EpisodeDto>
            {
                Info = new PageInfo { Count = 2, Pages = 1 },
                Results = [ new EpisodeDto { Id = 1 }, new EpisodeDto { Id = 2 } ]
            };
            var client = CreateClient(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = JsonContent.Create(fake)
            });

            var result = await client.GetAllEpisodesAsync();

            Assert.Equal(2, result.Results.Count);
            Assert.Equal(2, result.Info.Count);
        }

        [Fact]
        public async Task GetAllEpisodesAsync_ReturnsEmpty_WhenApiReturnsNull()
        {
            var client = CreateClient(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = JsonContent.Create<ApiResponse<EpisodeDto>?>(null)
            });

            var result = await client.GetAllEpisodesAsync();

            Assert.Empty(result.Results);
        }


        [Fact]
        public async Task GetSingleEpisodeAsync_ReturnsOne_WhenApiReturnsObject()
        {
            var dto = new EpisodeDto { Id = 42, Episode = "S03E05" };
            var client = CreateClient(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = JsonContent.Create(dto)
            });

            var result = await client.GetSingleEpisodeAsync(42);

            Assert.Single(result.Results);
            Assert.Equal(42, result.Results[0].Id);
        }

        [Fact]
        public async Task GetSingleEpisodeAsync_ReturnsEmpty_WhenApiReturnsNull()
        {
            var client = CreateClient(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = JsonContent.Create<EpisodeDto?>(null)
            });

            var result = await client.GetSingleEpisodeAsync(99);

            Assert.Empty(result.Results);
        }


        [Fact]
        public async Task GetMultipleEpisodesAsync_ReturnsList_WhenApiReturnsArray()
        {
            var list = new List<EpisodeDto> { new() { Id = 1 }, new() { Id = 2 } };
            var client = CreateClient(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = JsonContent.Create(list)
            });

            var result = await client.GetMultipleEpisodesAsync(new() { "1", "2" });

            Assert.Equal(2, result.Results.Count);
        }

        [Fact]
        public async Task GetMultipleEpisodesAsync_ReturnsSingle_WhenOnlyOneId()
        {
            var dto = new EpisodeDto { Id = 7 };
            var client = CreateClient(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = JsonContent.Create(dto)
            });

            var result = await client.GetMultipleEpisodesAsync(new() { "7" });

            Assert.Single(result.Results);
            Assert.Equal(7, result.Results[0].Id);
        }


        [Fact]
        public async Task GetFilteredEpisodes_ReturnsData_WhenFilterMatches()
        {
            var fake = new ApiResponse<EpisodeDto>
            {
                Info = new PageInfo { Count = 1, Pages = 1 },
                Results = new() { new EpisodeDto { Name = "Pilot" } }
            };
            var client = CreateClient(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = JsonContent.Create(fake)
            });

            var filter = new EpisodeFilter { Name = "Pilot" };
            var result = await client.GetFilteredEpisodes(filter);

            Assert.Single(result.Results);
            Assert.Equal("Pilot", result.Results[0].Name);
        }

        [Theory]
        [InlineData(typeof(HttpRequestException))]
        [InlineData(typeof(JsonException))]
        public async Task AllEpisodeMethods_ReturnEmpty_OnExceptions(Type exType)
        {   
            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .Throws((Exception)Activator.CreateInstance(exType)!);

            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("https://fakeapi.com")
            };

            var logger = new Mock<ILogger<RickAndMortyApiClient>>();
            var opts = Options.Create(new RickAndMortyOptions { BaseUrl = "https://fakeapi.com" });
            var client = new RickAndMortyApiClient(httpClient, logger.Object, opts);

            Assert.Empty((await client.GetAllEpisodesAsync()).Results);
            Assert.Empty((await client.GetSingleEpisodeAsync(1)).Results);
            Assert.Empty((await client.GetMultipleEpisodesAsync(new() { "1" })).Results);
            Assert.Empty((await client.GetFilteredEpisodes(new EpisodeFilter { Name = "x" })).Results);
        }
    }
}
