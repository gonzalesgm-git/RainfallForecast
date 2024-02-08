using Microsoft.Extensions.Logging;
using RainfailForecast.API.Domain.Model;
using RainfallForecast.API.Services.Http;
using RainfallForecast.API.Services.Patterns;

namespace RainfallForecast.API.Services.Queries.Rainfall
{

    public interface IRainfallListQuery : IQueryAsync<Result<Readings>, int>
    {
        public Task<Result<StationMeasures>> StationRainfallMeasures(int stationId, CancellationToken cancellationToken);
        public Task<Result<StationReadings>> StationRainfallReadings(int stationId, int limit, CancellationToken cancellationToken);
    }

    public class RainfallListQuery : ApiClient, IRainfallListQuery
    {
        const string floodMonitoringUrl = "https://environment.data.gov.uk/flood-monitoring";

        public RainfallListQuery(ILogger<Readings> logger) : base(logger)
        {
        }
        public async Task<Result<Readings>> ExecuteAsync(int criteria, CancellationToken cancellationToken = default)
        {
            var url = $"{floodMonitoringUrl}/id/stations?parameter=rainfall&_limit={50}";
            var response = await GetAsync<Readings>(url, cancellationToken);
            return HandleApiResult(response);
        }

        public async Task<Result<StationMeasures>> StationRainfallMeasures(int stationId, CancellationToken cancellationToken = default)
        {
            var url = $"{floodMonitoringUrl}/id/stations/{stationId}/measures";
            var response = await GetAsync<StationMeasures>(url, cancellationToken);
            return HandleApiResult(response);
        }

        public async Task<Result<StationReadings>> StationRainfallReadings(int stationId, int limit, CancellationToken cancellationToken = default)
        {
            var url = $"{floodMonitoringUrl}/id/stations/{stationId}/readings?_sorted&_limit={limit}";
            var response = await GetAsync<StationReadings>(url, cancellationToken);
            return HandleApiResult(response);
        }
    }
}
