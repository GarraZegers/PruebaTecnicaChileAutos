using Moq.Protected;

namespace PruebaTecnicaChileautos.Tests.Infrastructure.Clients
{
    public class RickAndMortyApiClient_Character_Tests
    {
        private RickAndMortyApiClient CreateClient(HttpResponseMessage response)
        {
            var handler = new Mock<HttpMessageHandler>();
            handler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            var httpClient = new HttpClient(handler.Object)
            {
                BaseAddress = new Uri("https://fakeapi.com")
            };

            var logger = new Mock<ILogger<RickAndMortyApiClient>>();
            var options = Options.Create(new RickAndMortyOptions
            {
                BaseUrl = "https://fakeapi.com"
            });

            return new RickAndMortyApiClient(httpClient, logger.Object, options);
        }

        [Fact]
        public async Task GetAllCharactersAsync_ReturnsValidResponse_WhenApiReturnsData()
        {
            var response = new ApiResponse<CharacterDto>
            {
                Info = new PageInfo { Count = 2 },
                Results = new() { new CharacterDto { Id = 1 }, new CharacterDto { Id = 2 } }
            };

            var client = CreateClient(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = JsonContent.Create(response)
            });

            var result = await client.GetAllCharactersAsync();

            Assert.Equal(2, result.Results.Count);
        }

        [Fact]
        public async Task GetAllCharactersAsync_ReturnsEmptyResponse_WhenApiReturnsNull()
        {
            var client = CreateClient(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = JsonContent.Create<ApiResponse<CharacterDto>?>(null)
            });

            var result = await client.GetAllCharactersAsync();

            Assert.Empty(result.Results);
        }

        [Fact]
        public async Task GetSingleCharacterAsync_ReturnsValidResponse_WhenApiReturnsData()
        {
            var dto = new CharacterDto { Id = 5, Name = "Morty" };

            var client = CreateClient(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = JsonContent.Create(dto)
            });

            var result = await client.GetSingleCharacterAsync(5);

            Assert.Single(result.Results);
            Assert.Equal("Morty", result.Results[0].Name);
        }

        [Fact]
        public async Task GetSingleCharacterAsync_ReturnsEmptyResponse_WhenApiReturnsNull()
        {
            var client = CreateClient(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = JsonContent.Create<CharacterDto?>(null)
            });

            var result = await client.GetSingleCharacterAsync(5);

            Assert.Empty(result.Results);
        }

        [Fact]
        public async Task GetMultipleCharactersAsync_ReturnsValidResponse_WhenApiReturnsData()
        {
            var list = new List<CharacterDto> { new() { Id = 1 }, new() { Id = 2 } };

            var client = CreateClient(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = JsonContent.Create(list)
            });

            var result = await client.GetMultipleCharactersAsync(new() { "1", "2" });

            Assert.Equal(2, result.Results.Count);
        }

        [Fact]
        public async Task GetMultipleCharactersAsync_ReturnsSingle_WhenOneId()
        {
            var character = new CharacterDto { Id = 99 };

            var client = CreateClient(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = JsonContent.Create(character)
            });

            var result = await client.GetMultipleCharactersAsync([ "99" ]);

            Assert.Single(result.Results);
            Assert.Equal(99, result.Results[0].Id);
        }

        [Fact]
        public async Task GetFilteredCharacters_ReturnsValidResponse_WhenApiReturnsData()
        {
            var dto = new ApiResponse<CharacterDto>
            {
                Info = new PageInfo { Count = 1 },
                Results = [ new CharacterDto { Name = "Summer" } ]
            };

            var client = CreateClient(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = JsonContent.Create(dto)
            });

            var filter = new CharacterFilter { Name = "Summer" };
            var result = await client.GetFilteredCharacters(filter);

            Assert.Single(result.Results);
            Assert.Equal("Summer", result.Results[0].Name);
        }

        [Theory]
        [InlineData(typeof(HttpRequestException))]
        [InlineData(typeof(JsonException))]
        public async Task CharactersMethods_ReturnEmpty_OnExceptions(Type exType)
        {
            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Throws((Exception)Activator.CreateInstance(exType)!);

            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("https://fakeapi.com")
            };

            var logger = new Mock<ILogger<RickAndMortyApiClient>>();
            var opts = Options.Create(new RickAndMortyOptions { BaseUrl = "https://fakeapi.com" });
            var client = new RickAndMortyApiClient(httpClient, logger.Object, opts);

            Assert.Empty((await client.GetAllCharactersAsync()).Results);
            Assert.Empty((await client.GetSingleCharacterAsync(1)).Results);
            Assert.Empty((await client.GetMultipleCharactersAsync(new() { "1" })).Results);
            Assert.Empty((await client.GetFilteredCharacters(new CharacterFilter { Name = "x" })).Results);
        }
    }
}
