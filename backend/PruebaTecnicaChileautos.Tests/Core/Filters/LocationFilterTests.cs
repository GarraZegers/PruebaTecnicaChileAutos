

namespace PruebaTecnicaChileautos.Tests.Core.Filters
{
    public class LocationFilterTests
    {
        [Fact]
        public void ToQueryString_WithNoValues_ReturnsEmptyString()
        {
            var filter = new LocationFilter();

            var result = filter.ToQueryString();

            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void ToQueryString_WithOnlyName_ReturnsNameParam()
        {
            var filter = new LocationFilter
            {
                Name = "Citadel of Ricks"
            };

            var result = filter.ToQueryString();

            Assert.Equal("name=Citadel%20of%20Ricks", result);
        }

        [Fact]
        public void ToQueryString_WithOnlyType_ReturnsTypeParam()
        {
            var filter = new LocationFilter
            {
                Type = "Space Station"
            };

            var result = filter.ToQueryString();

            Assert.Equal("type=Space%20Station", result);
        }

        [Fact]
        public void ToQueryString_WithOnlyDimension_ReturnsDimensionParam()
        {
            var filter = new LocationFilter
            {
                Dimension = "C-137"
            };

            var result = filter.ToQueryString();

            Assert.Equal("dimension=C-137", result);
        }

        [Fact]
        public void ToQueryString_WithAllFields_ReturnsAllQueryParams()
        {
            var filter = new LocationFilter
            {
                Name = "Earth",
                Type = "Planet",
                Dimension = "Replacement Dimension"
            };

            var result = filter.ToQueryString();

            Assert.Contains("name=Earth", result);
            Assert.Contains("type=Planet", result);
            Assert.Contains("dimension=Replacement%20Dimension", result);
            Assert.Equal(3, result.Split('&').Length);
        }
    }
}
