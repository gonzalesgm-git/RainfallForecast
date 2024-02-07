using Moq;
using RainfailForecast.API.Domain.Model;
using RainfallForecast.API.Services.Http;
using RainfallForecast.API.Services.Queries.Rainfall;

namespace RainfallForecast.Api.Services.Tests.Queries
{
    public class RainfallListQueryTest
    {
        private readonly Mock<IRainfallListQuery> _rainfallListQuery;

        public RainfallListQueryTest()
        {
            _rainfallListQuery = new Mock<IRainfallListQuery>();
        }

        [Fact]
        public async Task ExecuteAsync()
        {
            _rainfallListQuery.Setup(q => q.ExecuteAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                 .ReturnsAsync(new Result<Readings>
                 {
                     Value = new Readings()
                     {
                         Meta = new Meta { Publisher = "Publisher1" },
                         Items = new List<RainfallInfo>()
                         {
                             new RainfallInfo
                             {
                                 Easting = 1,
                                 Label = "label1"

                             },
                             new RainfallInfo
                             {
                                 Easting = 2,
                                 Label = "label2"

                             }
                         }.ToArray()
                     }
                 });

            var result = await _rainfallListQuery.Object.ExecuteAsync(50, default);

            Assert.Equal(2, result.Value.Items.Count());
        }
    }
}