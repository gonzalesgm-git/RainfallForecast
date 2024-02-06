using Microsoft.AspNetCore.Mvc;
using RainfailForecast.API.Domain.Model;
using RainfallForecast.API.Services.Queries.Rainfall;
using System.Net;

namespace RainfallForecast.API.Controllers.Rainfall
{
    [ApiController]
    [Route("[controller]")]
    public class RainfallController : ControllerBase
    {
        private readonly IRainfallListQuery _rainfallListQuery;
        public RainfallController(IRainfallListQuery rainfallListQuery)
        {
            _rainfallListQuery = rainfallListQuery;
        }

        /// <summary>List of RainfallInfo</summary>
        /// <returns></returns>
        [HttpGet]
        [Route("flood-monitoring/{limit}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<Readings>))]
        public async Task<IActionResult> RainfallInfo(CancellationToken cancellationToken, int limit = 50)
        {
            var items = await _rainfallListQuery.ExecuteAsync(limit, cancellationToken);

            return Ok(items);
        }

        /// <summary>List of RainfallInfo By Station</summary>
        /// <returns></returns>
        [HttpGet]
        [Route("flood-monitoring/{stationId}/measures")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<StationMeasures>))]
        public async Task<IActionResult> RainfallInfoByStation(CancellationToken cancellationToken, int stationId = 3680)
        {
            var items = await _rainfallListQuery.StationRainfallMeasures(stationId, cancellationToken);

            return Ok(items);
        }

        /// <summary>List of All of Station RainfallInfo</summary>
        /// <returns></returns>
        [HttpGet]
        [Route("flood-monitoring/{stationId}/reading/{limit}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<StationReadings>))]
        public async Task<IActionResult> AllRe(CancellationToken cancellationToken, int stationId = 3680, int limit = 100)
        {
            var items = await _rainfallListQuery.StationRainfallReadings(stationId, limit, cancellationToken);

            return Ok(items);
        }
    }
}
