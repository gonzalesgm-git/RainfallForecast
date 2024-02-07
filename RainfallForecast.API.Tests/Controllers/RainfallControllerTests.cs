using Microsoft.AspNetCore.Mvc;
using Moq;
using RainfallForecast.API.Controllers.Rainfall;
using RainfallForecast.API.Services.Queries.Rainfall;

namespace RainfallForecast.API.Tests.Controllers
{
    public class RainfallControllerTests
    {
        private readonly Mock<IRainfallListQuery> _rainfallListQuery;
        private readonly RainfallController _rainfallController;

        public RainfallControllerTests()
        {
            _rainfallListQuery = new Mock<IRainfallListQuery>();
            _rainfallController = new RainfallController(_rainfallListQuery.Object);
        }

        [Fact]
        public async Task RainfallInfo_ReturnsOkResult()
        {
            var result = await _rainfallController.RainfallInfo(default, 50);
            Assert.IsType<OkObjectResult>(result);
        }


        [Fact]
        public async Task StationRainfallMeasures_ReturnsOkResult()
        {
            var result = await _rainfallController.StationRainfallMeasures(default, 3680);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task StationRainfallReadings_ReturnsOkResult()
        {
            var result = await _rainfallController.StationRainfallReadings(default, 3680, 100);
            Assert.IsType<OkObjectResult>(result);
        }
    }
}