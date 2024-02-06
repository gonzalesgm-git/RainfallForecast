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
        [Route("flood-monitoring/{stationId}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<RainfallInfo>))]
        public async Task<IActionResult> RainfallInfo(int stationId, CancellationToken cancellationToken)
        {
            var items = await _rainfallListQuery.ExecuteAsync(stationId, cancellationToken);

            return Ok(items);
        }
    }
}
