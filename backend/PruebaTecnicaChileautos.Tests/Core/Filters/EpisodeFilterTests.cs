

namespace PruebaTecnicaChileautos.Tests.Core.Filters
{
    public class EpisodeFilterTests
    {
        [Fact]
        public void ToQueryString_WithNoValues_ReturnsEmptyString()
        {
            var filter = new EpisodeFilter();

            var result = filter.ToQueryString();

            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void ToQueryString_WithOnlyName_ReturnsNameParam()
        {
            var filter = new EpisodeFilter
            {
                Name = "Pilot"
            };

            var result = filter.ToQueryString();

            Assert.Equal("name=Pilot", result);
        }

        [Fact]
        public void ToQueryString_WithOnlyEpisode_ReturnsEpisodeParam()
        {
            var filter = new EpisodeFilter
            {
                Episode = "S01E01"
            };

            var result = filter.ToQueryString();

            Assert.Equal("episode=S01E01", result);
        }

        [Fact]
        public void ToQueryString_WithBothValues_ReturnsQueryJoinedByAmpersand()
        {
            var filter = new EpisodeFilter
            {
                Name = "Rick & Morty",
                Episode = "S01E01"
            };

            var result = filter.ToQueryString();

            Assert.Contains("name=Rick%20%26%20Morty", result);
            Assert.Contains("episode=S01E01", result);
            Assert.Equal(2, result.Split('&').Length);
        }
    }
}
