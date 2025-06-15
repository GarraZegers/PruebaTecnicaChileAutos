

namespace PruebaTecnicaChileautos.Tests.Core.Classes
{
    public class LocationInfoTest
    {
        [Fact]
        public void LocationInfo_Should_Initialize_Properly()
        {
            var pageInfo = new LocationInfo
            {
                Name = "ChileAutos",
                Url = "https://www.chileautos.cl"
            };

            Assert.Equal("ChileAutos", pageInfo.Name);
            Assert.Equal("https://www.chileautos.cl", pageInfo.Url);
        }

        [Fact]
        public void LocationInfo_DefaultConstructor_InitializesProperties() 
        {
            var locationInfo = new LocationInfo();

            Assert.NotNull(locationInfo.Url);
            Assert.NotNull(locationInfo.Name);
        }
    }
}
