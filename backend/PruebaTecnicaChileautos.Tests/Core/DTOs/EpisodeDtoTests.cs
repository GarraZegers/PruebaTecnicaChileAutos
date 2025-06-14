
namespace PruebaTecnicaChileautos.Tests.Core.DTOs
{
    public class EpisodeDtoTests
    {
        [Fact]
        public void EpisodeDto_DefaultConstructor_InitializesCollections()
        {
            var dto = new EpisodeDto();

            Assert.NotNull(dto.Name);
            Assert.NotNull(dto.AirDate);
            Assert.NotNull(dto.Episode);
            Assert.NotNull(dto.Characters);
            Assert.NotNull(dto.Url);
            Assert.NotNull(dto.Created);
            Assert.Empty(dto.Characters);
        }

        [Fact]
        public void EpisodeDto_AssignsValuesCorrectly()
        {
            var dto = new EpisodeDto
            {
                Id = 1,
                Name = "Pilot",
                AirDate = "December 2, 2013",
                Episode = "S01E01",
                Characters =[
                    "https://example.com/character/1",
                    "https://example.com/character/2"
                ],
                Url = "https://example.com/episode/1",
                Created = "2023-01-01T00:00:00Z"
            };

            Assert.Equal(1, dto.Id);
            Assert.Equal("Pilot", dto.Name);
            Assert.Equal("December 2, 2013", dto.AirDate);
            Assert.Equal("S01E01", dto.Episode);
            Assert.Equal(2, dto.Characters.Count);
            Assert.Equal("https://example.com/episode/1", dto.Url);
            Assert.Equal("2023-01-01T00:00:00Z", dto.Created);
        }
    }
}
