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

        [Fact]
        public async Task StationRainfallReadings()
        {
            _rainfallListQuery.Setup(q => q.StationRainfallReadings(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                 .ReturnsAsync(new Result<StationReadings>
                 {
                     Value = new StationReadings()
                     {
                         Meta = new Meta { Publisher = "Publisher1" },
                         Items = new List<LatestReading>()
                         {
                             new LatestReading
                             {
                                 Date = new DateTime().Date.ToString(),
                                 Measure = "Measure1",
                             }
                         }.ToArray()
                     }
                 });

            var result = await _rainfallListQuery.Object.StationRainfallReadings(3680, 100, default);

            Assert.Single(result.Value.Items);
        }
    }
}