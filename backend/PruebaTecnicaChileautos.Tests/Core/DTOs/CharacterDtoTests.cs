

namespace PruebaTecnicaChileautos.Tests.Core.DTOs
{
    public class CharacterDtoTests
    {
        [Fact]
        public void CharacterDto_DefaultConstructor_InitializesCollectionsAndObjects()
        {
            var dto = new CharacterDto();

            Assert.NotNull(dto.Name);
            Assert.NotNull(dto.Status);
            Assert.NotNull(dto.Species);
            Assert.NotNull(dto.Type);
            Assert.NotNull(dto.Gender);
            Assert.NotNull(dto.Origin);
            Assert.NotNull(dto.Location);
            Assert.NotNull(dto.Image);
            Assert.NotNull(dto.Episode);
            Assert.NotNull(dto.Url);
            Assert.NotNull(dto.Created);

            Assert.Empty(dto.Episode);
            Assert.IsType<LocationInfo>(dto.Origin);
            Assert.IsType<LocationInfo>(dto.Location);
        }

        [Fact]
        public void CharacterDto_AssignsValuesCorrectly()
        {
            var dto = new CharacterDto
            {
                Id = 10,
                Name = "Rick Sanchez",
                Status = "Alive",
                Species = "Human",
                Type = "Scientist",
                Gender = "Male",
                Origin = new LocationInfo { Name = "Earth", Url = "https://example.com/earth" },
                Location = new LocationInfo { Name = "Citadel", Url = "https://example.com/citadel" },
                Image = "https://example.com/image.png",
                Episode =[ "https://example.com/episode/1" ],
                Url = "https://example.com/character/10",
                Created = "2023-01-01T00:00:00Z"
            };

            Assert.Equal(10, dto.Id);
            Assert.Equal("Rick Sanchez", dto.Name);
            Assert.Equal("Alive", dto.Status);
            Assert.Equal("Human", dto.Species);
            Assert.Equal("Scientist", dto.Type);
            Assert.Equal("Male", dto.Gender);
            Assert.Equal("Earth", dto.Origin.Name);
            Assert.Equal("Citadel", dto.Location.Name);
            Assert.Single(dto.Episode);
            Assert.Equal("https://example.com/character/10", dto.Url);
            Assert.Equal("2023-01-01T00:00:00Z", dto.Created);
        }
    }
}
