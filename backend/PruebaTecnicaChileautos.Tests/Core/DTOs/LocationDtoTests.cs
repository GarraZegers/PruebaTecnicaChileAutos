

namespace PruebaTecnicaChileautos.Tests.Core.DTOs
{
    public class LocationDtoTests
    {
        [Fact]
        public void LocationDto_DefaultConstructor_InitializesCollections()
        {
            var dto = new LocationDto();

            Assert.NotNull(dto.Name);
            Assert.NotNull(dto.Type);
            Assert.NotNull(dto.Dimension);
            Assert.NotNull(dto.Residents);
            Assert.NotNull(dto.Url);
            Assert.NotNull(dto.Created);
            Assert.Empty(dto.Residents);
        }

        [Fact]
        public void LocationDto_AssignsValuesCorrectly()
        {
            var dto = new LocationDto
            {
                Id = 42,
                Name = "Citadel of Ricks",
                Type = "Space station",
                Dimension = "unknown",
                Residents = [
                    "https://example.com/character/1",
                    "https://example.com/character/2"
                ],
                Url = "https://example.com/location/42",
                Created = "2023-05-15T10:00:00Z"
            };

            Assert.Equal(42, dto.Id);
            Assert.Equal("Citadel of Ricks", dto.Name);
            Assert.Equal("Space station", dto.Type);
            Assert.Equal("unknown", dto.Dimension);
            Assert.Equal(2, dto.Residents.Count);
            Assert.Equal("https://example.com/location/42", dto.Url);
            Assert.Equal("2023-05-15T10:00:00Z", dto.Created);
        }
    }
}
