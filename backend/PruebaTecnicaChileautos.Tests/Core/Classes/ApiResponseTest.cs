

namespace PruebaTecnicaChileautos.Tests.Core.Classes
{
    public class ApiResponseTest
    {
        [Fact]
        public void ApiResponse_Should_Initialize_Properly()
        {
            var apiResponse = new ApiResponse<EpisodeDto>
            {
                Info = new PageInfo
                {
                    Count = 1,
                    Pages = 1,
                    Next = "",
                    Prev = "",
                },
                Results = [new EpisodeDto
                {
                    Id = 1,
                    Name = "Pilot",
                    AirDate = "Decembre 2, 2013",
                    Episode = "S01E01",
                    Characters = [
                            "https://rickandmortyapi.com/api/character/1",
                            "https://rickandmortyapi.com/api/character/2"
                        ],
                    Url = "https://rickandmortyapi.com/api/episode/1",
                    Created = "2017-11-10T12:56:33.798Z"
                }]
            };

            Assert.Equal(1, apiResponse.Info.Count);
            Assert.Equal(1, apiResponse.Info.Pages);
            Assert.Equal("", apiResponse.Info.Next);
            Assert.Equal("", apiResponse.Info.Next);
            Assert.Single(apiResponse.Results);
            
        }
    }

}
