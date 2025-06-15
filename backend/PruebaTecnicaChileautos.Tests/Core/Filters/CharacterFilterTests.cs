

namespace PruebaTecnicaChileautos.Tests.Core.Filters
{
    public class CharacterFilterTests
    {
        [Fact]
        public void ToQueryString_WithNoValues_ReturnsEmptyString()
        {
            var filter = new CharacterFilter();

            var result = filter.ToQueryString();

            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void ToQueryString_WithValidValues_BuildsQueryStringCorrectly()
        {
            var filter = new CharacterFilter
            {
                Name = "Rick Sanchez",
                Status = "alive",
                Species = "Human",
                Type = "Genius",
                Gender = "Male"
            };

            var result = filter.ToQueryString();

            Assert.Contains("name=Rick%20Sanchez", result);
            Assert.Contains("status=alive", result);
            Assert.Contains("species=Human", result);
            Assert.Contains("type=Genius", result);
            Assert.Contains("gender=Male", result);
            Assert.Equal(5, result.Split(',').Length);
        }

        [Fact]
        public void ToQueryString_WithInvalidStatus_SkipsStatus()
        {
            var filter = new CharacterFilter
            {
                Status = "not-a-valid-status",
                Name = "Morty"
            };

            var result = filter.ToQueryString();

            Assert.Contains("name=Morty", result);
            Assert.DoesNotContain("status=", result);
        }

        [Fact]
        public void ToQueryString_WithSomeEmptyValues_BuildsOnlyValidParts()
        {
            var filter = new CharacterFilter
            {
                Name = "Summer",
                Gender = "Female"
            };

            var result = filter.ToQueryString();

            Assert.Contains("name=Summer", result);
            Assert.Contains("gender=Female", result);
            Assert.Equal(2, result.Split(',').Length);
        }
    }
}
