using Microsoft.Extensions.Logging;
using RainfailForecast.API.Domain.Model;
using RainfallForecast.API.Services.Http;
using RainfallForecast.API.Services.Patterns;

namespace RainfallForecast.API.Services.Queries.Rainfall
{

    public interface IRainfallListQuery : IQueryAsync<Result<RainfallInfo>, int>
    {

    }

    public class RainfallListQuery : ApiClient, IRainfallListQuery
    {
        public RainfallListQuery(ILogger<RainfallInfo> logger) : base(logger)
        {
        }
        public async Task<Result<RainfallInfo>> ExecuteAsync(int criteria, CancellationToken cancellationToken = default)
        {
            var url = $"https://environment.data.gov.uk/flood-monitoring/id/stations?parameter=rainfall&_limit=50";
            var response = await GetAsync<RainfallInfo>(url, cancellationToken);
            return HandleApiResult(response);
        }
    }
}
