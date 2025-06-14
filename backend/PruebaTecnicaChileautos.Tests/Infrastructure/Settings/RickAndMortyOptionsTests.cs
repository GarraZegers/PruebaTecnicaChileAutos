namespace PruebaTecnicaChileautos.Tests.Infrastructure.Settings
{
    public class RickAndMortyOptionsTests
    {
        [Fact]
        public void Should_Initializes_With_Parameters()
        {
            var rickAndMortyOptions = new RickAndMortyOptions
            {
                BaseUrl = "http://www.chileautos.cl"
            };

            Assert.NotEmpty(rickAndMortyOptions.BaseUrl);
            Assert.Equal("http://www.chileautos.cl", rickAndMortyOptions.BaseUrl);
        }

        [Fact]
        public void Default_Constructor_Without_NullValues()
        {
            var rickAndMortyOptions = new RickAndMortyOptions();

            Assert.NotNull(rickAndMortyOptions.BaseUrl);
        }
    }
}
