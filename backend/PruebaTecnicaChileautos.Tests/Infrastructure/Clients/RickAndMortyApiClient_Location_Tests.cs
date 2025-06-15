

using Moq.Protected;

namespace PruebaTecnicaChileautos.Tests.Infrastructure.Clients
{
    public class RickAndMortyApiClient_Location_Tests
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
        public async Task GetAllLocationsAsync_ReturnsValidResponse_WhenApiReturnsData()
        {
            var response = new ApiResponse<LocationDto>
            {
                Info = new PageInfo { Count = 2 },
                Results = new() { new LocationDto { Id = 1 }, new LocationDto { Id = 2 } }
            };

            var client = CreateClient(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = JsonContent.Create(response)
            });

            var result = await client.GetAllLocationsAsync();

            Assert.Equal(2, result.Results.Count);
        }

        [Fact]
        public async Task GetAllLocationsAsync_ReturnsEmptyResponse_WhenApiReturnsNull()
        {
            var client = CreateClient(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = JsonContent.Create<ApiResponse<LocationDto>?>(null)
            });

            var result = await client.GetAllLocationsAsync();

            Assert.Empty(result.Results);
        }

        [Fact]
        public async Task GetSingleLocationAsync_ReturnsValidResponse_WhenApiReturnsData()
        {
            var dto = new LocationDto { Id = 10, Name = "Earth" };

            var client = CreateClient(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = JsonContent.Create(dto)
            });

            var result = await client.GetSingleLocationAsync(10);

            Assert.Single(result.Results);
            Assert.Equal("Earth", result.Results[0].Name);
        }

        [Fact]
        public async Task GetSingleLocationAsync_ReturnsEmptyResponse_WhenApiReturnsNull()
        {
            var client = CreateClient(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = JsonContent.Create<LocationDto?>(null)
            });

            var result = await client.GetSingleLocationAsync(1);

            Assert.Empty(result.Results);
        }

        [Fact]
        public async Task GetMultipleLocationsAsync_ReturnsValidResponse_WhenApiReturnsData()
        {
            var list = new List<LocationDto> { new() { Id = 1 }, new() { Id = 2 } };

            var client = CreateClient(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = JsonContent.Create(list)
            });

            var result = await client.GetMultipleLocationsAsync(new() { "1", "2" });

            Assert.Equal(2, result.Results.Count);
        }

        [Fact]
        public async Task GetMultipleLocationsAsync_ReturnsSingle_WhenOnlyOneId()
        {
            var dto = new LocationDto { Id = 7 };

            var client = CreateClient(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = JsonContent.Create(dto)
            });

            var result = await client.GetMultipleLocationsAsync(new() { "7" });

            Assert.Single(result.Results);
            Assert.Equal(7, result.Results[0].Id);
        }

        [Fact]
        public async Task GetFilteredLocations_ReturnsValidResponse_WhenApiReturnsData()
        {
            var response = new ApiResponse<LocationDto>
            {
                Info = new PageInfo { Count = 1 },
                Results = new() { new LocationDto { Name = "Citadel of Ricks" } }
            };

            var client = CreateClient(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = JsonContent.Create(response)
            });

            var filter = new LocationFilter { Name = "Citadel" };
            var result = await client.GetFilteredLocations(filter);

            Assert.Single(result.Results);
            Assert.Contains("Citadel", result.Results[0].Name);
        }

        [Theory]
        [InlineData(typeof(HttpRequestException))]
        [InlineData(typeof(JsonException))]
        public async Task LocationMethods_ReturnEmpty_OnExceptions(Type exType)
        {
            var handler = new Mock<HttpMessageHandler>();
            handler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Throws((Exception)Activator.CreateInstance(exType)!);

            var client = new RickAndMortyApiClient(
                new HttpClient(handler.Object) { BaseAddress = new Uri("https://fakeapi.com") },
                new Mock<ILogger<RickAndMortyApiClient>>().Object,
                Options.Create(new RickAndMortyOptions { BaseUrl = "https://fakeapi.com" })
            );

            Assert.Empty((await client.GetAllLocationsAsync()).Results);
            Assert.Empty((await client.GetSingleLocationAsync(1)).Results);
            Assert.Empty((await client.GetMultipleLocationsAsync([ "1" ])).Results);
            Assert.Empty((await client.GetFilteredLocations(new LocationFilter { Name = "x" })).Results);
        }
    }
}
