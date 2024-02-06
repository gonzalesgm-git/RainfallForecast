using RainfallForecast.API.Services.Queries.Patterns;

namespace RainfallForecast.API.Services.Queries.Rainfall
{

    public interface IRainfallListQuery : IQueryAsync<List<RainfallInfo>, int>
    {

    }

    public class RainfallListQuery : IRainfallListQuery
    {
        public async Task<List<RainfallInfo>> ExecuteAsync(int criteria, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }

    public class RainfallInfo
    {

    }
}
