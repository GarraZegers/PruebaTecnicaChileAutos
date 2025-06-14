using PruebaTecnicaChileautos.Core.Classes;
using System;


namespace PruebaTecnicaChileautos.Tests.Core.Classes
{
    public class PageInfoTests
    {
        [Fact]
        public void PageInfo_Should_Initialize_Properly()
        {
            var pageInfo = new PageInfo
            {
                Count = 42,
                Pages = 5,
                Next = "next_url",
                Prev = "prev_url"
            };

            Assert.Equal(42, pageInfo.Count);
            Assert.Equal(5, pageInfo.Pages);
            Assert.Equal("next_url", pageInfo.Next);
            Assert.Equal("prev_url", pageInfo.Prev);
        }

        [Fact]
        public void PageInfo_DefaultConstructor_Initialized_Attributes() 
        {
            var pageInfo = new PageInfo();

            Assert.Null(pageInfo.Next);
            Assert.Null(pageInfo.Prev);
            Assert.False(pageInfo.Count < 0);
            Assert.False(pageInfo.Pages < 0);
            
        }
    }
}
